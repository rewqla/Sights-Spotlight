﻿using Newtonsoft.Json;
using NUnit.Framework;
using StoreBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SightsSpotlight.Test.Api.Test
{
    public class CountryControllerTest
    {
        private HttpClient _httpClient;
        private const string RequestUri = "api/country/";

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7188/");
        }

        [Test]
        public async Task CountryController_GetAll_ReturnsOK()
        {
            //arrange

            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri);

            // assert
            httpResponse.EnsureSuccessStatusCode();

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task CountryController_GetCountryById_ReturnsOK()
        {
            //arrange
            var countryId = 3;

            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri + countryId);

            // assert
            httpResponse.EnsureSuccessStatusCode();

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task CountryController_GetCountryById_ReturnsBadRequest()
        {
            //arrange
            var countryId = 1099;

            // act
            var httpResponse = await _httpClient.GetAsync(RequestUri + countryId);

            // assert
            httpResponse.EnsureSuccessStatusCode();

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
