using Library.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.MVC.Models
{
    public class CreateInvoiceViewModel
    {
        public string SelectedCustomer { get; set; }
        
        public InvoiceLine SingleLine { get; set; } = new InvoiceLine();

        
        public List<SelectListItem>? ProductList { get; set; }
    }
}
