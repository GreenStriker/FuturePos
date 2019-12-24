using Microsoft.EntityFrameworkCore;

namespace vms.entity.models
{
    public partial class VmsContext : DbContext
    {
        public VmsContext()
        {
        }

        public VmsContext(DbContextOptions<VmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<AuditOperation> AuditOperations { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankBranch> BankBranches { get; set; }
        public virtual DbSet<BillOfMaterial> BillOfMaterials { get; set; }
        public virtual DbSet<BusinessNature> BusinessNatures { get; set; }
        public virtual DbSet<Coagroup> Coagroups { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<CreditNote> CreditNotes { get; set; }
        public virtual DbSet<CreditNoteDetail> CreditNoteDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Damage> Damages { get; set; }
        public virtual DbSet<DamageType> DamageTypes { get; set; }
        public virtual DbSet<DebitNote> DebitNotes { get; set; }
        public virtual DbSet<DebitNoteDetail> DebitNoteDetails { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<ExportType> ExportTypes { get; set; }
        public virtual DbSet<FinancialActivityNature> FinancialActivityNatures { get; set; }
        public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public virtual DbSet<MushakGeneration> MushakGenerations { get; set; }
        public virtual DbSet<MushakGenerationStage> MushakGenerationStages { get; set; }
        public virtual DbSet<NbrEconomicCode> NbrEconomicCodes { get; set; }
        public virtual DbSet<NbrEconomicCodeType> NbrEconomicCodeTypes { get; set; }
        public virtual DbSet<ObjectType> ObjectTypes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OverHeadCost> OverHeadCosts { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PriceSetup> PriceSetups { get; set; }
        public virtual DbSet<PriceSetupProductCost> PriceSetupProductCosts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<ProductProductTypeMapping> ProductProductTypeMappings { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<ProductVat> ProductVats { get; set; }
        public virtual DbSet<ProductVattype> ProductVattypes { get; set; }
        public virtual DbSet<Production> Productions { get; set; }
        public virtual DbSet<ProductionDetail> ProductionDetails { get; set; }
        public virtual DbSet<ProductionReceive> ProductionReceives { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual DbSet<PurchasePayment> PurchasePayments { get; set; }
        public virtual DbSet<PurchaseReason> PurchaseReasons { get; set; }
        public virtual DbSet<PurchaseType> PurchaseTypes { get; set; }
        public virtual DbSet<Right> Rights { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleRight> RoleRights { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SalesDeliveryType> SalesDeliveryTypes { get; set; }
        public virtual DbSet<SalesDetail> SalesDetails { get; set; }
        public virtual DbSet<SalesPaymentReceive> SalesPaymentReceives { get; set; }
        public virtual DbSet<SalesType> SalesTypes { get; set; }
        public virtual DbSet<StockIn> StockIns { get; set; }
        public virtual DbSet<SupplimentaryDuty> SupplimentaryDuties { get; set; }
        public virtual DbSet<TransectionType> TransectionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Descriptions)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.HasOne(d => d.AuditOperation)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.AuditOperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditLog_AuditOperation");

                entity.HasOne(d => d.ObjectType)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.ObjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditLog_ObjectType");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_AuditLog_Organizations");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditLog_Users");
            });

            modelBuilder.Entity<AuditOperation>(entity =>
            {
                entity.ToTable("AuditOperation");

                entity.Property(e => e.AuditOperationId).HasColumnName("AuditOperationID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(500);
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NameInBangla)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<BankBranch>(entity =>
            {
                entity.ToTable("BankBranch");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NameInBangla)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankBranches)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankBranch_Bank");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.BankBranches)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankBranch_District");
            });

            modelBuilder.Entity<BillOfMaterial>(entity =>
            {
                entity.ToTable("BillOfMaterial");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.UsedQuantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.BillOfMaterials)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillOfMaterial_MeasurementUnits");

                entity.HasOne(d => d.ProductionReceive)
                    .WithMany(p => p.BillOfMaterials)
                    .HasForeignKey(d => d.ProductionReceiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillOfMaterial_ProductionReceive");

                entity.HasOne(d => d.RawMaterial)
                    .WithMany(p => p.BillOfMaterials)
                    .HasForeignKey(d => d.RawMaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillOfMaterial_Products");

                entity.HasOne(d => d.StockIn)
                    .WithMany(p => p.BillOfMaterials)
                    .HasForeignKey(d => d.StockInId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillOfMaterial_StockIn");
            });

            modelBuilder.Entity<BusinessNature>(entity =>
            {
                entity.ToTable("BusinessNature");

                entity.Property(e => e.BusinessNatureId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameInBangla)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Coagroup>(entity =>
            {
                entity.ToTable("COAGroups", "acc");

                entity.Property(e => e.CoagroupId).HasColumnName("COAGroupId");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Node).HasMaxLength(50);
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("Content");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.FileUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.MimeType).HasMaxLength(50);

                entity.Property(e => e.Node).HasMaxLength(500);

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Content_DocumentType");
            });

            modelBuilder.Entity<CreditNote>(entity =>
            {
                entity.ToTable("CreditNote");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.ReasonOfReturn).HasMaxLength(500);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.HasOne(d => d.Sales)
                    .WithMany(p => p.CreditNotes)
                    .HasForeignKey(d => d.SalesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditNote_Sales");
            });

            modelBuilder.Entity<CreditNoteDetail>(entity =>
            {
                entity.ToTable("CreditNoteDetail");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.ReturnQuantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.CreditNote)
                    .WithMany(p => p.CreditNoteDetails)
                    .HasForeignKey(d => d.CreditNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditNoteDetail_CreditNote");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.CreditNoteDetails)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditNoteDetail_MeasurementUnits");

                entity.HasOne(d => d.SalesDetail)
                    .WithMany(p => p.CreditNoteDetails)
                    .HasForeignKey(d => d.SalesDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditNoteDetail_SalesDetails");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Bin)
                    .HasColumnName("BIN")
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Nidno)
                    .HasColumnName("NIDNo")
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Customer_Organizations");
            });

            modelBuilder.Entity<Damage>(entity =>
            {
                entity.ToTable("Damage");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DamageQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.DamageType)
                    .WithMany(p => p.Damages)
                    .HasForeignKey(d => d.DamageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Damage_DamageType");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Damages)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Damage_Organizations");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Damages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Damage_Products");

                entity.HasOne(d => d.StockIn)
                    .WithMany(p => p.Damages)
                    .HasForeignKey(d => d.StockInId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Damage_StockIn");
            });

            modelBuilder.Entity<DamageType>(entity =>
            {
                entity.ToTable("DamageType");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.DamageTypes)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_DamageType_Organizations");
            });

            modelBuilder.Entity<DebitNote>(entity =>
            {
                entity.ToTable("DebitNote");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.ReasonOfReturn).HasMaxLength(500);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.DebitNotes)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DebitNote_Purchase");
            });

            modelBuilder.Entity<DebitNoteDetail>(entity =>
            {
                entity.ToTable("DebitNoteDetail");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.ReturnQuantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.DebitNote)
                    .WithMany(p => p.DebitNoteDetails)
                    .HasForeignKey(d => d.DebitNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DebitNoteDetail_DebitNote");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.DebitNoteDetails)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DebitNoteDetail_MeasurementUnits");

                entity.HasOne(d => d.PurchaseDetail)
                    .WithMany(p => p.DebitNoteDetails)
                    .HasForeignKey(d => d.PurchaseDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DebitNoteDetail_PurchaseDetails");
            });

            modelBuilder.Entity<DeliveryMethod>(entity =>
            {
                entity.ToTable("DeliveryMethod");

                entity.Property(e => e.DeliveryMethodId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NameInBangla)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentType");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.DocumentTypes)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_DocumentType_Organizations");
            });

            modelBuilder.Entity<ExportType>(entity =>
            {
                entity.ToTable("ExportType");

                entity.Property(e => e.ExportTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.ExportTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FinancialActivityNature>(entity =>
            {
                entity.ToTable("FinancialActivityNature");

                entity.Property(e => e.FinancialActivityNatureId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameInBangla)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MeasurementUnit>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MushakGeneration>(entity =>
            {
                entity.ToTable("MushakGeneration");

                entity.Property(e => e.AmountForSuppDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AmountForVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DevelopmentSurcharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DevelopmentSurchargeChallanNo).HasMaxLength(20);

                entity.Property(e => e.DevelopmentSurchargePaymentDate).HasColumnType("datetime");

                entity.Property(e => e.EnvironmentProtectSurcharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EnvironmentProtectSurchargeChallanNo).HasMaxLength(20);

                entity.Property(e => e.EnvironmentProtectSurchargePaymentDate).HasColumnType("datetime");

                entity.Property(e => e.ExciseDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExciseDutyChallanNo).HasMaxLength(20);

                entity.Property(e => e.ExciseDutyPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.FinancialPenalty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FinancialPenaltyChallanNo).HasMaxLength(20);

                entity.Property(e => e.FinancialPenaltyPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.GenerateDate).HasColumnType("datetime");

                entity.Property(e => e.HealthDevelopmentSurcharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HealthDevelopmentSurchargeChallanNo).HasMaxLength(20);

                entity.Property(e => e.HealthDevelopmentSurchargePaymentDate).HasColumnType("datetime");

                entity.Property(e => e.InterestForDueSuppDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InterestForDueSuppDutyChallanNo).HasMaxLength(20);

                entity.Property(e => e.InterestForDueSuppDutyPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.InterestForDueVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InterestForDueVatChallanNo).HasMaxLength(20);

                entity.Property(e => e.InterestForDueVatPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.ItDevelopmentSurcharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ItDevelopmentSurchargeChallanNo).HasMaxLength(20);

                entity.Property(e => e.ItDevelopmentSurchargePaymentDate).HasColumnType("datetime");

                entity.Property(e => e.LastClosingSuppDutyAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LastClosingVatAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidDevelopmentSurcharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidEnvironmentProtectSurcharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidExciseDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidFinancialPenalty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidHealthDevelopmentSurcharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidInterestAmountForDueSuppDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidInterestAmountForDueVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidItDevelopmentSurcharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidSuppDutyAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidVatAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReturnAmountFromClosingSd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReturnAmountFromClosingVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReturnFromClosingSdChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnFromClosingSdChequeNo).HasMaxLength(50);

                entity.Property(e => e.ReturnFromClosingVatChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnFromClosingVatChequeNo).HasMaxLength(50);

                entity.Property(e => e.SubmissionDate).HasColumnType("datetime");

                entity.Property(e => e.SuppDutyChallanNo).HasMaxLength(20);

                entity.Property(e => e.SuppDutyPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.VatPaymentChallanNo).HasMaxLength(20);

                entity.Property(e => e.VatPaymentDate).HasColumnType("datetime");

                entity.HasOne(d => d.DevelopmentSurchargeBankBranch)
                    .WithMany(p => p.MushakGenerationDevelopmentSurchargeBankBranches)
                    .HasForeignKey(d => d.DevelopmentSurchargeBankBranchId);

                entity.HasOne(d => d.DevelopmentSurchargeEconomicCode)
                    .WithMany(p => p.MushakGenerationDevelopmentSurchargeEconomicCodes)
                    .HasForeignKey(d => d.DevelopmentSurchargeEconomicCodeId);

                entity.HasOne(d => d.EnvironmentProtectSurchargeBankBranch)
                    .WithMany(p => p.MushakGenerationEnvironmentProtectSurchargeBankBranches)
                    .HasForeignKey(d => d.EnvironmentProtectSurchargeBankBranchId);

                entity.HasOne(d => d.EnvironmentProtectSurchargeEconomicCode)
                    .WithMany(p => p.MushakGenerationEnvironmentProtectSurchargeEconomicCodes)
                    .HasForeignKey(d => d.EnvironmentProtectSurchargeEconomicCodeId);

                entity.HasOne(d => d.ExciseDutyBankBranch)
                    .WithMany(p => p.MushakGenerationExciseDutyBankBranches)
                    .HasForeignKey(d => d.ExciseDutyBankBranchId);

                entity.HasOne(d => d.ExciseDutyEconomicCode)
                    .WithMany(p => p.MushakGenerationExciseDutyEconomicCodes)
                    .HasForeignKey(d => d.ExciseDutyEconomicCodeId);

                entity.HasOne(d => d.FinancialPenaltyBankBranch)
                    .WithMany(p => p.MushakGenerationFinancialPenaltyBankBranches)
                    .HasForeignKey(d => d.FinancialPenaltyBankBranchId);

                entity.HasOne(d => d.FinancialPenaltyEconomicCode)
                    .WithMany(p => p.MushakGenerationFinancialPenaltyEconomicCodes)
                    .HasForeignKey(d => d.FinancialPenaltyEconomicCodeId);

                entity.HasOne(d => d.HealthDevelopmentSurchargeBankBranch)
                    .WithMany(p => p.MushakGenerationHealthDevelopmentSurchargeBankBranches)
                    .HasForeignKey(d => d.HealthDevelopmentSurchargeBankBranchId);

                entity.HasOne(d => d.HealthDevelopmentSurchargeEconomicCode)
                    .WithMany(p => p.MushakGenerationHealthDevelopmentSurchargeEconomicCodes)
                    .HasForeignKey(d => d.HealthDevelopmentSurchargeEconomicCodeId);

                entity.HasOne(d => d.InterestForDueSuppDutyBankBranch)
                    .WithMany(p => p.MushakGenerationInterestForDueSuppDutyBankBranches)
                    .HasForeignKey(d => d.InterestForDueSuppDutyBankBranchId);

                entity.HasOne(d => d.InterestForDueSuppDutyEconomicCode)
                    .WithMany(p => p.MushakGenerationInterestForDueSuppDutyEconomicCodes)
                    .HasForeignKey(d => d.InterestForDueSuppDutyEconomicCodeId);

                entity.HasOne(d => d.InterestForDueVatBankBranch)
                    .WithMany(p => p.MushakGenerationInterestForDueVatBankBranches)
                    .HasForeignKey(d => d.InterestForDueVatBankBranchId);

                entity.HasOne(d => d.InterestForDueVatEconomicCode)
                    .WithMany(p => p.MushakGenerationInterestForDueVatEconomicCodes)
                    .HasForeignKey(d => d.InterestForDueVatEconomicCodeId);

                entity.HasOne(d => d.ItDevelopmentSurchargeBankBranch)
                    .WithMany(p => p.MushakGenerationItDevelopmentSurchargeBankBranches)
                    .HasForeignKey(d => d.ItDevelopmentSurchargeBankBranchId);

                entity.HasOne(d => d.ItDevelopmentSurchargeEconomicCode)
                    .WithMany(p => p.MushakGenerationItDevelopmentSurchargeEconomicCodes)
                    .HasForeignKey(d => d.ItDevelopmentSurchargeEconomicCodeId);

                entity.HasOne(d => d.MushakGenerationStage)
                    .WithMany(p => p.MushakGenerations)
                    .HasForeignKey(d => d.MushakGenerationStageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MushakGeneration_MushakGenerationStage");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.MushakGenerations)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MushakGeneration_Organizations");

                entity.HasOne(d => d.ReturnFromClosingSdChequeBank)
                    .WithMany(p => p.MushakGenerationReturnFromClosingSdChequeBanks)
                    .HasForeignKey(d => d.ReturnFromClosingSdChequeBankId);

                entity.HasOne(d => d.ReturnFromClosingVatChequeBank)
                    .WithMany(p => p.MushakGenerationReturnFromClosingVatChequeBanks)
                    .HasForeignKey(d => d.ReturnFromClosingVatChequeBankId);

                entity.HasOne(d => d.SuppDutyBankBranch)
                    .WithMany(p => p.MushakGenerationSuppDutyBankBranches)
                    .HasForeignKey(d => d.SuppDutyBankBranchId);

                entity.HasOne(d => d.SuppDutyEconomicCode)
                    .WithMany(p => p.MushakGenerationSuppDutyEconomicCodes)
                    .HasForeignKey(d => d.SuppDutyEconomicCodeId);

                entity.HasOne(d => d.VatEconomicCode)
                    .WithMany(p => p.MushakGenerationVatEconomicCodes)
                    .HasForeignKey(d => d.VatEconomicCodeId);

                entity.HasOne(d => d.VatPaymentBankBranch)
                    .WithMany(p => p.MushakGenerationVatPaymentBankBranches)
                    .HasForeignKey(d => d.VatPaymentBankBranchId);
            });

            modelBuilder.Entity<MushakGenerationStage>(entity =>
            {
                entity.ToTable("MushakGenerationStage");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameInBangla)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<NbrEconomicCode>(entity =>
            {
                entity.ToTable("NbrEconomicCode");

                entity.Property(e => e.NbrEconomicCodeId).ValueGeneratedNever();

                entity.Property(e => e.Code10thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code11thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code12thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code13thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code1stDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code2ndDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code3rdDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code4thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code5thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code6thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code7thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code8thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code9thDisit)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.EconomicCode)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EconomicTitle)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EffectiveFrom)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EffectiveTo).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.NbrEconomicCodeType)
                    .WithMany(p => p.NbrEconomicCodes)
                    .HasForeignKey(d => d.NbrEconomicCodeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NbrEconomicCode_NbrEconomicCodeType");
            });

            modelBuilder.Entity<NbrEconomicCodeType>(entity =>
            {
                entity.ToTable("NbrEconomicCodeType");

                entity.Property(e => e.NbrEconomicCodeTypeId).ValueGeneratedNever();

                entity.Property(e => e.CodeTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ObjectType>(entity =>
            {
                entity.ToTable("ObjectType");

                entity.Property(e => e.ObjectTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Bin)
                    .HasColumnName("BIN")
                    .HasMaxLength(50);

                entity.Property(e => e.CertificateNo).HasMaxLength(50);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VatResponsiblePersonDesignation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VatResponsiblePersonEmailAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.VatResponsiblePersonMobileNo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.VatResponsiblePersonName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.VatregNo)
                    .HasColumnName("VATRegNo")
                    .HasMaxLength(50);

                entity.HasOne(d => d.BusinessNature)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.BusinessNatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organizations_BusinessNature");

                entity.HasOne(d => d.FinancialActivityNature)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.FinancialActivityNatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organizations_FinancialActivityNature");
            });

            modelBuilder.Entity<OverHeadCost>(entity =>
            {
                entity.ToTable("OverHeadCost");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.Property(e => e.PaymentMethodId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PriceSetup>(entity =>
            {
                entity.ToTable("PriceSetup");

                entity.Property(e => e.BaseTp)
                    .HasColumnName("BaseTP")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.EffectiveTo).HasColumnType("datetime");

                entity.Property(e => e.Mrp)
                    .HasColumnName("MRP")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PurchaseUnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SalesUnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.PriceSetups)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceSetup_MeasurementUnits");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.PriceSetups)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceSetup_Organizations");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PriceSetups)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceSetup_Products");
            });

            modelBuilder.Entity<PriceSetupProductCost>(entity =>
            {
                entity.ToTable("PriceSetupProductCost");

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequiredQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WastagePercentage).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.PriceSetupProductCosts)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .HasConstraintName("FK_PriceSetupProductCost_MeasurementUnits");

                entity.HasOne(d => d.OverHeadCost)
                    .WithMany(p => p.PriceSetupProductCosts)
                    .HasForeignKey(d => d.OverHeadCostId)
                    .HasConstraintName("FK_PriceSetupProductCost_OverHeadCost");

                entity.HasOne(d => d.PriceSetup)
                    .WithMany(p => p.PriceSetupProductCosts)
                    .HasForeignKey(d => d.PriceSetupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceSetupProductCost_PriceSetup");

                entity.HasOne(d => d.RawMaterial)
                    .WithMany(p => p.PriceSetupProductCosts)
                    .HasForeignKey(d => d.RawMaterialId)
                    .HasConstraintName("FK_PriceSetupProductCost_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.EffectiveTo).HasColumnType("datetime");

                entity.Property(e => e.Hscode)
                    .HasColumnName("HSCode")
                    .HasMaxLength(50);

                entity.Property(e => e.ModelNo).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalQuantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_MeasurementUnits");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Organizations");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .HasConstraintName("FK_Products_ProductCategory");

                entity.HasOne(d => d.ProductGroup)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductGroups");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_ProductCategory_Organizations");
            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Node).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductProductTypeMapping>(entity =>
            {
                entity.ToTable("ProductProductTypeMapping");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProductTypeMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductProductTypeMapping_Products");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.ProductProductTypeMappings)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductProductTypeMapping_ProductType");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProductVat>(entity =>
            {
                entity.ToTable("ProductVATs");

                entity.Property(e => e.ProductVatid).HasColumnName("ProductVATId");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.EffectiveTo).HasColumnType("datetime");

                entity.Property(e => e.ProductVattypeId).HasColumnName("ProductVATTypeId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVats)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductVATs_Products");

                entity.HasOne(d => d.ProductVattype)
                    .WithMany(p => p.ProductVats)
                    .HasForeignKey(d => d.ProductVattypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductVATs_ProductVATTypes");
            });

            modelBuilder.Entity<ProductVattype>(entity =>
            {
                entity.ToTable("ProductVATTypes");

                entity.Property(e => e.ProductVattypeId)
                    .HasColumnName("ProductVATTypeId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DefaultVatPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EffectiveFrom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EffectiveTo).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SupplementaryDutyPercent).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.PurchaseType)
                    .WithMany(p => p.ProductVattypes)
                    .HasForeignKey(d => d.PurchaseTypeId)
                    .HasConstraintName("FK_ProductVATTypes_PurchaseTypes");

                entity.HasOne(d => d.SalesType)
                    .WithMany(p => p.ProductVattypes)
                    .HasForeignKey(d => d.SalesTypeId)
                    .HasConstraintName("FK_ProductVATTypes_SalesType");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.ProductVattypes)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .HasConstraintName("FK_ProductVATTypes_TransectionTypes");
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ExpectedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Productions)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Productions_Organizations");
            });

            modelBuilder.Entity<ProductionDetail>(entity =>
            {
                entity.HasKey(e => e.ProductionDetailsId);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductionDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductionDetails_Products");

                entity.HasOne(d => d.Production)
                    .WithMany(p => p.ProductionDetails)
                    .HasForeignKey(d => d.ProductionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductionDetails_Productions");
            });

            modelBuilder.Entity<ProductionReceive>(entity =>
            {
                entity.ToTable("ProductionReceive");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.MaterialCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReceiveQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReceiveTime).HasColumnType("datetime");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.ProductionReceives)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductionReceive_Organizations");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("Purchase");

                entity.Property(e => e.BillOfEntry).HasMaxLength(50);

                entity.Property(e => e.BillOfEntryDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountOnTotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DueAmount)
                    .HasColumnType("decimal(22, 2)")
                    .HasComputedColumnSql("(CONVERT([decimal](21,2),case when [IsVatDeductedInSource]=(1) then [TotalPriceWithoutVat]-[DiscountOnTotalPrice] else ([TotalPriceWithoutVat]+[TotalVAT])-[DiscountOnTotalPrice] end)-[PaidAmount])");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.ExpectedDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LcDate).HasColumnType("datetime");

                entity.Property(e => e.LcNo).HasMaxLength(50);

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PayableAmount)
                    .HasColumnType("decimal(21, 2)")
                    .HasComputedColumnSql("(CONVERT([decimal](21,2),case when [IsVatDeductedInSource]=(1) then [TotalPriceWithoutVat]-[DiscountOnTotalPrice] else ([TotalPriceWithoutVat]+[TotalVAT])-[DiscountOnTotalPrice] end))");

                entity.Property(e => e.PoNumber).HasMaxLength(50);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.TermsOfLc).HasMaxLength(500);

                entity.Property(e => e.TotalAdvanceIncomeTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalAdvanceTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalDiscountOnIndividualProduct).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalImportDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPriceWithoutVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalRegulatoryDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalSupplementaryDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalVat)
                    .HasColumnName("TotalVAT")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VatChallanIssueDate).HasColumnType("datetime");

                entity.Property(e => e.VatChallanNo).HasMaxLength(50);

                entity.Property(e => e.VdscertificateDate)
                    .HasColumnName("VDSCertificateDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.VdscertificateNo)
                    .HasColumnName("VDSCertificateNo")
                    .HasMaxLength(50);

                entity.Property(e => e.VendorInvoiceNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Organizations");

                entity.HasOne(d => d.PurchaseReason)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.PurchaseReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_PurchaseReason");

                entity.HasOne(d => d.PurchaseType)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.PurchaseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_PurchaseTypes");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Vendor");
            });

            modelBuilder.Entity<PurchaseDetail>(entity =>
            {
                entity.Property(e => e.AdvanceIncomeTaxPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AdvanceTaxPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DiscountPerItem).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ImportDutyPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductVattypeId).HasColumnName("ProductVATTypeId");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RegulatoryDutyPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SupplementaryDutyPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vatpercent)
                    .HasColumnName("VATPercent")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_MeasurementUnits");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_Products");

                entity.HasOne(d => d.ProductVattype)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.ProductVattypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_ProductVATTypes");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_Purchase");
            });

            modelBuilder.Entity<PurchasePayment>(entity =>
            {
                entity.ToTable("PurchasePayment");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.PurchasePayments)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchasePayment_PaymentMethod");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.PurchasePayments)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchasePayment_Purchase");
            });

            modelBuilder.Entity<PurchaseReason>(entity =>
            {
                entity.ToTable("PurchaseReason");

                entity.Property(e => e.PurchaseReasonId).ValueGeneratedNever();

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PurchaseType>(entity =>
            {
                entity.Property(e => e.PurchaseTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Right>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(128);

                entity.Property(e => e.RightName)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Roles_Organizations");
            });

            modelBuilder.Entity<RoleRight>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.RoleRights)
                    .HasForeignKey(d => d.RightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRights_Rights");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleRights)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_RoleFeatures_dbo_Roles_RoleId");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SalesId);

                entity.Property(e => e.BillOfEntry).HasMaxLength(50);

                entity.Property(e => e.BillOfEntryDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerPoNumber).HasMaxLength(50);

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountOnTotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.ExpectedDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LcDate).HasColumnType("datetime");

                entity.Property(e => e.LcNo).HasMaxLength(50);

                entity.Property(e => e.PaymentDueAmount)
                    .HasColumnType("decimal(22, 2)")
                    .HasComputedColumnSql("(CONVERT([decimal](21,2),case when [IsVatDeductedInSource]=(1) then [TotalPriceWithoutVat]-[DiscountOnTotalPrice] else ([TotalPriceWithoutVat]+[TotalVAT])-[DiscountOnTotalPrice] end)-[PaymentReceiveAmount])");

                entity.Property(e => e.PaymentReceiveAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReceivableAmount)
                    .HasColumnType("decimal(21, 2)")
                    .HasComputedColumnSql("(CONVERT([decimal](21,2),case when [IsVatDeductedInSource]=(1) then [TotalPriceWithoutVat]-[DiscountOnTotalPrice] else ([TotalPriceWithoutVat]+[TotalVAT])-[DiscountOnTotalPrice] end))");

                entity.Property(e => e.ReceiverContactNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverName).HasMaxLength(200);

                entity.Property(e => e.SalesDate).HasColumnType("datetime");

                entity.Property(e => e.SalesDeliveryTypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.ShippingAddress).HasMaxLength(200);

                entity.Property(e => e.TaxInvoicePrintedTime).HasColumnType("datetime");

                entity.Property(e => e.TermsOfLc).HasMaxLength(500);

                entity.Property(e => e.TotalDiscountOnIndividualProduct).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPriceWithoutVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalSupplimentaryDuty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalVat)
                    .HasColumnName("TotalVAT")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VatChallanNo).HasMaxLength(50);

                entity.Property(e => e.WorkOrderNo).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Sales_Customer");

                entity.HasOne(d => d.DeliveryMethod)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.DeliveryMethodId)
                    .HasConstraintName("FK_Sales_DeliveryMethod");

                entity.HasOne(d => d.ExportType)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ExportTypeId)
                    .HasConstraintName("FK_Sales_ExportType");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.SaleOrganizations)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Organizations");

                entity.HasOne(d => d.OtherBranchOrganization)
                    .WithMany(p => p.SaleOtherBranchOrganizations)
                    .HasForeignKey(d => d.OtherBranchOrganizationId)
                    .HasConstraintName("FK_Sales_OtherBranchOrganization");

                entity.HasOne(d => d.SalesDeliveryType)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.SalesDeliveryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_SalesDeliveryType");

                entity.HasOne(d => d.SalesType)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.SalesTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_SalesType");
            });

            modelBuilder.Entity<SalesDeliveryType>(entity =>
            {
                entity.ToTable("SalesDeliveryType");

                entity.Property(e => e.SalesDeliveryTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SalesDetail>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DiscountPerItem).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductVattypeId)
                    .HasColumnName("ProductVATTypeId")
                    .HasDefaultValueSql("((139))");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SupplementaryDutyPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vatpercent)
                    .HasColumnName("VATPercent")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.SalesDetails)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesDetails_MeasurementUnits");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesDetails_Products");

                entity.HasOne(d => d.ProductVattype)
                    .WithMany(p => p.SalesDetails)
                    .HasForeignKey(d => d.ProductVattypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesDetails_ProductVATTypes");

                entity.HasOne(d => d.Sales)
                    .WithMany(p => p.SalesDetails)
                    .HasForeignKey(d => d.SalesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesDetails_Sales");
            });

            modelBuilder.Entity<SalesPaymentReceive>(entity =>
            {
                entity.ToTable("SalesPaymentReceive");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.ReceiveAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReceiveDate).HasColumnType("datetime");

                entity.HasOne(d => d.ReceivedPaymentMethod)
                    .WithMany(p => p.SalesPaymentReceives)
                    .HasForeignKey(d => d.ReceivedPaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesPaymentReceive_PaymentMethod");

                entity.HasOne(d => d.Sales)
                    .WithMany(p => p.SalesPaymentReceives)
                    .HasForeignKey(d => d.SalesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesPaymentReceive_Sales");
            });

            modelBuilder.Entity<SalesType>(entity =>
            {
                entity.ToTable("SalesType");

                entity.Property(e => e.SalesTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.SalesTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StockIn>(entity =>
            {
                entity.ToTable("StockIn");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.CurrentStock)
                    .HasColumnType("decimal(23, 2)")
                    .HasComputedColumnSql("((((([InQuantity]-[SaleQuantity])-[DamageQuantity])-[UsedInProductionQuantity])-[PurchaseReturnQty])+[SalesReturnQty])");

                entity.Property(e => e.DamageQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EndUnitPriceWithoutVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InitUnitPriceWithoutVat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InitialQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PurchaseReturnQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SaleQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SalesReturnQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UsedInProductionQuantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.StockIns)
                    .HasForeignKey(d => d.MeasurementUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockIn_MeasurementUnits");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.StockIns)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockIn_Organizations");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StockIns)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockIn_Products");

                entity.HasOne(d => d.ProductionReceive)
                    .WithMany(p => p.StockIns)
                    .HasForeignKey(d => d.ProductionReceiveId)
                    .HasConstraintName("FK_StockIn_ProductionReceive");

                entity.HasOne(d => d.PurchaseDetail)
                    .WithMany(p => p.StockIns)
                    .HasForeignKey(d => d.PurchaseDetailId)
                    .HasConstraintName("FK_StockIn_PurchaseDetails");
            });

            modelBuilder.Entity<SupplimentaryDuty>(entity =>
            {
                entity.ToTable("SupplimentaryDuty");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.EffectiveTo).HasColumnType("datetime");

                entity.Property(e => e.SdPercent).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.SupplimentaryDuties)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplimentaryDuty_Organizations");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SupplimentaryDuties)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplimentaryDuty_Products");
            });

            modelBuilder.Entity<TransectionType>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UK_Users_UserName")
                    .IsUnique();

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(64);

                entity.Property(e => e.FullName).HasMaxLength(200);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(64);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Users_Organizations");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_UserProfiles_dbo_Roles_RoleId");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserTypes");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");

                entity.Property(e => e.EffectiveTo).HasColumnType("datetime");

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.BinNo).HasMaxLength(20);

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NationalIdNo).HasMaxLength(50);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Vendors)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Vendor_Organizations");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}