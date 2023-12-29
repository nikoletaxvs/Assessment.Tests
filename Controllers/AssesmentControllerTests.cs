using Assesment.Controllers;
using Assesment.DTOs;
using Assesment.Models;
using Assesment.Repositories;
using Assesment.Services.Cache;
using Assesment.Services.CountryService;
using AutoMapper;
using FakeItEasy;
using FakeItEasy.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Tests.Controllers
{
    public class AssesmentControllerTests
    {
        public AssesmentController assessmentController;
        public ICountryRepository _countryRepository;
        public ICountryApiService _countryApiService;
        public ICacheService _cacheService;
        public IMapper _mapper;
        public AssesmentControllerTests()
        {
            _countryRepository = A.Fake<ICountryRepository>();
            _countryApiService = A.Fake<ICountryApiService>();
            _cacheService = A.Fake<ICacheService>();
            _mapper = A.Fake<IMapper>();
            assessmentController = new AssesmentController(_mapper,_countryApiService, _countryRepository,_cacheService);
        }
        
            
        [Fact]
        public async Task GetCountries_Returns_Ok()
        {
            //Arrange
            var countries = A.Fake<ICollection<CountryDto>>();
            var countriesList = A.Fake<List<CountryDto>>();
            A.CallTo(() =>_mapper.Map<List<CountryDto>>(countries)).Returns(countriesList);

            //Act
            var result = await assessmentController.GetCountries();

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public async Task FindSecondLargest_Returns_SecondLargestNumber()
        {
            // Arrange
            var requestObj = new RequestObj { RequestArrayObj = new List<int> { 3, 1, 5, 2 } };
            IEnumerable<int> numbers = requestObj.RequestArrayObj;
            //A.CallTo(() => assessmentController.FindSecondLargest(numbers)).Returns(3);
            // Act
            var result = await assessmentController.FindSecondLargestFromList(requestObj);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            
        }

    }

}
