using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Models
{
    public partial class NewbridgeContext : DbContext
    {
        public NewbridgeContext()
        {
        }

        public NewbridgeContext(DbContextOptions<NewbridgeContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=MPXNWBLTVS2035\\SQLEXPRESS;Database=eddsMapaex;Trusted_Connection=True");
            }
        }

        public virtual DbSet<CilTask> CilTasks { get; set; } = null!;
        public virtual DbSet<ClTask> ClTasks { get; set; } = null!;
        public virtual DbSet<Component> Components { get; set; } = null!;
        public virtual DbSet<ComponentAction> ComponentActions { get; set; } = null!;
        public virtual DbSet<DailyPlan> DailyPlans { get; set; } = null!;
        public virtual DbSet<DailyPlanCilTask> DailyPlanCilTasks { get; set; } = null!;
        public virtual DbSet<DailyPlanClTask> DailyPlanClTasks { get; set; } = null!;
        public virtual DbSet<DailyPlanDefectTask> DailyPlanDefectTasks { get; set; } = null!;
        public virtual DbSet<DailyPlanOtherTask> DailyPlanOtherTasks { get; set; } = null!;
        public virtual DbSet<DailyPlanPmTask> DailyPlanPmTasks { get; set; } = null!;
        public virtual DbSet<DailyResult> DailyResults { get; set; } = null!;
        public virtual DbSet<DailyTriggerAnswer> DailyTriggerAnswers { get; set; } = null!;
        public virtual DbSet<DailyTriggerQuestion> DailyTriggerQuestions { get; set; } = null!;
        public virtual DbSet<Defect> Defects { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<LineArea> LineAreas { get; set; } = null!;
        public virtual DbSet<Loss> Losses { get; set; } = null!;
        public virtual DbSet<OtherTask> OtherTasks { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<PmTask> PmTasks { get; set; } = null!;
        public virtual DbSet<ProductionLine> ProductionLines { get; set; } = null!;
        public virtual DbSet<ProductionLineDailyTriggerQuestion> ProductionLineDailyTriggerQuestions { get; set; } = null!;
        public virtual DbSet<SafetyZoneTrigger> SafetyZoneTriggers { get; set; } = null!;
        public virtual DbSet<SafetyZoneTriggerAnswer> SafetyZoneTriggerAnswers { get; set; } = null!;
        public virtual DbSet<SafetyZoneTriggerQuestion> SafetyZoneTriggerQuestions { get; set; } = null!;
        public virtual DbSet<SafetyZoneTriggerQuestionDepartment> SafetyZoneTriggerQuestionDepartments { get; set; } = null!;
        public virtual DbSet<Transformation> Transformations { get; set; } = null!;
        public virtual DbSet<ValueStream> ValueStreams { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CilTask>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.LineArea)
                    .WithMany(p => p.CilTasks)
                    .HasForeignKey(d => d.LineAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CilTasks_LineAreas");
            });

            modelBuilder.Entity<ClTask>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.LineArea)
                    .WithMany(p => p.ClTasks)
                    .HasForeignKey(d => d.LineAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClTasks_LineAreas");
            });

            modelBuilder.Entity<Component>(entity =>
            {
                entity.HasIndex(e => e.TransformationId, "IX_Components_TransformationId");

                entity.HasOne(d => d.Transformation)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.TransformationId);
            });

            modelBuilder.Entity<ComponentAction>(entity =>
            {
                entity.HasIndex(e => e.ComponentId, "IX_ComponentActions_ComponentId");

                entity.HasIndex(e => e.LineAreaId, "IX_ComponentActions_LineAreaId");

                entity.HasIndex(e => e.OwnerId, "IX_ComponentActions_OwnerId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.ComponentActions)
                    .HasForeignKey(d => d.ComponentId);

                entity.HasOne(d => d.LineArea)
                    .WithMany(p => p.ComponentActions)
                    .HasForeignKey(d => d.LineAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.ComponentActions)
                    .HasForeignKey(d => d.OwnerId);
            });

            modelBuilder.Entity<DailyPlan>(entity =>
            {
                entity.HasIndex(e => e.ProductionLineId, "IX_DailyPlans_ProductionLineId");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.ProductionLine)
                    .WithMany(p => p.DailyPlans)
                    .HasForeignKey(d => d.ProductionLineId);
            });

            modelBuilder.Entity<DailyPlanCilTask>(entity =>
            {
                entity.HasOne(d => d.DailyPlan)
                    .WithMany(p => p.DailyPlanCilTasks)
                    .HasForeignKey(d => d.DailyPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlanCilTasks_DailyPlans");

                entity.HasOne(d => d.LinkedTask)
                    .WithMany(p => p.DailyPlanCilTasks)
                    .HasForeignKey(d => d.LinkedTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlanCilTasks_CilTasks");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.DailyPlanCilTasks)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_DailyPlanCilTasks_People");
            });

            modelBuilder.Entity<DailyPlanClTask>(entity =>
            {
                entity.HasOne(d => d.DailyPlan)
                    .WithMany(p => p.DailyPlanClTasks)
                    .HasForeignKey(d => d.DailyPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlanClTasks_DailyPlans");

                entity.HasOne(d => d.LinkedTask)
                    .WithMany(p => p.DailyPlanClTasks)
                    .HasForeignKey(d => d.LinkedTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlanClTasks_ClTasks");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.DailyPlanClTasks)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_DailyPlanClTasks_People");
            });

            modelBuilder.Entity<DailyPlanDefectTask>(entity =>
            {
                entity.HasIndex(e => e.DailyPlanId, "IX_DailyPlanDefectTasks_DailyPlanId");

                entity.HasIndex(e => e.LinkedTaskId, "IX_DailyPlanDefectTasks_LinkedTaskId");

                entity.HasIndex(e => e.OwnerId, "IX_DailyPlanDefectTasks_OwnerId");

                entity.HasOne(d => d.DailyPlan)
                    .WithMany(p => p.DailyPlanDefectTasks)
                    .HasForeignKey(d => d.DailyPlanId);

                entity.HasOne(d => d.LinkedTask)
                    .WithMany(p => p.DailyPlanDefectTasks)
                    .HasForeignKey(d => d.LinkedTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.DailyPlanDefectTasks)
                    .HasForeignKey(d => d.OwnerId);
            });

            modelBuilder.Entity<DailyPlanOtherTask>(entity =>
            {
                entity.HasOne(d => d.DailyPlan)
                    .WithMany(p => p.DailyPlanOtherTasks)
                    .HasForeignKey(d => d.DailyPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlanOtherTasks_DailyPlans");

                entity.HasOne(d => d.LinkedTask)
                    .WithMany(p => p.DailyPlanOtherTasks)
                    .HasForeignKey(d => d.LinkedTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlanOtherTasks_OtherTasks");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.DailyPlanOtherTasks)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_DailyPlanOtherTasks_People");
            });

            modelBuilder.Entity<DailyPlanPmTask>(entity =>
            {
                entity.HasOne(d => d.DailyPlan)
                    .WithMany(p => p.DailyPlanPmTasks)
                    .HasForeignKey(d => d.DailyPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlanPmTasks_DailyPlans");

                entity.HasOne(d => d.LinkedTask)
                    .WithMany(p => p.DailyPlanPmTasks)
                    .HasForeignKey(d => d.LinkedTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyPlanPmTasks_PmTasks");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.DailyPlanPmTasks)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_DailyPlanPmTasks_People");
            });

            modelBuilder.Entity<DailyResult>(entity =>
            {
                entity.ToTable("DailyResult");

                entity.Property(e => e.CoDay).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CoEve).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CoNight)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("CoNIght");

                entity.Property(e => e.CommentDay).HasColumnType("text");

                entity.Property(e => e.CommentEve).HasColumnType("text");

                entity.Property(e => e.CommentNight).HasColumnType("text");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.OutputDay).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.OutputEve).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.OutputNight).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PdtDay).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PdtEve).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PdtNight).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PrDay).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PrEve).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PrNight).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StaffingDay).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StaffingEve).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.StaffingNight).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpdtDay).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpdtEve).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpdtNight).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.WasteDay).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.WasteEve).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.WasteNight).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.ProductionLine)
                    .WithMany(p => p.DailyResults)
                    .HasForeignKey(d => d.ProductionLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyResult_ProductionLines");
            });

            modelBuilder.Entity<DailyTriggerAnswer>(entity =>
            {
                entity.ToTable("DailyTriggerAnswer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.ProductionLine)
                    .WithMany(p => p.DailyTriggerAnswers)
                    .HasForeignKey(d => d.ProductionLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DailyTriggerAnswer_ProductionLines");
            });

            modelBuilder.Entity<DailyTriggerQuestion>(entity =>
            {
                entity.ToTable("DailyTriggerQuestion");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Defect>(entity =>
            {
                entity.HasIndex(e => e.LineAreaId, "IX_Defects_LineAreaId");

                entity.HasIndex(e => e.OwnerId, "IX_Defects_OwnerId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.LineArea)
                    .WithMany(p => p.Defects)
                    .HasForeignKey(d => d.LineAreaId);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Defects)
                    .HasForeignKey(d => d.OwnerId);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.ValueStreamId, "IX_Departments_ValueStreamId");

                entity.HasOne(d => d.ValueStream)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ValueStreamId);

                entity.HasMany(d => d.People)
                    .WithMany(p => p.Departments)
                    .UsingEntity<Dictionary<string, object>>(
                        "DepartmentPerson",
                        l => l.HasOne<Person>().WithMany().HasForeignKey("PeopleId"),
                        r => r.HasOne<Department>().WithMany().HasForeignKey("DepartmentsId"),
                        j =>
                        {
                            j.HasKey("DepartmentsId", "PeopleId");

                            j.ToTable("DepartmentPerson");

                            j.HasIndex(new[] { "PeopleId" }, "IX_DepartmentPerson_PeopleId");
                        });
            });

            modelBuilder.Entity<LineArea>(entity =>
            {
                entity.HasIndex(e => e.ProductionLineId, "IX_LineAreas_ProductionLineId");

                entity.HasOne(d => d.ProductionLine)
                    .WithMany(p => p.LineAreas)
                    .HasForeignKey(d => d.ProductionLineId);
            });

            modelBuilder.Entity<Loss>(entity =>
            {
                entity.HasIndex(e => e.LineAreaId, "IX_Losses_LineAreaId");

                entity.HasIndex(e => e.OwnerId, "IX_Losses_OwnerId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.LineArea)
                    .WithMany(p => p.Losses)
                    .HasForeignKey(d => d.LineAreaId);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Losses)
                    .HasForeignKey(d => d.OwnerId);
            });

            modelBuilder.Entity<OtherTask>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.LineArea)
                    .WithMany(p => p.OtherTasks)
                    .HasForeignKey(d => d.LineAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OtherTasks_LineAreas");
            });

            modelBuilder.Entity<PmTask>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.LineArea)
                    .WithMany(p => p.PmTasks)
                    .HasForeignKey(d => d.LineAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PmTasks_LineAreas");
            });

            modelBuilder.Entity<ProductionLine>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId, "IX_ProductionLines_DepartmentId");

                entity.Property(e => e.TargetCo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TargetPdt).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TargetPr).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TargetUpdt).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TargetWaste).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.ProductionLines)
                    .HasForeignKey(d => d.DepartmentId);
            });

            modelBuilder.Entity<ProductionLineDailyTriggerQuestion>(entity =>
            {
                entity.ToTable("ProductionLine_DailyTriggerQuestion");

                entity.HasOne(d => d.DailyTriggerQuestion)
                    .WithMany(p => p.ProductionLineDailyTriggerQuestions)
                    .HasForeignKey(d => d.DailyTriggerQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductionLine_DailyTriggerQuestion_DailyTriggerQuestion");

                entity.HasOne(d => d.ProductionLine)
                    .WithMany(p => p.ProductionLineDailyTriggerQuestions)
                    .HasForeignKey(d => d.ProductionLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductionLine_DailyTriggerQuestion_ProductionLines");
            });

            modelBuilder.Entity<SafetyZoneTrigger>(entity =>
            {
                entity.ToTable("SafetyZoneTrigger");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SafetyZoneTriggers)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SafetyZoneTrigger_Departments_null_fk");
            });

            modelBuilder.Entity<SafetyZoneTriggerAnswer>(entity =>
            {
                entity.ToTable("SafetyZoneTriggerAnswer");

                entity.HasOne(d => d.SafetyZoneTrigger)
                    .WithMany(p => p.SafetyZoneTriggerAnswers)
                    .HasForeignKey(d => d.SafetyZoneTriggerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SafetyZoneTriggerAnswer_SafetyZoneTrigger_null_fk");
            });

            modelBuilder.Entity<SafetyZoneTriggerQuestion>(entity =>
            {
                entity.ToTable("SafetyZoneTriggerQuestion");
            });

            modelBuilder.Entity<SafetyZoneTriggerQuestionDepartment>(entity =>
            {
                entity.ToTable("SafetyZoneTriggerQuestion_Department");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SafetyZoneTriggerQuestionDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SafetyZoneTriggerQuestion_Department_Departments_null_fk");

                entity.HasOne(d => d.SafetyZoneTriggerQuestion)
                    .WithMany(p => p.SafetyZoneTriggerQuestionDepartments)
                    .HasForeignKey(d => d.SafetyZoneTriggerQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SafetyZoneTriggerQuestion_Department_SafetyZoneTriggerQuestion_null_fk");
            });

            modelBuilder.Entity<Transformation>(entity =>
            {
                entity.HasIndex(e => e.LineAreaId, "IX_Transformations_LineAreaId");

                entity.HasOne(d => d.LineArea)
                    .WithMany(p => p.Transformations)
                    .HasForeignKey(d => d.LineAreaId);
            });

            modelBuilder.Entity<ValueStream>(entity =>
            {
                entity.HasIndex(e => e.PlantId, "IX_ValueStreams_PlantId");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.ValueStreams)
                    .HasForeignKey(d => d.PlantId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
