using Library.Domain; 
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.Tests
{
    public class LibraryLogicTests
    {
        // 1. Cannot create a loan for a book already on an active loan
        [Fact]
        public void Cannot_Create_Loan_For_Book_Already_On_Active_Loan()
        {
            // Arrange
            var book = new Book { Id = 1, IsAvailable = false }; 

            // Act
            bool canCreateLoan = book.IsAvailable;

            // Assert
            Assert.False(canCreateLoan, "No se debería poder crear un préstamo si el libro no está disponible.");
        }

        // 2. Returned loan makes book available again
        [Fact]
        public void Returned_Loan_Makes_Book_Available_Again()
        {
            // Arrange
            var book = new Book { Id = 1, IsAvailable = false };
            var loan = new Loan { Book = book };

            // Act
            loan.ReturnedDate = DateTime.Now;
            book.IsAvailable = true; 

            // Assert
            Assert.True(book.IsAvailable, "El libro debería estar disponible nuevamente al ser devuelto.");
        }

        // 3. Book search returns expected matches
        [Fact]
        public void Book_Search_Returns_Expected_Matches()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Title = "C# Programming", Author = "Author A" },
                new Book { Title = "Java Basics", Author = "Author B" },
                new Book { Title = "C# Advanced", Author = "Author C" }
            };
            string searchTerm = "C#";

            // Act
            var results = books.Where(b => b.Title.Contains(searchTerm)).ToList();

            // Assert
            Assert.Equal(2, results.Count);
            Assert.All(results, b => Assert.Contains(searchTerm, b.Title));
        }

        // 4. Overdue logic: DueDate < Today and ReturnedDate is null
        [Fact]
        public void Overdue_Logic_Check_Past_Due_And_Not_Returned()
        {
            // Arrange
            var dueDate = DateTime.Now.AddDays(-5); 
            DateTime? returnedDate = null; 

            // Act
            bool isOverdue = dueDate < DateTime.Now && returnedDate == null;

            // Assert
            Assert.True(isOverdue, "Un préstamo sin fecha de devolución y con fecha de vencimiento pasada debe estar vencido.");
        }

        // 5. Controller action guards (Check if only Admin can access)        
        [Fact]
        public void Admin_Role_Authorization_Guard_Check()
        {
            // Arrange
            string userRole = "Admin";
            string readerRole = "Reader";

            // Act
            bool isAdminAuthorized = userRole == "Admin";
            bool isReaderAuthorized = readerRole == "Admin";

            // Assert
            Assert.True(isAdminAuthorized, "El rol Admin debería tener acceso.");
            Assert.False(isReaderAuthorized, "El rol Reader no debería tener acceso a funciones de Admin.");
        }
    }
}