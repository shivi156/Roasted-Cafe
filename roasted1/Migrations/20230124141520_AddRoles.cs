using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace roasted1.Migrations
{
    public partial class AddRoles : Migration
    {
        private string AdminRoleID = Guid.NewGuid().ToString();

        private string MainAdminID = Guid.NewGuid().ToString();
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);

            SeedAdmin(migrationBuilder);

            SeedUserRoles(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES ('{AdminRoleID}', 'Admin', 'ADMIN', null);");
        }

        private void SeedAdmin(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[SecurityStamp],[ConcurrencyStamp],[LockoutEnd], [LockoutEnabled], [AccessFailedCount], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled]) VALUES (N'{MainAdminID}', N'Admin', N'Admin', N'admin@roasted.co.uk', N'ADMIN@ROASTED.CO.UK', 0, N'AQAAAAEAACcQAAAAEKGlmW1y/azayUAPnyBN2GRPG+81Cyksl1yuUcUauKruQ9kQ93ImSqXK919PcCMqAg==', N'5VZRXDLHPSQVENGQEPFDZOZLRMNSCWN5', N'b3a411d0-5272-49ef-8131-329af6b6fc08', NULL, 1, 0, N'010', 0, 0 )");
        }

        private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES ('{MainAdminID}', '{AdminRoleID}')");
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
