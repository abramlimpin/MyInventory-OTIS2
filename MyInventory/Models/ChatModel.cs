using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInventory.Models
{
    public class ChatModel
    {
        [Required(ErrorMessage = "This is required.")]
        public string Sender { get; set; }

        [Required]
        [Display(Name ="Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNo { get; set; }

        [Display(Name ="Date Added")]
        public DateTime DateAdded { get; set; }

        public ChatStatus Status { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid format.")]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [Key]
        public int Id { get; set; }
    }

    public enum ChatStatus { 
        Active = 1,
        Archived = 0
    }
}
