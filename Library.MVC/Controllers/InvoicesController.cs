using Library.Domain;
using Library.MVC.Data;
using Library.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.MVC.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var invoices = await _context.Invoices
                .Include(i => i.InvoiceLines)
                .ToListAsync();

            return View(invoices);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateInvoiceViewModel
            {
                ProductList = _context.Products.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var invoice = new Invoice
                {
                    CustomerName = model.SelectedCustomer,
                    Date = DateTime.Now
                };

                
                invoice.InvoiceLines.Add(model.SingleLine);

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Product"); 
            }
            return View(model);
        }
    }
}
