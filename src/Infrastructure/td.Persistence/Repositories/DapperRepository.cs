using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace td.Persistence.Repositories
{

    public enum enmTransactionState
    {
        Idle = 1,
        Opened = 2,
        Committed = 3,
        Rollbacked = 4
    }
    
    public class DapperRepository : IDisposable
    {

        #region ..:: Definitions ::..
        public IDbConnection Connection = null;
        public IDbTransaction Transaction = null;
        private const string DefaultConnectionStringName = "SqlConnection";
        private enmTransactionState trnState = enmTransactionState.Idle;
        private readonly IConfiguration configuration;

        #endregion

        #region ..:: Constructors ::..
        public DapperRepository(IConfiguration configuration)
        {
            CreateConnection(configuration.GetConnectionString(DefaultConnectionStringName));
        }
        public DapperRepository(IConfiguration configuration, IDbConnection pConection) : this(configuration, pConection, null) { }
        public DapperRepository(IConfiguration configuration, IDbConnection pConnection, IDbTransaction pTransaction)
        {
            this.configuration = configuration;
            this.Connection = pConnection;
            this.Transaction = pTransaction;
        }

        public DapperRepository(string pConnectionString)
        {
            CreateConnection(pConnectionString);
        }

        #endregion

        #region ..:: Utils ::..

        private void CreateConnection(string pConnectionString)
        {
            CreateMySqlSQLConnection(pConnectionString);
        }

        public IDbConnection CreateMasterConnection(IConfiguration configuration)
        {
            return new System.Data.SqlClient.SqlConnection(configuration.GetConnectionString(DefaultConnectionStringName));
        }
        /// <summary>
        /// Create Ms Sql Connection
        /// </summary>
        /// <param name="pConnectionString">Connection String</param>

        private void CreateMySqlSQLConnection(string pConnectionString)
        {
            this.Connection = new System.Data.SqlClient.SqlConnection(pConnectionString);
        }
        /// <summary>
        /// Create My Sql Connection
        /// </summary>
        /// <param name="pConnectionString">Connection String</param>
        private Exception CreateTransactionException()
        {
            return new Exception(string.Format("Transaction already {0}!", this.TranSactionState.ToString()));
        }
        public void Dispose()
        {
            if (Transaction != null) this.Transaction.Dispose();
            if (Connection != null) this.Connection.Dispose();
        }
        #endregion

        #region ..:: Transaction ::..
        public enmTransactionState TranSactionState
        {
            get { return this.trnState; }
        }
        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.ReadCommitted);
        }
        public IDbTransaction BeginTransaction(IsolationLevel pIsolationLevel)
        {
            if (this.TranSactionState == enmTransactionState.Opened) throw CreateTransactionException();
            if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
            DisposeTransaction();
            this.Transaction = this.Connection.BeginTransaction(pIsolationLevel);
            if (this.Transaction != null) this.trnState = enmTransactionState.Opened;
            return Transaction;
        }
        public void CommitTransaction()
        {
            if (this.TranSactionState != enmTransactionState.Opened) throw CreateTransactionException();
            this.Transaction.Commit();
            this.trnState = enmTransactionState.Committed;
        }
        public void RollbackTransaction()
        {
            if (this.TranSactionState != enmTransactionState.Opened) throw CreateTransactionException();
            this.Transaction.Rollback();
            this.trnState = enmTransactionState.Rollbacked;
        }
        private void DisposeTransaction()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
                this.trnState = enmTransactionState.Idle;
            }
        }
        #endregion

        #region ..:: Execution Methods (Dapper Proxy) ::..
        public int Execute(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<int> Target = delegate ()
            {
                return SqlMapper.Execute(this.Connection, sql, param, this.Transaction, commandTimeout, commandType);
            };
            return Invoke<int>(Target, sql, param);
        }


        public async Task<int> ExecuteAsync(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<int>.Factory.StartNew(() => Execute(sql, param, commandTimeout, commandType));
        }

        public IDataReader ExecuteReader(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IDataReader> Target = delegate ()
            {
                return SqlMapper.ExecuteReader(this.Connection, sql, param, this.Transaction, commandTimeout, commandType);
            };
            return Invoke<IDataReader>(Target, sql, param);
        }
        public async Task<IDataReader> ExecuteReaderAsync(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IDataReader>.Factory.StartNew(() => ExecuteReader(sql, param, commandTimeout, commandType));
        }

        public object ExecuteScalar(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<object> Target = delegate ()
            {
                return SqlMapper.ExecuteScalar(this.Connection, sql, param, this.Transaction, commandTimeout, commandType);
            };
            return Invoke<object>(Target, sql, param);
        }
        public async Task<object> ExecuteScalarAsync(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<object>.Factory.StartNew(() => ExecuteScalar(sql, param, commandTimeout, commandType));
        }

        public T ExecuteScalar<T>(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<T> Target = delegate ()
            {
                return SqlMapper.ExecuteScalar<T>(this.Connection, sql, param, this.Transaction, commandTimeout, commandType);
            };
            return Invoke<T>(Target, sql, param);
        }
        public async Task<T> ExecuteScalarAsync<T>(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<T>.Factory.StartNew(() => ExecuteScalar<T>(sql, param, commandTimeout, commandType));
        }

        public IEnumerable<dynamic> Query(string sql, dynamic param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<dynamic>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query(this.Connection, sql, param, this.Transaction, buffered, commandTimeout, commandType);
            };
            return Invoke<IEnumerable<dynamic>>(Target, sql, param);
        }
        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<dynamic>>.Factory.StartNew(() => Query(sql, param, true, commandTimeout, commandType));
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, dynamic param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await SqlMapper.QueryFirstOrDefaultAsync<T>(this.Connection, sql, param, this.Transaction, commandTimeout, commandType);

        }
        public IEnumerable<T> Query<T>(string sql, dynamic param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<T>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query<T>(this.Connection, sql, param, this.Transaction, buffered, commandTimeout, commandType);
            };

            return Invoke<IEnumerable<T>>(Target, sql, param);
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<T>>.Factory.StartNew(() => Query<T>(sql, param, true, commandTimeout, commandType));
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<TReturn>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query<TFirst, TSecond, TReturn>(this.Connection, sql, map, param, this.Transaction, buffered, splitOn, commandTimeout, commandType);
            };

            return Invoke<IEnumerable<TReturn>>(Target, sql, param);
        }
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<TReturn>>.Factory.StartNew(() => Query<TFirst, TSecond, TReturn>(sql, map, param, buffered, splitOn, commandTimeout, commandType));
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<TReturn>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query<TFirst, TSecond, TThird, TReturn>(this.Connection, sql, map, param, this.Transaction, buffered, splitOn, commandTimeout, commandType);
            };

            return Invoke<IEnumerable<TReturn>>(Target, sql, param);
        }
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<TReturn>>.Factory.StartNew(() => Query<TFirst, TSecond, TThird, TReturn>(sql, map, param, buffered, splitOn, commandTimeout, commandType));
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<TReturn>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TReturn>(this.Connection, sql, map, param, this.Transaction, buffered, splitOn, commandTimeout, commandType);
            };

            return Invoke<IEnumerable<TReturn>>(Target, sql, param);
        }
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<TReturn>>.Factory.StartNew(() => Query<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map, param, buffered, splitOn, commandTimeout, commandType));
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<TReturn>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this.Connection, sql, map, param, this.Transaction, buffered, splitOn, commandTimeout, commandType);
            };

            return Invoke<IEnumerable<TReturn>>(Target, sql, param);
        }
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, bool buffered = true, string splintOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<TReturn>>.Factory.StartNew(() => Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, map, param, buffered, splintOn, commandTimeout, commandType));
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, dynamic param = null, bool buffered = true, string splintOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<TReturn>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this.Connection, sql, map, param, this.Transaction, buffered, splintOn, commandTimeout, commandType);
            };

            return Invoke<IEnumerable<TReturn>>(Target, sql, param);
        }
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, dynamic param = null, bool buffered = true, string splintOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<TReturn>>.Factory.StartNew(() => Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql, map, param, buffered, splintOn, commandTimeout, commandType));
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, dynamic param = null, bool buffered = true, string splintOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<TReturn>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this.Connection, sql, map, param, this.Transaction, buffered, splintOn, commandTimeout, commandType);
            };

            return Invoke<IEnumerable<TReturn>>(Target, sql, param);
        }
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, dynamic param = null, bool buffered = true, string splintOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<TReturn>>.Factory.StartNew(() => Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, map, param, buffered, splintOn, commandTimeout, commandType));
        }

        public MultiReader QueryMultiple(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<SqlMapper.GridReader> Target = delegate ()
            {
                return SqlMapper.QueryMultiple(this.Connection, sql, param, this.Transaction, commandTimeout, commandType);
            };
            return new MultiReader(Invoke<SqlMapper.GridReader>(Target, sql, param));
        }

        public async Task<MultiReader> QueryMultipleAsync(string sql, dynamic param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<MultiReader>.Factory.StartNew(() => QueryMultiple(sql, param, commandTimeout, commandType));
        }

        public IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Func<IEnumerable<TReturn>> Target = delegate ()
            {
                return Dapper.SqlMapper.Query<TReturn>(this.Connection, sql, types, map, param, this.Transaction, buffered, splitOn, commandTimeout, commandType);
            };
            return Invoke<IEnumerable<TReturn>>(Target, sql, param);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Task<IEnumerable<TReturn>>.Factory.StartNew(() => Query(sql, types, map, param, buffered, splitOn, commandTimeout, commandType));
        }

        #endregion

        #region ..:: Invoke ::..

        private T Invoke<T>(Func<T> pFunction, string sql, dynamic param = null)
        {
            Guid Key = Guid.NewGuid();
            dynamic Result = null;
            //Stopwatch sw = new Stopwatch();
            try
            {
                //BeforeExecution<T>(Key, sql, param);
                //sw.Start();
                Result = pFunction.Invoke();
                //sw.Stop();
                return Result;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            finally
            {
                //AfterExecution<T>(Key, Result, sw.Elapsed, sql, param);
            }
        }
        #endregion
    }

}


