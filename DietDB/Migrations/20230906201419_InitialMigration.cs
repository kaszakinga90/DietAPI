using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietDB.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryOfDiet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Micronutrient = table.Column<float>(type: "real", nullable: false),
                    Macronutrient = table.Column<float>(type: "real", nullable: false),
                    Calories = table.Column<float>(type: "real", nullable: false),
                    Glycemic = table.Column<float>(type: "real", nullable: false),
                    DishPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipesId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Micronutrient = table.Column<float>(type: "real", nullable: false),
                    Macronutrient = table.Column<float>(type: "real", nullable: false),
                    Calories = table.Column<float>(type: "real", nullable: false),
                    Glycemic = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutPhoto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tooltip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tooltip", x => x.Id);
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "SingleDiet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealTimeHour = table.Column<int>(type: "int", nullable: false),
                    MealTimeMinute = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayWeekId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleDiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleDiet_DayWeek_DayWeekId",
                        column: x => x.DayWeekId,
                        principalTable: "DayWeek",
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "DishIngredient",
                columns: table => new
                {
                    DishesId = table.Column<int>(type: "int", nullable: false),
                    IngredientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishIngredient", x => new { x.DishesId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_DishIngredient_Dish_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishIngredient_Ingredient_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredient",
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "Navbar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LayoutCategoryId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    DishesId = table.Column<int>(type: "int", nullable: false),
                    MeasuresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishMeasure", x => new { x.DishesId, x.MeasuresId });
                    table.ForeignKey(
                        name: "FK_DishMeasure_Dish_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishMeasure_Measure_MeasuresId",
                        column: x => x.MeasuresId,
                        principalTable: "Measure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "CategoryOfDietSingleDiet",
                columns: table => new
                {
                    DietCategoriesId = table.Column<int>(type: "int", nullable: false),
                    SingleDietsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryOfDietSingleDiet", x => new { x.DietCategoriesId, x.SingleDietsId });
                    table.ForeignKey(
                        name: "FK_CategoryOfDietSingleDiet_CategoryOfDiet_DietCategoriesId",
                        column: x => x.DietCategoriesId,
                        principalTable: "CategoryOfDiet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryOfDietSingleDiet_SingleDiet_SingleDietsId",
                        column: x => x.SingleDietsId,
                        principalTable: "SingleDiet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealTimeSingleDiet",
                columns: table => new
                {
                    MealTimesId = table.Column<int>(type: "int", nullable: false),
                    SingleDietsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTimeSingleDiet", x => new { x.MealTimesId, x.SingleDietsId });
                    table.ForeignKey(
                        name: "FK_MealTimeSingleDiet_MealTime_MealTimesId",
                        column: x => x.MealTimesId,
                        principalTable: "MealTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealTimeSingleDiet_SingleDiet_SingleDietsId",
                        column: x => x.SingleDietsId,
                        principalTable: "SingleDiet",
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
                name: "Footer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Footer_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isPatient = table.Column<bool>(type: "bit", nullable: false),
                    isDietician = table.Column<bool>(type: "bit", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    PatientCardId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Survey_PatientCard_PatientCardId",
                        column: x => x.PatientCardId,
                        principalTable: "PatientCard",
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
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "Dietician",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RatingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dietician", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dietician_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dietician_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id");
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "Diet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DieteticianId = table.Column<int>(type: "int", nullable: false),
                    DieticianId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diet_Dietician_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "Dietician",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DieticianMessage",
                columns: table => new
                {
                    DieticiansId = table.Column<int>(type: "int", nullable: false),
                    MessagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieticianMessage", x => new { x.DieticiansId, x.MessagesId });
                    table.ForeignKey(
                        name: "FK_DieticianMessage_Dietician_DieticiansId",
                        column: x => x.DieticiansId,
                        principalTable: "Dietician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DieticianMessage_Message_MessagesId",
                        column: x => x.MessagesId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DieticianOffice",
                columns: table => new
                {
                    DieticianId = table.Column<int>(type: "int", nullable: false),
                    OfficesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieticianOffice", x => new { x.DieticianId, x.OfficesId });
                    table.ForeignKey(
                        name: "FK_DieticianOffice_Dietician_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "Dietician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DieticianOffice_Office_OfficesId",
                        column: x => x.OfficesId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diploma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diploma_Dietician_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "Dietician",
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodCatalog_Dietician_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "Dietician",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DieticianId = table.Column<int>(type: "int", nullable: false),
                    PatientCardId = table.Column<int>(type: "int", nullable: false),
                    SexId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Dietician_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "Dietician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_PatientCard_PatientCardId",
                        column: x => x.PatientCardId,
                        principalTable: "PatientCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_Sex_SexId",
                        column: x => x.SexId,
                        principalTable: "Sex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patient_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DietSingleDiet",
                columns: table => new
                {
                    DietsId = table.Column<int>(type: "int", nullable: false),
                    SingleDietsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietSingleDiet", x => new { x.DietsId, x.SingleDietsId });
                    table.ForeignKey(
                        name: "FK_DietSingleDiet_Diet_DietsId",
                        column: x => x.DietsId,
                        principalTable: "Diet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietSingleDiet_SingleDiet_SingleDietsId",
                        column: x => x.SingleDietsId,
                        principalTable: "SingleDiet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishFoodCatalog",
                columns: table => new
                {
                    DishesId = table.Column<int>(type: "int", nullable: false),
                    FoodCatalogsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishFoodCatalog", x => new { x.DishesId, x.FoodCatalogsId });
                    table.ForeignKey(
                        name: "FK_DishFoodCatalog_Dish_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishFoodCatalog_FoodCatalog_FoodCatalogsId",
                        column: x => x.FoodCatalogsId,
                        principalTable: "FoodCatalog",
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Dietician_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "Dietician",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DietPatient",
                columns: table => new
                {
                    DietsId = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPatient", x => new { x.DietsId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_DietPatient_Diet_DietsId",
                        column: x => x.DietsId,
                        principalTable: "Diet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietPatient_Patient_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessagePatient",
                columns: table => new
                {
                    MessagesId = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagePatient", x => new { x.MessagesId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_MessagePatient_Message_MessagesId",
                        column: x => x.MessagesId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessagePatient_Patient_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientRating",
                columns: table => new
                {
                    PatientsId = table.Column<int>(type: "int", nullable: false),
                    RatingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRating", x => new { x.PatientsId, x.RatingsId });
                    table.ForeignKey(
                        name: "FK_PatientRating_Patient_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientRating_Rating_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEqual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestEqual_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestEqual_PatientCard_PatientCardId",
                        column: x => x.PatientCardId,
                        principalTable: "PatientCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoAdded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whoDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visit_Dietician_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "Dietician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
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
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Address_OfficeId",
                table: "Address",
                column: "OfficeId");

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
                name: "IX_CategoryOfDietSingleDiet_SingleDietsId",
                table: "CategoryOfDietSingleDiet",
                column: "SingleDietsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_DieticianId",
                table: "Comment",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PatientId",
                table: "Comment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Diet_DieticianId",
                table: "Diet",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Dietician_RatingId",
                table: "Dietician",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_DieticianMessage_MessagesId",
                table: "DieticianMessage",
                column: "MessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_DieticianOffice_OfficesId",
                table: "DieticianOffice",
                column: "OfficesId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPatient_PatientsId",
                table: "DietPatient",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_DietSingleDiet_SingleDietsId",
                table: "DietSingleDiet",
                column: "SingleDietsId");

            migrationBuilder.CreateIndex(
                name: "IX_Diploma_DieticianId",
                table: "Diploma",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_DishFoodCatalog_FoodCatalogsId",
                table: "DishFoodCatalog",
                column: "FoodCatalogsId");

            migrationBuilder.CreateIndex(
                name: "IX_DishIngredient_IngredientsId",
                table: "DishIngredient",
                column: "IngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_DishMeasure_MeasuresId",
                table: "DishMeasure",
                column: "MeasuresId");

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
                name: "IX_Footer_AddressId",
                table: "Footer",
                column: "AddressId",
                unique: true);

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
                name: "IX_MealTimeSingleDiet_SingleDietsId",
                table: "MealTimeSingleDiet",
                column: "SingleDietsId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagePatient_PatientsId",
                table: "MessagePatient",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Navbar_LayoutCategoryId",
                table: "Navbar",
                column: "LayoutCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_PatientId",
                table: "Note",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_UserId",
                table: "Note",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DieticianId",
                table: "Patient",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PatientCardId",
                table: "Patient",
                column: "PatientCardId",
                unique: true,
                filter: "[PatientCardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_SexId",
                table: "Patient",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCard_SexId",
                table: "PatientCard",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRating_RatingsId",
                table: "PatientRating",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_DishId",
                table: "Recipe",
                column: "DishId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SingleDiet_DayWeekId",
                table: "SingleDiet",
                column: "DayWeekId");

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
                column: "LinkId");

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
                name: "IX_Survey_PatientCardId",
                table: "Survey",
                column: "PatientCardId");

            migrationBuilder.CreateIndex(
                name: "IX_TestEqual_PatientCardId",
                table: "TestEqual",
                column: "PatientCardId");

            migrationBuilder.CreateIndex(
                name: "IX_TestEqual_PatientId",
                table: "TestEqual",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_User_AddressId",
                table: "User",
                column: "AddressId");

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
                name: "CategoryOfDietSingleDiet");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "DieticianMessage");

            migrationBuilder.DropTable(
                name: "DieticianOffice");

            migrationBuilder.DropTable(
                name: "DietPatient");

            migrationBuilder.DropTable(
                name: "DietSingleDiet");

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
                name: "Examples");

            migrationBuilder.DropTable(
                name: "LayoutPhotoNews");

            migrationBuilder.DropTable(
                name: "MainNavbarTab");

            migrationBuilder.DropTable(
                name: "Manual");

            migrationBuilder.DropTable(
                name: "MealTimeSingleDiet");

            migrationBuilder.DropTable(
                name: "MessagePatient");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "PatientRating");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "SingleTestEqual");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "SubTab");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "Tooltip");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Carousel");

            migrationBuilder.DropTable(
                name: "CategoryOfDiet");

            migrationBuilder.DropTable(
                name: "Diet");

            migrationBuilder.DropTable(
                name: "FoodCatalog");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Measure");

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
                name: "MealTime");

            migrationBuilder.DropTable(
                name: "SingleDiet");

            migrationBuilder.DropTable(
                name: "Message");

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
                name: "CategoryType");

            migrationBuilder.DropTable(
                name: "DayWeek");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Footer");

            migrationBuilder.DropTable(
                name: "LayoutCategory");

            migrationBuilder.DropTable(
                name: "Dietician");

            migrationBuilder.DropTable(
                name: "PatientCard");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Sex");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Office");
        }
    }
}
