using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyTriggerQuestion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TargetInt = table.Column<int>(type: "int", nullable: false),
                    Field = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTriggerQuestion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRealPerson = table.Column<bool>(type: "bit", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SafetyZoneTriggerQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyZoneTriggerQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValueStreams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueStreamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueStreams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueStreams_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueStreamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_ValueStreams_ValueStreamId",
                        column: x => x.ValueStreamId,
                        principalTable: "ValueStreams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPerson",
                columns: table => new
                {
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    PeopleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPerson", x => new { x.DepartmentsId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_DepartmentPerson_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentPerson_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProficyLine = table.Column<int>(type: "int", nullable: true),
                    ProficyUnit = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsProficyBased = table.Column<int>(type: "int", nullable: true),
                    FamilyId = table.Column<int>(type: "int", nullable: true),
                    TargetPr = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    TargetCo = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    TargetUpdt = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    TargetPdt = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    TargetWaste = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionLines_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyZoneTrigger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyZoneTrigger", x => x.Id);
                    table.ForeignKey(
                        name: "SafetyZoneTrigger_Departments_null_fk",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SafetyZoneTriggerQuestion_Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    SafetyZoneTriggerQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyZoneTriggerQuestion_Department", x => x.Id);
                    table.ForeignKey(
                        name: "SafetyZoneTriggerQuestion_Department_Departments_null_fk",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "SafetyZoneTriggerQuestion_Department_SafetyZoneTriggerQuestion_null_fk",
                        column: x => x.SafetyZoneTriggerQuestionId,
                        principalTable: "SafetyZoneTriggerQuestion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductionLineId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyPlanTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlans_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionLineId = table.Column<int>(type: "int", nullable: false),
                    OutputDay = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    OutputEve = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    OutputNight = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    StaffingDay = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    StaffingEve = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    StaffingNight = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    PrDay = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    PrEve = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    PrNight = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    UpdtDay = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    UpdtEve = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    UpdtNight = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    PdtDay = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    PdtEve = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    PdtNight = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CoDay = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CoEve = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CoNIght = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    StopsDay = table.Column<int>(type: "int", nullable: true),
                    StopsEve = table.Column<int>(type: "int", nullable: true),
                    StopsNight = table.Column<int>(type: "int", nullable: true),
                    WasteDay = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    WasteEve = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    WasteNight = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    CommentDay = table.Column<string>(type: "text", nullable: true),
                    CommentEve = table.Column<string>(type: "text", nullable: true),
                    CommentNight = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyResult_ProductionLines",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyTriggerAnswer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HintText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    TargetInt = table.Column<int>(type: "int", nullable: true),
                    ProductionLineId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    AnswerTextDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerTextEve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerTextNight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerIntDay = table.Column<int>(type: "int", nullable: true),
                    AnswerIntEve = table.Column<int>(type: "int", nullable: true),
                    AnswerIntNight = table.Column<int>(type: "int", nullable: true),
                    AnswerTriggerDay = table.Column<int>(type: "int", nullable: true),
                    AnswerTriggerEve = table.Column<int>(type: "int", nullable: true),
                    AnswerTriggerNight = table.Column<int>(type: "int", nullable: true),
                    Field = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTriggerAnswer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DailyTriggerAnswer_ProductionLines",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LineAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionLineId = table.Column<int>(type: "int", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineAreas_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLine_DailyTriggerQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionLineId = table.Column<int>(type: "int", nullable: false),
                    DailyTriggerQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLine_DailyTriggerQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionLine_DailyTriggerQuestion_DailyTriggerQuestion",
                        column: x => x.DailyTriggerQuestionId,
                        principalTable: "DailyTriggerQuestion",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductionLine_DailyTriggerQuestion_ProductionLines",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SafetyZoneTriggerAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<bool>(type: "bit", nullable: false),
                    SafetyZoneTriggerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyZoneTriggerAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "SafetyZoneTriggerAnswer_SafetyZoneTrigger_null_fk",
                        column: x => x.SafetyZoneTriggerId,
                        principalTable: "SafetyZoneTrigger",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CilTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LineAreaId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CilTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CilTasks_LineAreas",
                        column: x => x.LineAreaId,
                        principalTable: "LineAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LineAreaId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClTasks_LineAreas",
                        column: x => x.LineAreaId,
                        principalTable: "LineAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubTypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LineAreaId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    FoundDuringCil = table.Column<bool>(type: "bit", nullable: false),
                    PmHelpNeeded = table.Column<bool>(type: "bit", nullable: false),
                    PmHelpText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Defects_LineAreas_LineAreaId",
                        column: x => x.LineAreaId,
                        principalTable: "LineAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Defects_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Losses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubTypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LineAreaId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Losses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Losses_LineAreas_LineAreaId",
                        column: x => x.LineAreaId,
                        principalTable: "LineAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Losses_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OtherTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LineAreaId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherTasks_LineAreas",
                        column: x => x.LineAreaId,
                        principalTable: "LineAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PmTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LineAreaId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PmTasks_LineAreas",
                        column: x => x.LineAreaId,
                        principalTable: "LineAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineAreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transformations_LineAreas_LineAreaId",
                        column: x => x.LineAreaId,
                        principalTable: "LineAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyPlanCilTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    TimingComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskLevel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DailyPlanId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlanCilTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlanCilTasks_CilTasks",
                        column: x => x.LinkedTaskId,
                        principalTable: "CilTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanCilTasks_DailyPlans",
                        column: x => x.DailyPlanId,
                        principalTable: "DailyPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanCilTasks_People",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyPlanClTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    TimingComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskLevel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DailyPlanId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlanClTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlanClTasks_ClTasks",
                        column: x => x.LinkedTaskId,
                        principalTable: "ClTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanClTasks_DailyPlans",
                        column: x => x.DailyPlanId,
                        principalTable: "DailyPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanClTasks_People",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyPlanDefectTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    TimingComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskLevel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DailyPlanId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlanDefectTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlanDefectTasks_DailyPlans_DailyPlanId",
                        column: x => x.DailyPlanId,
                        principalTable: "DailyPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyPlanDefectTasks_Defects_LinkedTaskId",
                        column: x => x.LinkedTaskId,
                        principalTable: "Defects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanDefectTasks_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyPlanOtherTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    TimingComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskLevel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DailyPlanId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlanOtherTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlanOtherTasks_DailyPlans",
                        column: x => x.DailyPlanId,
                        principalTable: "DailyPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanOtherTasks_OtherTasks",
                        column: x => x.LinkedTaskId,
                        principalTable: "OtherTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanOtherTasks_People",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyPlanPmTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    TimingComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskLevel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DailyPlanId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlanPmTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlanPmTasks_DailyPlans",
                        column: x => x.DailyPlanId,
                        principalTable: "DailyPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanPmTasks_People",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyPlanPmTasks_PmTasks",
                        column: x => x.LinkedTaskId,
                        principalTable: "PmTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SapNumber = table.Column<int>(type: "int", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TransformationId = table.Column<int>(type: "int", nullable: false),
                    PmRequired = table.Column<bool>(type: "bit", nullable: false),
                    AmRequired = table.Column<bool>(type: "bit", nullable: false),
                    PmInPlace = table.Column<bool>(type: "bit", nullable: false),
                    AmInPlace = table.Column<bool>(type: "bit", nullable: false),
                    WearComponent = table.Column<bool>(type: "bit", nullable: false),
                    SpareRequired = table.Column<bool>(type: "bit", nullable: false),
                    SpareAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ClRequired = table.Column<bool>(type: "bit", nullable: false),
                    ClInPlace = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Transformations_TransformationId",
                        column: x => x.TransformationId,
                        principalTable: "Transformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubTypeId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LineAreaId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentActions_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentActions_LineAreas_LineAreaId",
                        column: x => x.LineAreaId,
                        principalTable: "LineAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentActions_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CilTasks_LineAreaId",
                table: "CilTasks",
                column: "LineAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClTasks_LineAreaId",
                table: "ClTasks",
                column: "LineAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentActions_ComponentId",
                table: "ComponentActions",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentActions_LineAreaId",
                table: "ComponentActions",
                column: "LineAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentActions_OwnerId",
                table: "ComponentActions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_TransformationId",
                table: "Components",
                column: "TransformationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanCilTasks_DailyPlanId",
                table: "DailyPlanCilTasks",
                column: "DailyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanCilTasks_LinkedTaskId",
                table: "DailyPlanCilTasks",
                column: "LinkedTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanCilTasks_OwnerId",
                table: "DailyPlanCilTasks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanClTasks_DailyPlanId",
                table: "DailyPlanClTasks",
                column: "DailyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanClTasks_LinkedTaskId",
                table: "DailyPlanClTasks",
                column: "LinkedTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanClTasks_OwnerId",
                table: "DailyPlanClTasks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanDefectTasks_DailyPlanId",
                table: "DailyPlanDefectTasks",
                column: "DailyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanDefectTasks_LinkedTaskId",
                table: "DailyPlanDefectTasks",
                column: "LinkedTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanDefectTasks_OwnerId",
                table: "DailyPlanDefectTasks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanOtherTasks_DailyPlanId",
                table: "DailyPlanOtherTasks",
                column: "DailyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanOtherTasks_LinkedTaskId",
                table: "DailyPlanOtherTasks",
                column: "LinkedTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanOtherTasks_OwnerId",
                table: "DailyPlanOtherTasks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanPmTasks_DailyPlanId",
                table: "DailyPlanPmTasks",
                column: "DailyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanPmTasks_LinkedTaskId",
                table: "DailyPlanPmTasks",
                column: "LinkedTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlanPmTasks_OwnerId",
                table: "DailyPlanPmTasks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlans_ProductionLineId",
                table: "DailyPlans",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyResult_ProductionLineId",
                table: "DailyResult",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTriggerAnswer_ProductionLineId",
                table: "DailyTriggerAnswer",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_LineAreaId",
                table: "Defects",
                column: "LineAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_OwnerId",
                table: "Defects",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPerson_PeopleId",
                table: "DepartmentPerson",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ValueStreamId",
                table: "Departments",
                column: "ValueStreamId");

            migrationBuilder.CreateIndex(
                name: "IX_LineAreas_ProductionLineId",
                table: "LineAreas",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Losses_LineAreaId",
                table: "Losses",
                column: "LineAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Losses_OwnerId",
                table: "Losses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherTasks_LineAreaId",
                table: "OtherTasks",
                column: "LineAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PmTasks_LineAreaId",
                table: "PmTasks",
                column: "LineAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_DailyTriggerQuestion_DailyTriggerQuestionId",
                table: "ProductionLine_DailyTriggerQuestion",
                column: "DailyTriggerQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_DailyTriggerQuestion_ProductionLineId",
                table: "ProductionLine_DailyTriggerQuestion",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLines_DepartmentId",
                table: "ProductionLines",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyZoneTrigger_DepartmentId",
                table: "SafetyZoneTrigger",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyZoneTriggerAnswer_SafetyZoneTriggerId",
                table: "SafetyZoneTriggerAnswer",
                column: "SafetyZoneTriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyZoneTriggerQuestion_Department_DepartmentId",
                table: "SafetyZoneTriggerQuestion_Department",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyZoneTriggerQuestion_Department_SafetyZoneTriggerQuestionId",
                table: "SafetyZoneTriggerQuestion_Department",
                column: "SafetyZoneTriggerQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transformations_LineAreaId",
                table: "Transformations",
                column: "LineAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueStreams_PlantId",
                table: "ValueStreams",
                column: "PlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentActions");

            migrationBuilder.DropTable(
                name: "DailyPlanCilTasks");

            migrationBuilder.DropTable(
                name: "DailyPlanClTasks");

            migrationBuilder.DropTable(
                name: "DailyPlanDefectTasks");

            migrationBuilder.DropTable(
                name: "DailyPlanOtherTasks");

            migrationBuilder.DropTable(
                name: "DailyPlanPmTasks");

            migrationBuilder.DropTable(
                name: "DailyResult");

            migrationBuilder.DropTable(
                name: "DailyTriggerAnswer");

            migrationBuilder.DropTable(
                name: "DepartmentPerson");

            migrationBuilder.DropTable(
                name: "Losses");

            migrationBuilder.DropTable(
                name: "ProductionLine_DailyTriggerQuestion");

            migrationBuilder.DropTable(
                name: "SafetyZoneTriggerAnswer");

            migrationBuilder.DropTable(
                name: "SafetyZoneTriggerQuestion_Department");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "CilTasks");

            migrationBuilder.DropTable(
                name: "ClTasks");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "OtherTasks");

            migrationBuilder.DropTable(
                name: "DailyPlans");

            migrationBuilder.DropTable(
                name: "PmTasks");

            migrationBuilder.DropTable(
                name: "DailyTriggerQuestion");

            migrationBuilder.DropTable(
                name: "SafetyZoneTrigger");

            migrationBuilder.DropTable(
                name: "SafetyZoneTriggerQuestion");

            migrationBuilder.DropTable(
                name: "Transformations");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "LineAreas");

            migrationBuilder.DropTable(
                name: "ProductionLines");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "ValueStreams");

            migrationBuilder.DropTable(
                name: "Plants");
        }
    }
}
