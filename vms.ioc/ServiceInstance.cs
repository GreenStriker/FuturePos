using vms.service.dbo;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using URF.Core.Abstractions;
using URF.Core.EF;
using vms.entity.models;
using vms.entity.viewModels;
using vms.repository.dbo;
using vms.repository.dbo.StoredProcedure;
using vms.service.dbo.StoredProdecure;

//using vms.repository.dbo.StoredProcedure;

//using vms.service.dbo.acc;
//using vms.service.dbo.StoredProdecure;

namespace vms.ioc
{
    public static class ServiceInstance
    {
        public static void RegisterVMSServiceInstance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("dev");
            services.AddDbContext<InventoryContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, InventoryContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddSingleton<PurposeStringConstants>();

            //services.AddScoped<IAutocompleteRepository, AutocompleteRepository>();
            //services.AddScoped<IAutocompleteService, AutocompleteService>();
            services.AddScoped<ISalesPaymentReceiveRepository, SalesPaymentReceiveRepository>();
            services.AddScoped<ISalesPaymentReceiveService, SalesPaymentReceiveService>();
            services.AddScoped<IProductLogRepository, ProductLogRepository>();
            services.AddScoped<IProductLogService, ProductLogService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IAutocompleteRepository, AutocompleteRepository>();
            services.AddScoped<IAutocompleteService, AutocompleteService>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISalePaymentRepository, SalePaymentRepository>();
            services.AddScoped<ISalePaymentService, SalePaymentService>();
            services.AddScoped<ISaleContentRepository, SaleContentRepository>();
            services.AddScoped<ISaleContentService, SaleContentService>();

            services.AddScoped<IPurchaseDetailRepository, PurchaseDetailRepository>();
            services.AddScoped<IPurchaseDetailService, PurchaseDetailService>();

            services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
            services.AddScoped<ISaleDetailService, SaleDetailService>();

            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IVendorService, VendorService>();

            services.AddScoped<IPurchasePaymentRepository, PurchasePaymentRepository>();
            services.AddScoped<IPurchasePaymentService, PurchasePaymentService>();

            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockService, StockService>();


            services.AddScoped<IPurchaseContentRepository, PurchaseContentRepository>();
            services.AddScoped<IPurchaseContentService, PurchaseContentService>();
            //services.AddScoped<IMushok6P3ViewRepositoy, Mushok6P3ViewRepositoy>();
            //services.AddScoped<IMushok6P3ViewService, Mushok6P3ViewService>();
            //services.AddScoped<ISpGetSalePagedRepository, SpGetSalePagedRepository>();
            //services.AddScoped<ISpGetSalePagedService, SpGetSalePagedService>();
            //services.AddScoped<IDamageInvoiceListRepository, DamageInvoiceListRepository>();
            //services.AddScoped<IDamageInvoiceListService, DamageInvoiceListService>();

            //services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            //services.AddTransient<IOrganizationService, OrganizationService>();

            //services.AddTransient<INbrEconomicCodeRepository, NbrEconomicCodeRepository>();
            //services.AddTransient<INbrEconomicCodeService, NbrEconomicCodeService>();
            //services.AddTransient<IBankBranchRepository, BankBranchRepository>();
            //services.AddTransient<IBankBranchService, BankBranchService>();

            //services.AddTransient<IMeasurementUnitRepository, MeasurementUnitRepository>();
            //services.AddTransient<IMeasurementUnitService, MeasurementUnitService>();

            //services.AddTransient<IOrderRepository, OrderRepository>();
            //services.AddTransient<IOrderService, OrderService>();

            //services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<IProductService, ProductService>();

            //services.AddTransient<IProductGroupRepository, ProductGroupRepository>();
            //services.AddTransient<IProductGroupService, ProductGroupService>();

            //services.AddTransient<IDebitNoteRepository, DebitNoteRepository>();
            //services.AddTransient<IDebitNoteService, DebitNoteService>();

            //services.AddTransient<ICreditNoteRepository, CreditNoteRepository>();
            //services.AddTransient<ICreditNoteService, CreditNoteService>();



            //services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
            //services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();
            //services.AddTransient<ISaleOrderRepository, SaleOrderRepository>();
            //services.AddTransient<ISaleOrdersService, SaleOrdersService>();
            //services.AddTransient<ISaleOrderDetailsRepository, SaleOrderDetailRepository>();
            //services.AddTransient<ISaleOrderDetailService, SaleOrderDetailService>();

            //services.AddTransient<IPurchaseOrderDetailsRepository, PurchaseOrderDetailRepository>();
            //services.AddTransient<IPurchaseOrderDetailService, PurchaseOrderDetailService>();

            //services.AddTransient<IPurchaseTypeRepository, PurchaseTypeRepository>();
            //services.AddTransient<IPurchaseTypeService, PurchaseTypeService>();

            //services.AddTransient<IRightRepository, RightRepository>();
            //services.AddTransient<IRightService, RightService>();

            services.AddTransient<IThemeRepository, ThemeRepository>();
            services.AddTransient<IThemeService, ThemeService>();

            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IColorService, ColorService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IVatRepository, VatRepository>();
            services.AddTransient<IVatService, VatService>();
            services.AddTransient<IContentRepository, ContentRepository>();
            services.AddTransient<IContentService, ContentService>();

            services.AddTransient<IContentTypeRepository, ContentTypeRepository>();
            services.AddTransient<IContentTypeService, ContentTypeService>();

            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();

            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPaymentService, PaymentService>();

            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<IBranchService, BranchService>();

            services.AddTransient<IMesureUnitRepository, MesureUnitRepository>();
            services.AddTransient<IMesureUnitService, MesureUnitService>();

            services.AddTransient<IProductPriceRepository, ProductPriceRepository>();
            services.AddTransient<IProductPriceService, ProductPriceService>();

            services.AddTransient<IEmployeRepository, EmployeRepository>();
            services.AddTransient<IEmployeService, EmployeService>();

            services.AddTransient<ISalaryRepository, SalaryRepository>();
            services.AddTransient<ISalaryService, SalaryService>();

            services.AddTransient<IExpenceRepository, ExpenceRepository>();
            services.AddTransient<IExpenceService, ExpenceService>();

            services.AddTransient<IExpenceTypeRepository, ExpenceTypeRepository>();
            services.AddTransient<IExpenceTypeService, ExpenceTypeService>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();

            services.AddTransient<IAdvancedSalaryRepository, AdvancedSalaryRepository>();
            services.AddTransient<IAdvancedSalaryService, AdvancedSalaryService>();

            services.AddTransient<IOvertimeRepository, OvertimeRepository>();
            services.AddTransient<IOvertimeService, OvertimeService>();

            services.AddTransient<IAtendenceRepository, AtendenceRepository>();
            services.AddTransient<IAtendenceService, AtendenceService>();


            services.AddTransient<IAttendenceDetailRepository, AttendenceDetailRepository>();
            services.AddTransient<IAttendenceDetailService, AttendenceDetailService>();

            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<ISettingService, SettingService>();

            services.AddTransient<IIncentiveRepository, IncentiveRepository>();
            services.AddTransient<IIncentiveService, IncentiveService>();

            services.AddTransient<IPayrollRepository, PayrollRepository>();
            services.AddTransient<IPayrollService, PayrollService>();

            services.AddTransient<IPayrollDetailRepository, PayrollDetailRepository>();
            services.AddTransient<IPayrollDetailService, PayrollDetailService>();

            services.AddTransient<IRewardPointRepository, RewardPointRepository>();
            services.AddTransient<IRewardPointService, RewardPointService>();

            //services.AddTransient<IProductProductTypeMappingRepository, ProductProductTypeMappingRepository>();
            //services.AddTransient<IProductProductTypeMappingService, ProductProductTypeMappingService>();

            //services.AddTransient<IProductionRepository, ProductionRepository>();
            //services.AddTransient<IProductionService, ProductionService>();

            //services.AddTransient<IProductionDetailRepository, ProductionDetailRepository>();
            //services.AddTransient<IProductionDetailService, ProductionDetailService>();

            //services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            //services.AddTransient<IProductCategoryService, ProductCategoryService>();

            //services.AddTransient<IMushakGenerationRepository, MushakGenerationRepository>();
            //services.AddTransient<IMushakGenerationService, MushakGenerationService>();

            //services.AddTransient<ISupplimentaryDutyRepository, SupplimentaryDutyRepository>();
            //services.AddTransient<ISupplimentaryDutyService, SupplimentaryDutyService>();

            //services.AddTransient<IDamageTypeRepository, DamageTypeRepository>();
            //services.AddTransient<IDamageTypeService, DamageTypeService>();





            //services.AddTransient<IDeliveryMethodRepository, DeliveryMethodRepository>();
            //services.AddTransient<IDeliveryMethodService, DeliveryMethodService>();

            //services.AddTransient<IDeliveryMethodRepository, DeliveryMethodRepository>();
            //services.AddTransient<IDeliveryMethodService, DeliveryMethodService>();

            //services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
            //services.AddTransient<IDocumentTypeService, DocumentTypeService>();

            //services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            //services.AddTransient<IPaymentMethodService, PaymentMethodService>();

            //services.AddTransient<IPurchasePaymentRepository, PurchasePaymentRepository>();
            //services.AddTransient<IPurchasePaymentService, PurchasePaymentService>();

            //services.AddTransient<ISalesPaymentReceiveRepository, SalesPaymentReceiveRepository>();
            //services.AddTransient<ISalesPaymentReceiveService, SalesPaymentReceiveService>();
        }
    }
}