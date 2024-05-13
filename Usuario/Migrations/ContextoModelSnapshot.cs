﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Usuario.Models;

#nullable disable

namespace Usuario.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Usuario.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserLogin")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("userLogin");

                    b.Property<string>("UserNivel")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("userNivel");

                    b.Property<string>("UserNome")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("userNome");

                    b.Property<string>("UserSenha")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("userSenha");

                    b.Property<string>("UserStatus")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("userStatus");

                    b.Property<string>("UserTelefone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("userTelefone");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}