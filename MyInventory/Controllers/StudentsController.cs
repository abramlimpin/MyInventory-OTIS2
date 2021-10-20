using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyInventory.Data;
using MyInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyInventory.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student record)
        {
            var student = new Student()
            {
                FN = record.FN,
                LN = record.LN,
                StudentNo = record.StudentNo,
                Remarks = record.Remarks,
                DateAdded = DateTime.Now,
                Email = record.Email,
                Birthdate = record.Birthdate
            };

            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user.Type != UserTypes.Admin)
            {
                return RedirectToAction("Index", "Home");
            }

            var list = _context
                .Students.ToList();
            return View(list);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var record = _context.Students
                .Where(s => s.Id == id).SingleOrDefault();

            if (record == null)
                return RedirectToAction("Index");

            return View(record);
        }

        [HttpPost]
        public IActionResult Details(int? id, Student record)
        {
            var student = new Student()
            {
                Id = record.Id,
                FN = record.FN,
                LN = record.LN,
                StudentNo = record.StudentNo,
                Remarks = record.Remarks,
                Email = record.Email,
                Birthdate = record.Birthdate
            };
            _context.Students.Update(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var record = _context.Students
                .Where(s => s.Id == id).SingleOrDefault();

            if (record == null)
                return RedirectToAction("Index");

            _context.Students.Remove(record);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
