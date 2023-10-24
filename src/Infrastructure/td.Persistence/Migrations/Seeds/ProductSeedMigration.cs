using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace td.Persistence.Migrations.Seeds
{

    [Migration(101, "ProductSeedMigration")]
    public class ProductSeedMigration : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            SeedProducts();
        }

        private void SeedProducts()
        {
            Insert.IntoTable("Products")
                .Row(new
                {
                    id = new Guid("7309ce78-2fd4-4bf5-9eae-a6e8d9c494fc"),
                    name = "Keyboard",
                    value = 10,
                    quantity = 100,
                    createdate = DateTime.Now,

                })
                .Row(new
                {
                    id = new Guid("fc65e4a5-93a2-43cb-956a-ec12e36f5632"),
                    name = "Mouse",
                    value = 20,
                    quantity = 200,
                    createdate = DateTime.Now,
                });
        }
    }


}
