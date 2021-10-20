using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyInventory.Data;
using MyInventory.Models;

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
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();

            var model = new StoreViewModel
            {
                ProductList = products,
                CategoryList = categories
            };
            return View(model);
        }
    }
}
