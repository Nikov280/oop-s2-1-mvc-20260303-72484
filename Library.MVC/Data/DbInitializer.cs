using Bogus;
using Library.Domain;
using Library.MVC.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Library.MVC.Data
{
    public static class DbInitializer
    {
        public static void SeedData(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // 1. Seed Admin Role
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            }

            var adminEmail = "admin@library.com";
            var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                userManager.CreateAsync(adminUser, "Admin123!").Wait();
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
            }

            
            if (context.Books.Any()) return;

            // 2. Seed Members
            var memberFaker = new Faker<Member>()
                .RuleFor(m => m.FullName, f => f.Name.FullName())
                .RuleFor(m => m.Email, f => f.Internet.Email())
                .RuleFor(m => m.Phone, f => f.Phone.PhoneNumber());

            var members = memberFaker.Generate(10);
            context.Members.AddRange(members);
            context.SaveChanges();

            // 3. Seed Books
            var bookFaker = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Commerce.ProductName())
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Isbn, f => f.Random.Replace("###-##########"))
                .RuleFor(b => b.Category, f => f.PickRandom("Fiction", "Science", "History", "Biography"))
                .RuleFor(b => b.IsAvailable, true);

            var books = bookFaker.Generate(20);
            context.Books.AddRange(books);
            context.SaveChanges();

            // 4. Seed Loans
            var random = new Random();
            for (int i = 0; i < 15; i++)
            {
                var book = books[i];
                bool isReturned = i >= 10;
                bool isOverdue = i < 3;

                var loan = new Loan
                {
                    BookId = book.Id,
                    MemberId = members[random.Next(0, 10)].Id,
                    UserId = adminUser.Id, 
                    LoanDate = DateTime.Now.AddDays(-15),
                    DueDate = isOverdue ? DateTime.Now.AddDays(-2) : DateTime.Now.AddDays(7),
                    ReturnedDate = isReturned ? DateTime.Now.AddDays(-1) : null
                };

                if (!isReturned) book.IsAvailable = false;

                
                context.Loans.Add(loan);
            }
            context.SaveChanges();
        }
    }
}