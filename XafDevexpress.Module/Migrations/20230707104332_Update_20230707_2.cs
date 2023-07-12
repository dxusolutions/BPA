using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XafDevexpress.Module.Migrations
{
    /// <inheritdoc />
    public partial class Update_20230707_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyActionPermissionObject_PermissionPolicyRoleBase_RoleID",
                table: "PermissionPolicyActionPermissionObject");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyNavigationPermissionObject_PermissionPolicyRoleBase_RoleID",
                table: "PermissionPolicyNavigationPermissionObject");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyRolePermissionPolicyUser_PermissionPolicyRoleBase_RolesID",
                table: "PermissionPolicyRolePermissionPolicyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyTypePermissionObject_PermissionPolicyRoleBase_RoleID",
                table: "PermissionPolicyTypePermissionObject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionPolicyRoleBase",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.RenameTable(
                name: "PermissionPolicyRoleBase",
                newName: "RoleBase");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleBase",
                table: "RoleBase",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyActionPermissionObject_RoleBase_RoleID",
                table: "PermissionPolicyActionPermissionObject",
                column: "RoleID",
                principalTable: "RoleBase",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyNavigationPermissionObject_RoleBase_RoleID",
                table: "PermissionPolicyNavigationPermissionObject",
                column: "RoleID",
                principalTable: "RoleBase",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyRolePermissionPolicyUser_RoleBase_RolesID",
                table: "PermissionPolicyRolePermissionPolicyUser",
                column: "RolesID",
                principalTable: "RoleBase",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyTypePermissionObject_RoleBase_RoleID",
                table: "PermissionPolicyTypePermissionObject",
                column: "RoleID",
                principalTable: "RoleBase",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyActionPermissionObject_RoleBase_RoleID",
                table: "PermissionPolicyActionPermissionObject");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyNavigationPermissionObject_RoleBase_RoleID",
                table: "PermissionPolicyNavigationPermissionObject");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyRolePermissionPolicyUser_RoleBase_RolesID",
                table: "PermissionPolicyRolePermissionPolicyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyTypePermissionObject_RoleBase_RoleID",
                table: "PermissionPolicyTypePermissionObject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleBase",
                table: "RoleBase");

            migrationBuilder.RenameTable(
                name: "RoleBase",
                newName: "PermissionPolicyRoleBase");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionPolicyRoleBase",
                table: "PermissionPolicyRoleBase",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyActionPermissionObject_PermissionPolicyRoleBase_RoleID",
                table: "PermissionPolicyActionPermissionObject",
                column: "RoleID",
                principalTable: "PermissionPolicyRoleBase",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyNavigationPermissionObject_PermissionPolicyRoleBase_RoleID",
                table: "PermissionPolicyNavigationPermissionObject",
                column: "RoleID",
                principalTable: "PermissionPolicyRoleBase",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyRolePermissionPolicyUser_PermissionPolicyRoleBase_RolesID",
                table: "PermissionPolicyRolePermissionPolicyUser",
                column: "RolesID",
                principalTable: "PermissionPolicyRoleBase",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyTypePermissionObject_PermissionPolicyRoleBase_RoleID",
                table: "PermissionPolicyTypePermissionObject",
                column: "RoleID",
                principalTable: "PermissionPolicyRoleBase",
                principalColumn: "ID");
        }
    }
}
