using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace td.Persistence.Migrations.Tables
{
    [Migration(1, "ProductTableMigration")]
    public class ProductTableMigration : Migration
    {

        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            CreateProductTable();
        }

        private void CreateProductTable()
        {
            Create.Table("Products")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("name").AsString().Nullable()
                .WithColumn("value").AsInt16().Nullable()
                .WithColumn("quantity").AsInt16().Nullable()
                .WithColumn("createdate").AsDateTime().Nullable();

        }
    }
}
