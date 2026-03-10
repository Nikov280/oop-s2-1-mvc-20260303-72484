using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string CustomerName { get; set; } // O una relación con Customer
        public List<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    }
}
