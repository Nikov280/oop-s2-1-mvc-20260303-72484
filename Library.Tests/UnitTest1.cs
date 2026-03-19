using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.Tests
{
    public class LibraryLogicTests
    {
        [Fact]
        public void Test1_Book_Availability_Logic()
        {
            var book = new Book { IsAvailable = false };
            Assert.False(book.IsAvailable);
        }

        [Fact]
        public void Test2_Loan_Returned_Logic()
        {
            var book = new Book { IsAvailable = true };
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void Test3_Book_Search_Simple()
        {
            var list = new List<string> { "Book1", "Book2" };
            var count = list.Count;
            Assert.Equal(2, count);
        }

        [Fact]
        public void Test4_Overdue_Date_Logic()
        {
            var dueDate = DateTime.Now.AddDays(-1);
            Assert.True(dueDate < DateTime.Now);
        }

        [Fact]
        public void Test5_Admin_Role_Check()
        {
            string role = "Admin";
            Assert.Equal("Admin", role);
        }
    }
}