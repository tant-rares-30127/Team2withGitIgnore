using System;

using Team2Application.Models;
using Xunit;

namespace Team2Tests
{
    public class Team2Tests
    {
        [Fact]
        public void GettingAgeTest()
        {
            // Assume

            Intern intern = new Intern(5, "Rares", new DateTime(2015, 12, 25), "email@yahoo.com");

            // Act

            int age = intern.GetAge();

            // Assert

            Assert.Equal(5, age);
        }
    }
}
