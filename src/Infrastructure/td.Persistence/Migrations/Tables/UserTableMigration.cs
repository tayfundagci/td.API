using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace td.Persistence.Migrations.Tables
{
    [Migration(2, "UserTableMigration")]
    public class UserTableMigration : Migration
    {

        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            CreateUserTable();
        }

        private void CreateUserTable()
        {
            Create.Table("Users")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("name").AsString().Nullable()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("role").AsInt16().NotNullable()
                .WithColumn("createdate").AsDateTime().Nullable();

        }
    }
}
