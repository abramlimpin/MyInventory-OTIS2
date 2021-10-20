using Microsoft.AspNetCore.Mvc;
using MyInventory.Data;
using MyInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInventory.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ChatModel record)
        {
            var chat = new ChatModel()
            {
                DateAdded = DateTime.Now,
                Sender = record.Sender,
                Message = record.Message,
                ContactNo = record.ContactNo,
                Status = ChatStatus.Active,
                Email = record.Email
            };

            _context.Chat.Add(chat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var list = _context.Chat
                .Where(c => c.Status == ChatStatus.Active)
                .OrderByDescending(c => c.DateAdded)
                .ToList();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var record = _context.Chat.Where(c => c.Id == id).SingleOrDefault();
            if (record == null)
            {
                return RedirectToAction("Index");
            }

            return View(record);
        }

        [HttpPost]
        public IActionResult Details(ChatModel record)
        {
            var chat = new ChatModel()
            {
                Id = record.Id,
                Sender = record.Sender,
                Message = record.Message,
                ContactNo = record.ContactNo,
                Status = ChatStatus.Active,
                Email = record.Email
            };

            _context.Chat.Update(chat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var record = _context.Chat.Where(c => c.Id == id).SingleOrDefault();
            if (record == null)
            {
                return RedirectToAction("Index");
            }
            record.Status = ChatStatus.Archived;
            _context.Chat.Update(record);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
