using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace td.Persistence.Repositories
{
    public class MultiReader : IDisposable
    {
        private SqlMapper.GridReader Reader = null;

        internal MultiReader(SqlMapper.GridReader pGridReader)
        {
            this.Reader = pGridReader;
        }

        public bool IsConsumed
        {
            get { return this.Reader.IsConsumed; }
        }

        public IEnumerable<T> Read<T>(bool buffered = true)
        {
            return this.Reader.Read<T>(buffered);
        }

        public IEnumerable<dynamic> Read(bool buffered = true)
        {
            return this.Reader.Read(buffered);
        }

        public IEnumerable<object> Read(Type type, bool buffered = true)
        {
            return this.Reader.Read(type, buffered);
        }

        public IEnumerable<TReturn> Read<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            return this.Reader.Read<TFirst, TSecond, TReturn>(func, splitOn, buffered);
        }

        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TReturn>(Func<TFirst, TSecond, TThird, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            return this.Reader.Read<TFirst, TSecond, TThird, TReturn>(func, splitOn, buffered);
        }

        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            return this.Reader.Read<TFirst, TSecond, TThird, TFourth, TReturn>(func, splitOn, buffered);
        }

        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            return this.Reader.Read<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(func, splitOn, buffered);
        }

        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            return this.Reader.Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(func, splitOn, buffered);
        }

        public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            return this.Reader.Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(func, splitOn, buffered);
        }

        public IEnumerable<TReturn> Read<TReturn>(Type[] types, Func<object[], TReturn> map, string splitOn = "id", bool buffered = true)
        {
            return this.Reader.Read<TReturn>(types, map, splitOn, buffered);
        }

        public void Dispose()
        {
            this.Reader.Dispose();
        }
    }
}
