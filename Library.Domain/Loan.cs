using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain
{
    public class Loan
    {
        public int Id { get; set; }

        [Display(Name = "Book Title")]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [Display(Name = "Member ID")]
        public int MemberId { get; set; }

        public string? UserId { get; set; }
        public Member? Member { get; set; }

        [Display(Name = "Loan Date")]
        public DateTime LoanDate { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime? ReturnedDate { get; set; } // Nullable: if null, book is still out
    }
}
