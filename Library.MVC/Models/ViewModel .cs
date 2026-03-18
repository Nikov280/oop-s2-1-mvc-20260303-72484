using Library.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.MVC.Models
{
    // ViewModel for creating a new loan
    public class CreateLoanViewModel
    {
        [Required(ErrorMessage = "Please select a member")]
        public int SelectedMemberId { get; set; }

        [Required(ErrorMessage = "Please select a book")]
        public int SelectedBookId { get; set; }

        public IEnumerable<SelectListItem>? MemberList { get; set; }

        // This list should only contain available books
        public IEnumerable<SelectListItem>? BookList { get; set; }

        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Required]
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14); // Default 2 weeks
    }
}