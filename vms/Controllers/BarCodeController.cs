using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inventory.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using vms.entity.viewModels;
using vms.service.dbo;
using vms.utility.StaticData;

namespace Inventory.Controllers
{
    public class BarCodeController : ControllerBase
    {
        private readonly IProductService _prodService;

        private readonly IProductPriceService _price;
        //private readonly IProductService _prodService;
        // private readonly IProductLogService _logService;
        // private readonly IMesureUnitService _unitService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BarCodeController(

            ControllerBaseParamModel controllerBaseParamModel,
            IHostingEnvironment hostingEnvironment,
            IProductService prodService,
            IProductPriceService price
        ) : base(controllerBaseParamModel)
        {

            _hostingEnvironment = hostingEnvironment;
            _prodService = prodService;
            _price = price;

        }
        public async Task<IActionResult> Index()
        {
            ViewData["ProductId"] = new SelectList(await _prodService.Query().Where(c=>c.IsActive==true).SelectAsync(), "ProductId", "Code");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(BarCode barCode)
        {
            ViewData["ProductId"] = new SelectList(await _prodService.Query().Where(c=>c.IsActive==true).SelectAsync(), "ProductId", "Code");

            var salesPRice = await _price.Query().Include(c => c.Product).SingleOrDefaultAsync(c => c.ProductId == barCode.ProductId && c.IsActive == true, CancellationToken.None);
          
            if (barCode.ProductId == 0 || salesPRice == null)  
            {
                return View();
            }
            ViewBag.Price = salesPRice.SaleAmount;
            ViewBag.SaleCode = salesPRice.Product.Code;
            ViewBag.NumberOfBarCode = barCode.Value;
            ViewBag.Flag = true;
            var barcodeValue = salesPRice.Product.Code;
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            var image = barcode.Draw(barcodeValue, 25, 1);
            // ViewBag.Image = image;
            var imgName = "sabbir";
            var FileExtenstion = Path.GetExtension(imgName);

            string FileName = Guid.NewGuid().ToString();

            FileName += FileExtenstion;
            var FolderName = ControllerStaticData.APPLICATION_DOCUMENT + "BarCode";
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, FolderName);
            // string ImageFolder = Server.MapPath("~/img");
            //image.Save(ImageFolder + "/" + imgName.Trim() + ".bmp");


            bool exists = Directory.Exists(uploads);
            if (!exists)
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, imgName);

            image.Save(uploads + "/" + imgName.Trim() + ".bmp");

            //image to displa in view  
            var virtualPath = string.Format("~/Images/{0}.bmp", imgName.Trim());
            //ViewBag.Image = virtualPath;

            return View();

        }
    }
}