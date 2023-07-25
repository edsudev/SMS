using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class jupebapplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "UgApplicants",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JupepApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicantId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicantionId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtherName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sex = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaritalStatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlaceOfBirth = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermanentHomeAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NationalityId = table.Column<int>(type: "int", nullable: true),
                    StateOfOriginId = table.Column<int>(type: "int", nullable: true),
                    LGAId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AltPhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrimarySchool = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecondarySchool = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Indigine = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ModeOfEntry = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PreviousInstitution = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PreviousLevel = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PreviousGrade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstChoice = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecondChoice = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ThirdChoice = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoOfSittings = table.Column<int>(type: "int", nullable: true),
                    Ssce1Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Year = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject3 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject4 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject5 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject6 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject7 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject8 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject9 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject1Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject2Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject3Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject4Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject5Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject6Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject7Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject8Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1Subject9Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Year = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject3 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject4 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject5 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject6 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject7 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject8 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject9 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce2Subject1Grade = table.Column<int>(type: "int", nullable: true),
                    Ssce2Subject2Grade = table.Column<int>(type: "int", nullable: true),
                    Ssce2Subject3Grade = table.Column<int>(type: "int", nullable: true),
                    Ssce2Subject4Grade = table.Column<int>(type: "int", nullable: true),
                    Ssce2Subject5Grade = table.Column<int>(type: "int", nullable: true),
                    Ssce2Subject6Grade = table.Column<int>(type: "int", nullable: true),
                    Ssce2Subject7Grade = table.Column<int>(type: "int", nullable: true),
                    Ssce2Subject8Grade = table.Column<int>(type: "int", nullable: true),
                    Ssce2Subject9Grade = table.Column<int>(type: "int", nullable: true),
                    ParentFullName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentPhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentAlternatePhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentOccupation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PassportUpload = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ssce1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthCertUpload = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DirectEntryUpload = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LGAUpload = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Screened = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LevelAdmittedTo = table.Column<int>(type: "int", nullable: true),
                    AdmittedInto = table.Column<int>(type: "int", nullable: true),
                    ProgrameId = table.Column<int>(type: "int", nullable: true),
                    YearOfAdmission = table.Column<int>(type: "int", nullable: true),
                    Cleared = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Paid = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JupepApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JupepApplication_Countries_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupepApplication_Departments_AdmittedInto",
                        column: x => x.AdmittedInto,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupepApplication_Levels_LevelAdmittedTo",
                        column: x => x.LevelAdmittedTo,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupepApplication_Lgas_LGAId",
                        column: x => x.LGAId,
                        principalTable: "Lgas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupepApplication_Programs_ProgrameId",
                        column: x => x.ProgrameId,
                        principalTable: "Programs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupepApplication_Sessions_YearOfAdmission",
                        column: x => x.YearOfAdmission,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupepApplication_States_StateOfOriginId",
                        column: x => x.StateOfOriginId,
                        principalTable: "States",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JupebStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicantId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Picture = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fullname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sex = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Religion = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AltPhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NationalityId = table.Column<int>(type: "int", nullable: true),
                    StateOfOriginId = table.Column<int>(type: "int", nullable: true),
                    LGAId = table.Column<int>(type: "int", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermanentHomeAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaritalStatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentOccupation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentAltPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolEmailAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MatNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Faculty = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    ModeOfAdmission = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProgrameId = table.Column<int>(type: "int", nullable: true),
                    YearOfAdmission = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    CurrentSession = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Cleared = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ClearedBy = table.Column<int>(type: "int", nullable: true),
                    IsStillAStudent = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    StudentStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JupebStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JupebStudents_Countries_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_Departments_Department",
                        column: x => x.Department,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_Faculties_Faculty",
                        column: x => x.Faculty,
                        principalTable: "Faculties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_JupepApplication_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "JupepApplication",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_Levels_Level",
                        column: x => x.Level,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_Lgas_LGAId",
                        column: x => x.LGAId,
                        principalTable: "Lgas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_Programs_ProgrameId",
                        column: x => x.ProgrameId,
                        principalTable: "Programs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_Sessions_CurrentSession",
                        column: x => x.CurrentSession,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_Sessions_YearOfAdmission",
                        column: x => x.YearOfAdmission,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_Staffs_ClearedBy",
                        column: x => x.ClearedBy,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JupebStudents_States_StateOfOriginId",
                        column: x => x.StateOfOriginId,
                        principalTable: "States",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_ApplicantId",
                table: "JupebStudents",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_ClearedBy",
                table: "JupebStudents",
                column: "ClearedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_CurrentSession",
                table: "JupebStudents",
                column: "CurrentSession");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_Department",
                table: "JupebStudents",
                column: "Department");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_Faculty",
                table: "JupebStudents",
                column: "Faculty");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_Level",
                table: "JupebStudents",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_LGAId",
                table: "JupebStudents",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_NationalityId",
                table: "JupebStudents",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_ProgrameId",
                table: "JupebStudents",
                column: "ProgrameId");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_StateOfOriginId",
                table: "JupebStudents",
                column: "StateOfOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_JupebStudents_YearOfAdmission",
                table: "JupebStudents",
                column: "YearOfAdmission");

            migrationBuilder.CreateIndex(
                name: "IX_JupepApplication_AdmittedInto",
                table: "JupepApplication",
                column: "AdmittedInto");

            migrationBuilder.CreateIndex(
                name: "IX_JupepApplication_LevelAdmittedTo",
                table: "JupepApplication",
                column: "LevelAdmittedTo");

            migrationBuilder.CreateIndex(
                name: "IX_JupepApplication_LGAId",
                table: "JupepApplication",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_JupepApplication_NationalityId",
                table: "JupepApplication",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_JupepApplication_ProgrameId",
                table: "JupepApplication",
                column: "ProgrameId");

            migrationBuilder.CreateIndex(
                name: "IX_JupepApplication_StateOfOriginId",
                table: "JupepApplication",
                column: "StateOfOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_JupepApplication_YearOfAdmission",
                table: "JupepApplication",
                column: "YearOfAdmission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JupebStudents");

            migrationBuilder.DropTable(
                name: "JupepApplication");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "UgApplicants");
        }
    }
}
