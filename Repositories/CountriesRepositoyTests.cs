using Assesment.Data;
using Assesment.Models;
using Assesment.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Tests.Repositories
{
    public class CountriesRepositoyTests
    {

        [Fact]
        public void AddCountry_ValidCountry_CallsSaveChanges()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var countryRepository = new CountryRepository(dbContext);

            var country = new Country
            {
                CommonName = "TestCountry",
                Capital = "TestCapital"
            };

            // Act
            countryRepository.AddCountry(country);

            // Assert
            var addedCountry = dbContext.Countries.FirstOrDefault(c => c.CommonName == "TestCountry");
            Assert.NotNull(addedCountry);
            Assert.Equal("TestCapital", addedCountry.Capital);
        }

        [Fact]
        public void AddCountry_CountryExists_DoesNotCallSaveChanges()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var countryRepository = new CountryRepository(dbContext);

            var existingCountry = new Country
            {
                CommonName = "ExistingCountry",
                Capital = "ExistingCapital"
            };

            dbContext.Countries.Add(existingCountry);
            dbContext.SaveChanges();

            var country = new Country
            {
                CommonName = "ExistingCountry",
                Capital = "TestCapital"
            };

            // Act
            countryRepository.AddCountry(country);

            // Assert
            Assert.Equal("ExistingCapital", existingCountry.Capital);
        }

        // Additional test methods for other repository functions...

        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
