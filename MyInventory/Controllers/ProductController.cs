using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyInventory.Data;
using MyInventory.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyInventory.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product record, IFormFile ImagePath)
        {
            var selectedCategory = _context.Categories
                .Where(c => c.CatId == record.CatId).SingleOrDefault();

            var product = new Product()
            {
                Name = record.Name,
                Code = record.Code,
                Description = record.Description,
                Price = record.Price,
                Available = 0,
                DateAdded = DateTime.Now,
                Status = "Active",
                Category = selectedCategory,
                CatId = record.CatId
            };

            if (ImagePath != null)
            {
                if (ImagePath.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/img/products", ImagePath.FileName);
                    
                    var extension = Path.GetExtension(ImagePath.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImagePath.CopyTo(stream);
                    }

                    var newImage = System.Drawing.Image.FromFile(filePath);
                    var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    var qualityEncoder = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(qualityEncoder, 50L);

                    newImage.Save(filePath.Replace(extension, ".jpeg"), jpgEncoder, encoderParameters);

                    product.ImagePath = ImagePath.FileName;
                }
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var list = _context.Products.Include(p => p.Category).ToList();
            return View(list);
        }
    }
}
