using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class Conversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PgMainWallets_UgApplicants_ApplicantId",
                table: "PgMainWallets");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGraduateStudents_UgApplicants_ApplicantId",
                table: "PostGraduateStudents");

            migrationBuilder.CreateTable(
                name: "ConversionApplicants",
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
                    DOB = table.Column<DateTime>(type: "datetime(6)", nullable: true),
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
                    UTMENumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CourseChoseInJamb = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UTMESubject1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UTMESubject2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UTMESubject3 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UTMESubject4 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UTMESubject1Score = table.Column<int>(type: "int", nullable: true),
                    UTMESubject2Score = table.Column<int>(type: "int", nullable: true),
                    UTMESubject3Score = table.Column<int>(type: "int", nullable: true),
                    UTMESubject4Score = table.Column<int>(type: "int", nullable: true),
                    UTMETotal = table.Column<int>(type: "int", nullable: true),
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
                    JambUpload = table.Column<string>(type: "longtext", nullable: true)
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
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionApplicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionApplicants_Countries_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionApplicants_Departments_AdmittedInto",
                        column: x => x.AdmittedInto,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionApplicants_Levels_LevelAdmittedTo",
                        column: x => x.LevelAdmittedTo,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionApplicants_Lgas_LGAId",
                        column: x => x.LGAId,
                        principalTable: "Lgas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionApplicants_Programs_ProgrameId",
                        column: x => x.ProgrameId,
                        principalTable: "Programs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionApplicants_Sessions_YearOfAdmission",
                        column: x => x.YearOfAdmission,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionApplicants_States_StateOfOriginId",
                        column: x => x.StateOfOriginId,
                        principalTable: "States",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConversionPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameOfProgram = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionPrograms_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PgCourseRegs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseRegId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateApproved = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PgCourseRegs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PgCourseRegs_PgCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "PgCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PgCourseRegs_PostGraduateStudents_StudentId",
                        column: x => x.StudentId,
                        principalTable: "PostGraduateStudents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PgCourseRegs_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConversionMainWallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicantId = table.Column<int>(type: "int", nullable: true),
                    WalletId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreditBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BulkDebitBalanace = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionMainWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionMainWallets_ConversionApplicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ConversionApplicants",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConversionStudents",
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
                    DOB = table.Column<DateTime>(type: "datetime(6)", nullable: true),
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
                    UTMENumber = table.Column<string>(type: "longtext", nullable: true)
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
                    table.PrimaryKey("PK_ConversionStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionStudents_ConversionApplicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ConversionApplicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Countries_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Departments_Department",
                        column: x => x.Department,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Faculties_Faculty",
                        column: x => x.Faculty,
                        principalTable: "Faculties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Levels_Level",
                        column: x => x.Level,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Lgas_LGAId",
                        column: x => x.LGAId,
                        principalTable: "Lgas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Programs_ProgrameId",
                        column: x => x.ProgrameId,
                        principalTable: "Programs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Sessions_CurrentSession",
                        column: x => x.CurrentSession,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Sessions_YearOfAdmission",
                        column: x => x.YearOfAdmission,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_Staffs_ClearedBy",
                        column: x => x.ClearedBy,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudents_States_StateOfOriginId",
                        column: x => x.StateOfOriginId,
                        principalTable: "States",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConversionSubWallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicantId = table.Column<int>(type: "int", nullable: true),
                    WalletId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegNo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreditBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Tuition = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FortyPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SixtyPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LMS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AcceptanceFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SRC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDHIS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionSubWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionSubWallets_ConversionApplicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ConversionApplicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionSubWallets_Departments_Department",
                        column: x => x.Department,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionSubWallets_Levels_Level",
                        column: x => x.Level,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionSubWallets_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConversionCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Semester = table.Column<int>(type: "int", nullable: true),
                    ProgrammeId = table.Column<int>(type: "int", nullable: true),
                    CreditUnit = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionCourses_ConversionPrograms_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "ConversionPrograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionCourses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionCourses_Levels_Level",
                        column: x => x.Level,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionCourses_Semesters_Semester",
                        column: x => x.Semester,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConversionStudentSupervisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Student = table.Column<int>(type: "int", nullable: true),
                    Supervisor = table.Column<int>(type: "int", nullable: true),
                    SupervisorRole = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionStudentSupervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionStudentSupervisors_ConversionStudents_Student",
                        column: x => x.Student,
                        principalTable: "ConversionStudents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionStudentSupervisors_Staffs_Supervisor",
                        column: x => x.Supervisor,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConversionCourseRegs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseRegId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateApproved = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionCourseRegs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionCourseRegs_ConversionCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "ConversionCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionCourseRegs_ConversionStudents_StudentId",
                        column: x => x.StudentId,
                        principalTable: "ConversionStudents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionCourseRegs_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionApplicants_AdmittedInto",
                table: "ConversionApplicants",
                column: "AdmittedInto");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionApplicants_LevelAdmittedTo",
                table: "ConversionApplicants",
                column: "LevelAdmittedTo");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionApplicants_LGAId",
                table: "ConversionApplicants",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionApplicants_NationalityId",
                table: "ConversionApplicants",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionApplicants_ProgrameId",
                table: "ConversionApplicants",
                column: "ProgrameId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionApplicants_StateOfOriginId",
                table: "ConversionApplicants",
                column: "StateOfOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionApplicants_YearOfAdmission",
                table: "ConversionApplicants",
                column: "YearOfAdmission");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionCourseRegs_CourseId",
                table: "ConversionCourseRegs",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionCourseRegs_SessionId",
                table: "ConversionCourseRegs",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionCourseRegs_StudentId_CourseId",
                table: "ConversionCourseRegs",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConversionCourses_DepartmentId",
                table: "ConversionCourses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionCourses_Level",
                table: "ConversionCourses",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionCourses_ProgrammeId",
                table: "ConversionCourses",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionCourses_Semester",
                table: "ConversionCourses",
                column: "Semester");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionMainWallets_ApplicantId",
                table: "ConversionMainWallets",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionPrograms_DepartmentId",
                table: "ConversionPrograms",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_ApplicantId",
                table: "ConversionStudents",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_ClearedBy",
                table: "ConversionStudents",
                column: "ClearedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_CurrentSession",
                table: "ConversionStudents",
                column: "CurrentSession");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_Department",
                table: "ConversionStudents",
                column: "Department");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_Faculty",
                table: "ConversionStudents",
                column: "Faculty");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_Level",
                table: "ConversionStudents",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_LGAId",
                table: "ConversionStudents",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_NationalityId",
                table: "ConversionStudents",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_ProgrameId",
                table: "ConversionStudents",
                column: "ProgrameId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_StateOfOriginId",
                table: "ConversionStudents",
                column: "StateOfOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudents_YearOfAdmission",
                table: "ConversionStudents",
                column: "YearOfAdmission");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudentSupervisors_Student",
                table: "ConversionStudentSupervisors",
                column: "Student");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionStudentSupervisors_Supervisor",
                table: "ConversionStudentSupervisors",
                column: "Supervisor");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSubWallets_ApplicantId",
                table: "ConversionSubWallets",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSubWallets_Department",
                table: "ConversionSubWallets",
                column: "Department");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSubWallets_Level",
                table: "ConversionSubWallets",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionSubWallets_SessionId",
                table: "ConversionSubWallets",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PgCourseRegs_CourseId",
                table: "PgCourseRegs",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PgCourseRegs_SessionId",
                table: "PgCourseRegs",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PgCourseRegs_StudentId_CourseId",
                table: "PgCourseRegs",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PgMainWallets_PgApplicants_ApplicantId",
                table: "PgMainWallets",
                column: "ApplicantId",
                principalTable: "PgApplicants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostGraduateStudents_PgApplicants_ApplicantId",
                table: "PostGraduateStudents",
                column: "ApplicantId",
                principalTable: "PgApplicants",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PgMainWallets_PgApplicants_ApplicantId",
                table: "PgMainWallets");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGraduateStudents_PgApplicants_ApplicantId",
                table: "PostGraduateStudents");

            migrationBuilder.DropTable(
                name: "ConversionCourseRegs");

            migrationBuilder.DropTable(
                name: "ConversionMainWallets");

            migrationBuilder.DropTable(
                name: "ConversionStudentSupervisors");

            migrationBuilder.DropTable(
                name: "ConversionSubWallets");

            migrationBuilder.DropTable(
                name: "PgCourseRegs");

            migrationBuilder.DropTable(
                name: "ConversionCourses");

            migrationBuilder.DropTable(
                name: "ConversionStudents");

            migrationBuilder.DropTable(
                name: "ConversionPrograms");

            migrationBuilder.DropTable(
                name: "ConversionApplicants");

            migrationBuilder.AddForeignKey(
                name: "FK_PgMainWallets_UgApplicants_ApplicantId",
                table: "PgMainWallets",
                column: "ApplicantId",
                principalTable: "UgApplicants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostGraduateStudents_UgApplicants_ApplicantId",
                table: "PostGraduateStudents",
                column: "ApplicantId",
                principalTable: "UgApplicants",
                principalColumn: "Id");
        }
    }
}
