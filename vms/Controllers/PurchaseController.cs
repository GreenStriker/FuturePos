using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inventory.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vms.entity.models;
using vms.entity.viewModels;
using vms.Models;
using vms.service.dbo;
using vms.utility.StaticData;
using X.PagedList;

namespace Inventory.Controllers
{
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _service;
        private readonly IPurchaseDetailService _detailService;
        private readonly IVendorService _vendorService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IPurchasePaymentService _paymentService;
        private readonly IStockService _stockService;
        private readonly IPurchaseContentService _contentService;
        private readonly IPaymentMethodService _paymentMethodService;
        public PurchaseController(
            IPaymentMethodService paymentMethodService,
            ControllerBaseParamModel controllerBaseParamModel,
            IHostingEnvironment hostingEnvironment,
            IPurchaseService service,
            IVendorService vendorService,
            IPurchaseDetailService detailService,
            IPurchasePaymentService paymentService,
            IStockService stockService,
            IPurchaseContentService contentService
        ) : base(controllerBaseParamModel)
        {
            _paymentMethodService = paymentMethodService;
            _vendorService = vendorService;
            _hostingEnvironment = hostingEnvironment;
            _service = service;
            _detailService = detailService;
            _paymentService = paymentService;
            _stockService = stockService;
            _contentService = contentService;

        }

        public async System.Threading.Tasks.Task<IActionResult> Index(int? page, string search = null)
        {
            var getPurchase = await _service.Query().Where(c => c.BranchId == _session.BranchId)
                .Include(c => c.Vendor)
                .Include(c=>c.Branch)
                .OrderByDescending(c => c.PurchaseId).SelectAsync(CancellationToken.None);
            if (search != null)
            {
                search = search.ToLower().Trim();
                getPurchase = getPurchase.Where(c => c.PurchaseInvoice.ToLower().Contains(search)
                );
                ViewData[ViewStaticData.SEARCH_TEXT] = search;
            }
            else
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = string.Empty;
            }
            var pageNumber = page ?? 1;
            var listOfPurchase = getPurchase.ToPagedList(pageNumber, 10);

            return View(listOfPurchase);
        }

        public async System.Threading.Tasks.Task<IActionResult> Create()
        {
            //var createdBy = _session.UserId;
            var branchId = _session.BranchId;
            ViewData[ControllerStaticData.VENDOR_ID] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _vendorService.Query().SelectAsync(), ControllerStaticData.VENDOR_ID, ViewStaticData.NAME);
            return View();
        }
        public async Task<FileSaveFeedbackDto> FileSaveAsync(IFormFile File)
        {
            FileSaveFeedbackDto fdto = new FileSaveFeedbackDto();
            var FileExtenstion = Path.GetExtension(File.FileName);

            string FileName = Guid.NewGuid().ToString();

            FileName += FileExtenstion;
            var FolderName = ControllerStaticData.APPLICATION_DOCUMENT + "Product";
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, FolderName);

            fdto.MimeType = FileExtenstion;
            bool exists = Directory.Exists(uploads);
            if (!exists)
            {
                Directory.CreateDirectory(uploads);
            }
            if (File.Length > 0)
            {
                var filePath = Path.Combine(uploads, File.FileName);
                fdto.FileUrl = filePath;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }
            }
            return fdto;
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Create(vmPurchase vm)
        {
            var createdBy = _session.UserId;
            var branchId = _session.BranchId;
            bool status = false;
            if (vm.PurchaseOrderDetailList.Count<0)
            {
                return Json(status);
            }

            try
            {
                //For Purchase insert 
                Purchase purchase = new Purchase();
                //Get Payable Amount 
                decimal payableAmount = 0;
                decimal paidAmount = 0;
                decimal totalDiscountPerItem = 0;
                foreach (var detail in vm.PurchaseOrderDetailList)
                {
                    payableAmount += detail.Amount.Value * detail.Qty.Value;
                    totalDiscountPerItem += detail.DiscountPerItem.Value;
                }

                if (vm.PurchasePaymenJson==null)
                {
                    paidAmount = 0;
                }
                else
                {
                    foreach (var detail in vm.PurchasePaymenJson)
                    {
                        paidAmount += detail.PaidAmount.Value;
                    }
                }
               
                //Generate Invoice and Voucher No
                var voucher = DateTime.Now.ToLocalTime().ToString();
                //  purchase.IsActive = true;
                purchase.PurchaseInvoice ="invoice#"+ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                purchase.VoucherNo = null;
                purchase.VendorId = vm.VendorId;
                purchase.BranchId = _session.BranchId;
                purchase.PayableAmount = payableAmount;
                purchase.DiscountOnTotal = vm.DiscountOnTotal;
                purchase.TotalDiscountOnIndividualProduct = totalDiscountPerItem;
                purchase.PaidAmount = paidAmount;
                purchase.EfectiveFrom = DateTime.Now;
                purchase.EfectiveTo = null;
                purchase.IsActive = true;
                purchase.CreatedBy = createdBy;
                purchase.CreatedTime = DateTime.Now;
                _service.Insert(purchase);
               // await UnitOfWork.SaveChangesAsync();
                if (vm.PurchaseOrderDetailList.Count > 0)
                {

                    foreach (var item in vm.PurchaseOrderDetailList)
                    {
                        Stock stock = new Stock();
                        PurchaseDetail detail = new PurchaseDetail();
                        detail.PurchaseId = purchase.PurchaseId;
                        detail.ProductId = item.ProductId;
                        detail.Qty = item.Qty;
                        detail.Amount = item.Amount;
                        detail.DiscountPerItem = item.DiscountPerItem;
                        _detailService.Insert(detail);
                        stock.ProductId = item.ProductId;
                        stock.PurchaseDetailId = detail.PurchaseDetailId;
                        stock.InQty = detail.Qty;
                        // stock.InitialQty = null;
                        stock.BranchId = _session.BranchId;
                        stock.CreatedBy = _session.UserId;
                        stock.CreateTime = DateTime.Now;
                        stock.IsActive = true;
                        _stockService.Insert(stock);
                    }

                   // await UnitOfWork.SaveChangesAsync();
                }
                //PurchasePayment payment
                if (vm.PurchasePaymenJson!=null)
                {

                    foreach (var item in vm.PurchasePaymenJson)
                    {
                        PurchasePayment payment = new PurchasePayment();
                        payment.PurchaseId = purchase.PurchaseId;
                        payment.PaymentMethodId = item.PaymentMethodId;
                        payment.PaidAmount = item.PaidAmount;
                        payment.CreatedBy = _session.UserId;
                        payment.CreatedTime = DateTime.Now;
                        payment.PaymentDate = item.PaymentDate;
                        _paymentService.Insert(payment);
                    }

                   // await UnitOfWork.SaveChangesAsync();
                }
                await UnitOfWork.SaveChangesAsync();
                if (vm.ContentInfoJson != null)
                {
                    // Content content;
                    foreach (var contentInfo in vm.ContentInfoJson)
                    {
                        if (contentInfo.UploadFile != null && contentInfo.UploadFile.Length > 0)
                        {
                            var File = contentInfo.UploadFile;
                            var FileSaveFeedbackDto = await FileSaveAsync(File);
                            PurchaseContent content = new PurchaseContent();
                            content.IsActive = true;
                            content.Name = "Images";
                            // content.ContentTypeId = 1;
                            content.PurchaseId = purchase.PurchaseId;
                            content.Remark = "Test";
                            content.CreatedBy = _session.UserId;
                            content.CreatedTime = DateTime.Now;
                            content.Url = FileSaveFeedbackDto.FileUrl;
                           _contentService.Insert(content);
                        }

                       
                    }
                    await UnitOfWork.SaveChangesAsync();
                }
                return Json(true);

            }
            catch (Exception e)
            {
                return Json(status);
            }

        }
        public async System.Threading.Tasks.Task<IActionResult> Details(int id)
        {
           
            var purchase = await _service.GetById(id);
           
            return View(purchase);
        }

        public async Task<IActionResult> PurchaseDue(int? page, string search = null)
        {
            var getPurchase = await _service.Query().Where(c => c.BranchId == _session.BranchId)
                .Include(c => c.Vendor)
                .Include(c => c.Branch)
                .Where(c=>c.DueAmount>0)
                .OrderByDescending(c => c.PurchaseId).SelectAsync(CancellationToken.None);
            ViewBag.PageCount = getPurchase.Count();
            if (search != null)
            {
                search = search.ToLower().Trim();
                getPurchase = getPurchase.Where(c => c.PurchaseInvoice.ToLower().Contains(search)
                );
                ViewData[ViewStaticData.SEARCH_TEXT] = search;
            }
            else
            {
                ViewData[ViewStaticData.SEARCH_TEXT] = string.Empty;
            }
            var pageNumber = page ?? 1;
            var listOfPurchase = getPurchase.ToPagedList(pageNumber, 10);

            return View(listOfPurchase);

        }
        public async Task<IActionResult> PurchasePayment(int id)
        {
            var purchaseDetails = await _service.GetById(id);
            //Query().SingleOrDefaultAsync(p => p.PurchaseId == id, CancellationToken.None);
            var payments = await _paymentMethodService.Query().SelectAsync();
            IEnumerable<SelectListItems> paymentMethods = payments.Select(s => new SelectListItems
            {
                Id = s.PaymentMethodId,
                Name = s.Name
            });
            //int purchaseId = int.Parse(_dataProtector.Unprotect(id));
            VmDuePayment duePayment = new VmDuePayment
            {
                PurchaseId = id,
                PaymentMethods = paymentMethods,
                PayableAmount = purchaseDetails.PayableAmount,
                PrevPaidAmount = purchaseDetails.PaidAmount,
                DueAmount = purchaseDetails.DueAmount,
            };
          
            return View(duePayment);
        }
        [HttpPost]
        public async Task<IActionResult> PurchasePayment(VmDuePayment purchasePayment, int id)
        {
           // int purchaseId = int.Parse(_dataProtector.Unprotect(id));
            var purchaseDetails = await _service.Query().SingleOrDefaultAsync(p => p.PurchaseId == id, CancellationToken.None);

            var value = Convert.ToInt32(purchasePayment.DueAmount);
            if (purchasePayment.PaidAmount <= Convert.ToDecimal(purchaseDetails.PayableAmount))
            {
                if (purchasePayment.PaidAmount <= Convert.ToDecimal(value))
                {
                    var totalPaidAmount = purchaseDetails.PaidAmount + purchasePayment.PaidAmount;
                    vmPurchasePayment vmPurchasePayment = new vmPurchasePayment
                    {
                        PurchaseId = id,
                        PaymentMethodId = purchasePayment.PaymentMethodId,
                        TotalPaidAmount = Convert.ToDecimal(totalPaidAmount),
                        PaidAmount = purchasePayment.PaidAmount,
                        CreatedBy = _session.UserId
                    };
                    await _paymentService.ManagePurchaseDue(vmPurchasePayment);

                    TempData[ControllerStaticData.MESSAGE] = ControllerStaticData.SUCCESS_CLASSNAME;

                    return RedirectToAction(ViewStaticData.PURCHASE_DUE, ControllerStaticData.PURCHASE);
                }
                else
                {
                    ViewData[ControllerStaticData.MESSAGE] = MessageStaticData.PURCHASE_DUE_MESSAGE;
                }
            }
            else
            {
                ViewData[ControllerStaticData.MESSAGE] = MessageStaticData.PURCHASE_DUE_PAID_MESSAGE;
            }

            var payments = await _paymentMethodService.Query().SelectAsync();
            IEnumerable<SelectListItems> paymentMethods = payments.Select(s => new SelectListItems
            {
                Id = s.PaymentMethodId,
                Name = s.Name
            });
            purchasePayment.PaymentMethods = paymentMethods;
            return View(purchasePayment);
        }

    }
}