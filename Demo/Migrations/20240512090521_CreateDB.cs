using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
<<<<<<<< HEAD:Demo/Migrations/20240512062920_CreateDB.cs
                name: "Tutors",
========
                name: "Classes",
>>>>>>>> main:Demo/Migrations/20240512090521_CreateDB.cs
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
<<<<<<<< HEAD:Demo/Migrations/20240512062920_CreateDB.cs
                    Gender = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.Id);
========
                    ClassType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
>>>>>>>> main:Demo/Migrations/20240512090521_CreateDB.cs
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ClassesId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
<<<<<<<< HEAD:Demo/Migrations/20240512062920_CreateDB.cs
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClassType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<string>(type: "nvarchar(100)", nullable: true)
========
                    IsAttend = table.Column<bool>(type: "bit", maxLength: 1, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(100)", nullable: true)
>>>>>>>> main:Demo/Migrations/20240512090521_CreateDB.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fees = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    TutorId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Users_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClassesId = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClassesSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeSpan>(type: "Time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "Time", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassestId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassesSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassesSubjects_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassesSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
<<<<<<<< HEAD:Demo/Migrations/20240512062920_CreateDB.cs
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TutorId",
                table: "Classes",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesSubjects_ClassesId",
                table: "ClassesSubjects",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesSubjects_SubjectsId",
                table: "ClassesSubjects",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassesId",
                table: "Students",
                column: "ClassesId");
========
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");
>>>>>>>> main:Demo/Migrations/20240512090521_CreateDB.cs

            migrationBuilder.CreateIndex(
                name: "IX_ClassesSubjects_ClassId",
                table: "ClassesSubjects",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesSubjects_SubjectId",
                table: "ClassesSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TutorId",
                table: "Subjects",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassId",
                table: "Users",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
<<<<<<<< HEAD:Demo/Migrations/20240512062920_CreateDB.cs
                name: "ClassesSubjects");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Tutors");
========
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "ClassesSubjects");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classes");
>>>>>>>> main:Demo/Migrations/20240512090521_CreateDB.cs
        }
    }
}
