using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Shared.Enums;

namespace td.Persistence.Migrations.Seeds
{
    [Migration(102, "UserSeedMigration")]
    public class UserSeedMigration : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            SeedUsers();
        }

        private void SeedUsers()
        {
            Insert.IntoTable("Users")
                .Row(new
                {
                    id = new Guid("74b5e054-3e11-43c6-a9a9-0b0d3ec8a103"),
                    name = "Admin",
                    email = "admin@gmail.com",
                    password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    role = (int)enmRole.Admin,
                    createdate = DateTime.Now,

                })
                .Row(new
                {
                    id = new Guid("51d6a265-3606-44f1-bcea-dd4272adda52"),
                    name = "User",
                    email = "user@gmail.com",
                    password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    role = (int)enmRole.User,
                    createdate = DateTime.Now,
                });
        }
    }
}
