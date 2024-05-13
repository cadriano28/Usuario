using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuario.Migrations
{
    /// <inheritdoc />
    public partial class dbusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userCpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userTelefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userSenha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userNivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropTable(
        name: "Users");
}
    }
}
