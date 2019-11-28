using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application;
using Application.Models.DataTransferObjects;
using Application.Models.Entities;
using Application.Repositories;
using Application.Services;
using Application.Utility.Models;
using ApplicationModulTests.TestUtility;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace ApplicationIntegerationsTests
{
    public class UserServiceTest : IClassFixture<WebApplicationFactory<StartupDevelopment>>
    {
        private readonly WebApplicationFactory<StartupDevelopment> _factory;

        public UserServiceTest(WebApplicationFactory<StartupDevelopment> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Health_ping()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync("/api/health/ping");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GEt_Health_ping()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/health/ping");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var actual = JsonConvert.DeserializeObject<MessageObj>(await response.Content.ReadAsStringAsync());
            Assert.Equal("pong!", actual.Message);
        }

        [Fact]
        public async Task Get_AllUsers()
        {
            // Arrange
            Environment.SetEnvironmentVariable("USERS_SERVICE_HTTP", "users-service.api.converge-app.net");

            var client = _factory.CreateClient();

            var httpClient = new HttpClient();
            var authUser = await AuthUtility.GenerateAndAuthenticate(httpClient);

            AuthUtility.AddAuthorization(client, authUser.Token);
            // Act
            var response = await client.GetAsync("/api/Users");

            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_Users()
        {
            // Arrange
            Environment.SetEnvironmentVariable("USERS_SERVICE_HTTP", "users-service.api.converge-app.net");

            var client = _factory.CreateClient();
            UserCreationDto user = new UserCreationDto
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString() + "test@gmail.com",

            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Users", user);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            var actual = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(actual);


        }

        [Fact]
        public async Task Get_UsersById()
        {
            // Arrange
            Environment.SetEnvironmentVariable("USERS_SERVICE_HTTP", "users-service.api.converge-app.net");

            var client = _factory.CreateClient();

            var httpClient = new HttpClient();
            var authUser = await AuthUtility.GenerateAndAuthenticate(httpClient);

            AuthUtility.AddAuthorization(client, authUser.Token);
            // Act
            var response = await client.GetAsync("/api/Users" + authUser.Id);

            Assert.NotNull(HttpStatusCode.OK);


        }


        [Fact]
        public async Task Update_UsersById()
        {
            // Arrange
            Environment.SetEnvironmentVariable("USERS_SERVICE_HTTP", "users-service.api.converge-app.net");

            var client = _factory.CreateClient();

            var httpClient = new HttpClient();
            var authUser = await AuthUtility.GenerateAndAuthenticate(httpClient);

            AuthUtility.AddAuthorization(client, authUser.Token);

            UserUpdateDto updateUser = new UserUpdateDto
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Username = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            // Act

            var response = await client.PutAsJsonAsync("/api/Users" + authUser.Id, updateUser);

            //Assert
            Assert.NotNull(response.StatusCode);

        }



        [Fact]
        public async Task Delete_UsersById()
        {
            // Arrange
            Environment.SetEnvironmentVariable("USERS_SERVICE_HTTP", "users-service.api.converge-app.net");

            var client = _factory.CreateClient();

            var httpClient = new HttpClient();
            var authUser = await AuthUtility.GenerateAndAuthenticate(httpClient);

            AuthUtility.AddAuthorization(client, authUser.Token);
            // Act
            var response = await client.DeleteAsync("/api/Users" + authUser.Id);

            Assert.NotNull(HttpStatusCode.OK);

        }

        [Fact]
        public async Task Get_ByEmail()
        {
            // Arrange
            Environment.SetEnvironmentVariable("USERS_SERVICE_HTTP", "users-service.api.converge-app.net");

            var client = _factory.CreateClient();

            var httpClient = new HttpClient();
            var authUser = await AuthUtility.GenerateAndAuthenticate(httpClient);

            AuthUtility.AddAuthorization(client, authUser.Token);
            UserDto sa = new UserDto();
            // Act
            var response = await client.DeleteAsync("/api/Users" + sa.Email);

            Assert.NotNull(HttpStatusCode.OK);


        }





    }
}