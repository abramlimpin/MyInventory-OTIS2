using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInventory.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="First Name")]
        [Required]
        public string FN { get; set; }

        [Display(Name ="Last Name")]
        [Required]
        public string LN { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }

        [Display(Name = "Student Number")]
        public int StudentNo { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
}
