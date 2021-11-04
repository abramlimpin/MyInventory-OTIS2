using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyInventory.Models;
using MyInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace MyInventory.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category)
                .ToList();

            if (Request.Query.ContainsKey("c"))
            {
                products = products.Where(p => p.CatId ==
                    int.Parse(Request.Query["c"].ToString()))
                    .ToList();
            }

            var categories = _context.Categories
                .OrderBy(c => c.Name)
                .ToList();

            var record = new StoreViewModel()
            {
                ProductList = products,
                CategoryList = categories
            };

            return View(record);
        }
    }
}
