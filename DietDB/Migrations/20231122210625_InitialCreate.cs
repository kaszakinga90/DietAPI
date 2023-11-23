﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietDB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryOfDiet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryOfDiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayWeek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWeek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Footer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LayoutCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LayoutPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutPhoto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Heigth = table.Column<float>(type: "real", nullable: false),
                    Weith = table.Column<float>(type: "real", nullable: false),
                    MeasureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isPatient = table.Column<bool>(type: "bit", nullable: false),
                    isDietician = table.Column<bool>(type: "bit", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminsDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminsDb_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Office_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryTypeId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileCategory_CategoryType_CategoryTypeId",
                        column: x => x.CategoryTypeId,
                        principalTable: "CategoryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Step = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Dish_DishId",
                        column: x => x.DishId,
                        principalTable: "Dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayoutCategoryId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_LayoutCategory_LayoutCategoryId",
                        column: x => x.LayoutCategoryId,
                        principalTable: "LayoutCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carousel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayoutCategoryId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carousel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carousel_LayoutCategory_LayoutCategoryId",
                        column: x => x.LayoutCategoryId,
                        principalTable: "LayoutCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterId = table.Column<int>(type: "int", nullable: false),
                    LayoutCategoryId = table.Column<int>(type: "int", nullable: false),
                    SocialMediaId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Link_Footer_FooterId",
                        column: x => x.FooterId,
                        principalTable: "Footer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Link_LayoutCategory_LayoutCategoryId",
                        column: x => x.LayoutCategoryId,
                        principalTable: "LayoutCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Navbar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LayoutCategoryId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Navbar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Navbar_LayoutCategory_LayoutCategoryId",
                        column: x => x.LayoutCategoryId,
                        principalTable: "LayoutCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishMeasure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishMeasure_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LayoutPhotoNews",
                columns: table => new
                {
                    NewsesId = table.Column<int>(type: "int", nullable: false),
                    PhotosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutPhotoNews", x => new { x.NewsesId, x.PhotosId });
                    table.ForeignKey(
                        name: "FK_LayoutPhotoNews_LayoutPhoto_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "LayoutPhoto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LayoutPhotoNews_News_NewsesId",
                        column: x => x.NewsesId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SexId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCard_Sex_SexId",
                        column: x => x.SexId,
                        principalTable: "Sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<float>(type: "real", nullable: false),
                    ServingQuantity = table.Column<float>(type: "real", nullable: true),
                    MeasureId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    GlycemicIndex = table.Column<int>(type: "int", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredient_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nutrient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NutritionixId = table.Column<int>(type: "int", nullable: true),
                    NamePL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMacronutrient = table.Column<bool>(type: "bit", nullable: false),
                    IsMicronutrient = table.Column<bool>(type: "bit", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nutrient_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    FileCategoryId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_FileCategory_FileCategoryId",
                        column: x => x.FileCategoryId,
                        principalTable: "FileCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manual",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileCategoryId = table.Column<int>(type: "int", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manual_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Manual_FileCategory_FileCategoryId",
                        column: x => x.FileCategoryId,
                        principalTable: "FileCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleLayoutPhoto",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    PhotosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleLayoutPhoto", x => new { x.ArticlesId, x.PhotosId });
                    table.ForeignKey(
                        name: "FK_ArticleLayoutPhoto_Article_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleLayoutPhoto_LayoutPhoto_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "LayoutPhoto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTag",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTag", x => new { x.ArticlesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ArticleTag_Article_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarouselLayoutPhoto",
                columns: table => new
                {
                    CarouselsId = table.Column<int>(type: "int", nullable: false),
                    PhotosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselLayoutPhoto", x => new { x.CarouselsId, x.PhotosId });
                    table.ForeignKey(
                        name: "FK_CarouselLayoutPhoto_Carousel_CarouselsId",
                        column: x => x.CarouselsId,
                        principalTable: "Carousel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarouselLayoutPhoto_LayoutPhoto_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "LayoutPhoto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkId = table.Column<int>(type: "int", nullable: false),
                    FooterId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedia_Footer_FooterId",
                        column: x => x.FooterId,
                        principalTable: "Footer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SocialMedia_Link_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Link",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubTab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkId = table.Column<int>(type: "int", nullable: false),
                    TabId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTab", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTab_Link_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Link",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubTab_Tab_TabId",
                        column: x => x.TabId,
                        principalTable: "Tab",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MainNavbarTab",
                columns: table => new
                {
                    MainNavbarsId = table.Column<int>(type: "int", nullable: false),
                    TabsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainNavbarTab", x => new { x.MainNavbarsId, x.TabsId });
                    table.ForeignKey(
                        name: "FK_MainNavbarTab_Navbar_MainNavbarsId",
                        column: x => x.MainNavbarsId,
                        principalTable: "Navbar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainNavbarTab_Tab_TabsId",
                        column: x => x.TabsId,
                        principalTable: "Tab",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientCardSurvey",
                columns: table => new
                {
                    PatientCardId = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCardSurvey", x => new { x.PatientCardId, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_PatientCardSurvey_PatientCard_PatientCardId",
                        column: x => x.PatientCardId,
                        principalTable: "PatientCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientCardSurvey_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientCardId = table.Column<int>(type: "int", nullable: false),
                    SexId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isPatient = table.Column<bool>(type: "bit", nullable: false),
                    isDietician = table.Column<bool>(type: "bit", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientsDb_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientsDb_PatientCard_PatientCardId",
                        column: x => x.PatientCardId,
                        principalTable: "PatientCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientsDb_Sex_SexId",
                        column: x => x.SexId,
                        principalTable: "Sex",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DishIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IngredientNutrient",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    NutrientId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    NutrientValue = table.Column<float>(type: "real", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientNutrient", x => new { x.IngredientId, x.NutrientId });
                    table.ForeignKey(
                        name: "FK_IngredientNutrient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientNutrient_Nutrient_NutrientId",
                        column: x => x.NutrientId,
                        principalTable: "Nutrient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestEqual",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PatientCardId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEqual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestEqual_PatientCard_PatientCardId",
                        column: x => x.PatientCardId,
                        principalTable: "PatientCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestEqual_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DieticiansDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isPatient = table.Column<bool>(type: "bit", nullable: false),
                    isDietician = table.Column<bool>(type: "bit", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieticiansDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DieticiansDb_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DieticiansDb_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DieticiansDb_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleTestEqual",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test1 = table.Column<float>(type: "real", nullable: false),
                    test2 = table.Column<float>(type: "real", nullable: false),
                    test3 = table.Column<float>(type: "real", nullable: false),
                    TestEqualId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleTestEqual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleTestEqual_TestEqual_TestEqualId",
                        column: x => x.TestEqualId,
                        principalTable: "TestEqual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DieteticianId = table.Column<int>(type: "int", nullable: false),
                    DieticianId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    numberOfMeals = table.Column<int>(type: "int", nullable: false),
                    DieteticianId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diet_DieticiansDb_DieteticianId",
                        column: x => x.DieteticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diet_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DieticianOffice",
                columns: table => new
                {
                    DieticianId = table.Column<int>(type: "int", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieticianOffice", x => new { x.DieticianId, x.OfficeId });
                    table.ForeignKey(
                        name: "FK_DieticianOffice_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DieticianOffice_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DieticianPatientsDb",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DieticianId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieticianPatientsDb", x => new { x.PatientId, x.DieticianId });
                    table.ForeignKey(
                        name: "FK_DieticianPatientsDb_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DieticianPatientsDb_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diploma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoDiplomaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoDiplomaLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DieteticianId = table.Column<int>(type: "int", nullable: false),
                    DieticianId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diploma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diploma_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DieteticianId = table.Column<int>(type: "int", nullable: false),
                    DieticianId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodCatalog_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageToDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    DieticianId = table.Column<int>(type: "int", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageToDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageToDb_AdminsDb_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminsDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageToDb_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageToDb_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isVisibleToPatient = table.Column<bool>(type: "bit", nullable: false),
                    DieticianId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_AdminsDb_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminsDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Note_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    DieticianId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visit_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visit_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_Term_TermId",
                        column: x => x.TermId,
                        principalTable: "Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealTimeToXYAxis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    MealTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DietId = table.Column<int>(type: "int", nullable: true),
                    DishId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTimeToXYAxis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealTimeToXYAxis_Diet_DietId",
                        column: x => x.DietId,
                        principalTable: "Diet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MealTimeToXYAxis_Dish_DishId",
                        column: x => x.DishId,
                        principalTable: "Dish",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MealTimeToXYAxis_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishFoodCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodCatalogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishFoodCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishFoodCatalog_FoodCatalog_FoodCatalogId",
                        column: x => x.FoodCatalogId,
                        principalTable: "FoodCatalog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DieticianNote",
                columns: table => new
                {
                    DieticianId = table.Column<int>(type: "int", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieticianNote", x => new { x.DieticianId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_DieticianNote_DieticiansDb_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "DieticiansDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DieticianNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotePatient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotePatient", x => new { x.PatientId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_NotePatient_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotePatient_PatientsDb_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminsDb_AddressId",
                table: "AdminsDb",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_LayoutCategoryId",
                table: "Article",
                column: "LayoutCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLayoutPhoto_PhotosId",
                table: "ArticleLayoutPhoto",
                column: "PhotosId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_TagsId",
                table: "ArticleTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Carousel_LayoutCategoryId",
                table: "Carousel",
                column: "LayoutCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CarouselLayoutPhoto_PhotosId",
                table: "CarouselLayoutPhoto",
                column: "PhotosId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_DieticianId",
                table: "Comment",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PatientId",
                table: "Comment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Diet_DieteticianId",
                table: "Diet",
                column: "DieteticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Diet_PatientId",
                table: "Diet",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DieticianNote_NoteId",
                table: "DieticianNote",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_DieticianOffice_OfficeId",
                table: "DieticianOffice",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DieticianPatientsDb_DieticianId",
                table: "DieticianPatientsDb",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_DieticiansDb_AddressId",
                table: "DieticiansDb",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DieticiansDb_PatientId",
                table: "DieticiansDb",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DieticiansDb_RatingId",
                table: "DieticiansDb",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Diploma_DieticianId",
                table: "Diploma",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_DishFoodCatalog_FoodCatalogId",
                table: "DishFoodCatalog",
                column: "FoodCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_DishIngredient_IngredientId",
                table: "DishIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_DishMeasure_MeasureId",
                table: "DishMeasure",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_ContentId",
                table: "Document",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_FileCategoryId",
                table: "Document",
                column: "FileCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCategory_CategoryTypeId",
                table: "FileCategory",
                column: "CategoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodCatalog_DieticianId",
                table: "FoodCatalog",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_MeasureId",
                table: "Ingredient",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_UnitId",
                table: "Ingredient",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientNutrient_NutrientId",
                table: "IngredientNutrient",
                column: "NutrientId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutPhotoNews_PhotosId",
                table: "LayoutPhotoNews",
                column: "PhotosId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_FooterId",
                table: "Link",
                column: "FooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_LayoutCategoryId",
                table: "Link",
                column: "LayoutCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MainNavbarTab_TabsId",
                table: "MainNavbarTab",
                column: "TabsId");

            migrationBuilder.CreateIndex(
                name: "IX_Manual_ContentId",
                table: "Manual",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Manual_FileCategoryId",
                table: "Manual",
                column: "FileCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTimeToXYAxis_DietId",
                table: "MealTimeToXYAxis",
                column: "DietId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTimeToXYAxis_DishId",
                table: "MealTimeToXYAxis",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTimeToXYAxis_MealId",
                table: "MealTimeToXYAxis",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageToDb_AdminId",
                table: "MessageToDb",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageToDb_DieticianId",
                table: "MessageToDb",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageToDb_PatientId",
                table: "MessageToDb",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Navbar_LayoutCategoryId",
                table: "Navbar",
                column: "LayoutCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_AdminId",
                table: "Note",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_DieticianId",
                table: "Note",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_PatientId",
                table: "Note",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_NotePatient_NoteId",
                table: "NotePatient",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrient_UnitId",
                table: "Nutrient",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Office_AddressId",
                table: "Office",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCard_SexId",
                table: "PatientCard",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCardSurvey_SurveyId",
                table: "PatientCardSurvey",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsDb_AddressId",
                table: "PatientsDb",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsDb_PatientCardId",
                table: "PatientsDb",
                column: "PatientCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsDb_SexId",
                table: "PatientsDb",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_PatientId",
                table: "Rating",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_DishId",
                table: "Recipe",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleTestEqual_TestEqualId",
                table: "SingleTestEqual",
                column: "TestEqualId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_FooterId",
                table: "SocialMedia",
                column: "FooterId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_LinkId",
                table: "SocialMedia",
                column: "LinkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubTab_LinkId",
                table: "SubTab",
                column: "LinkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubTab_TabId",
                table: "SubTab",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_TestEqual_PatientCardId",
                table: "TestEqual",
                column: "PatientCardId");

            migrationBuilder.CreateIndex(
                name: "IX_TestEqual_PatientId",
                table: "TestEqual",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_DieticianId",
                table: "Visit",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_PatientId",
                table: "Visit",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_StatusId",
                table: "Visit",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_TermId",
                table: "Visit",
                column: "TermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleLayoutPhoto");

            migrationBuilder.DropTable(
                name: "ArticleTag");

            migrationBuilder.DropTable(
                name: "CarouselLayoutPhoto");

            migrationBuilder.DropTable(
                name: "CategoryOfDiet");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "DayWeek");

            migrationBuilder.DropTable(
                name: "DieticianNote");

            migrationBuilder.DropTable(
                name: "DieticianOffice");

            migrationBuilder.DropTable(
                name: "DieticianPatientsDb");

            migrationBuilder.DropTable(
                name: "Diploma");

            migrationBuilder.DropTable(
                name: "DishFoodCatalog");

            migrationBuilder.DropTable(
                name: "DishIngredient");

            migrationBuilder.DropTable(
                name: "DishMeasure");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "IngredientNutrient");

            migrationBuilder.DropTable(
                name: "LayoutPhotoNews");

            migrationBuilder.DropTable(
                name: "MainNavbarTab");

            migrationBuilder.DropTable(
                name: "Manual");

            migrationBuilder.DropTable(
                name: "MealTimeToXYAxis");

            migrationBuilder.DropTable(
                name: "MessageToDb");

            migrationBuilder.DropTable(
                name: "NotePatient");

            migrationBuilder.DropTable(
                name: "PatientCardSurvey");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "SingleTestEqual");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "SubTab");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Carousel");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropTable(
                name: "FoodCatalog");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Nutrient");

            migrationBuilder.DropTable(
                name: "LayoutPhoto");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Navbar");

            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "FileCategory");

            migrationBuilder.DropTable(
                name: "Diet");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "Dish");

            migrationBuilder.DropTable(
                name: "TestEqual");

            migrationBuilder.DropTable(
                name: "Link");

            migrationBuilder.DropTable(
                name: "Tab");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Measure");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "CategoryType");

            migrationBuilder.DropTable(
                name: "AdminsDb");

            migrationBuilder.DropTable(
                name: "DieticiansDb");

            migrationBuilder.DropTable(
                name: "Footer");

            migrationBuilder.DropTable(
                name: "LayoutCategory");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "PatientsDb");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "PatientCard");

            migrationBuilder.DropTable(
                name: "Sex");
        }
    }
}
