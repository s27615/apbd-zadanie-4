using System;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace LegacyApp.Tests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ReturnsFalseWhenFirstNameIsEmpty()
    {
        
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            null, 
            "Kowalski", 
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );
        
        // Assert.Equal(false, result);
        //Assert.False(result);
        Assert.Equals(false, result);
    }
    
    // AddUser_ReturnsFalseWhenMissingAtSignAndDotInEmail
    // AddUser_ReturnsFalseWhenYoungerThen21YearsOld
    // AddUser_ReturnsTrueWhenVeryImportantClient
    // AddUser_ReturnsTrueWhenImportantClient
    // AddUser_ReturnsTrueWhenNormalClient
    // AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit
    // AddUser_ThrowsExceptionWhenUserDoesNotExist
    // AddUser_ThrowsExceptionWhenUserNoCreditLimitExistsForUser
    
    [Fact]
    public void AddUser_ThrowsArgumentExceptionWhenClientDoesNotExist()
    {
        
        // Arrange
        var userService = new UserService();

        // Act
        TestDelegate action = () => userService.AddUser(
            "Jan", 
            "Kowalski", 
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            100
        );

        // Assert
        Assert.Throws<ArgumentException>(action);
    }
    
    [Fact]
    public void AddUser_ReturnsFalseWhenMissingAtSignAndDotInEmail()
    {
            // Arrange
            var userService = new UserService();

            // Act
            var result = userService.AddUser("Jan", "Kowalski", "invalidemail", DateTime.Parse("1980-01-01"), 1);

            // Assert
            Assert.Equals(false, result);
    }

        [Fact]
        public void AddUser_ReturnsFalseWhenYoungerThan21YearsOld()
        {
            // Arrange
            var userService = new UserService();

            // Act
            var result = userService.AddUser("Jan", "Kowalski", "jan@kowalski.pl", DateTime.Now.AddYears(-20), 1);

            // Assert
            //Assert.False(result);
            Assert.Equals(false, result);
        }

        [Fact]
        public void AddUser_ReturnsTrueWhenVeryImportantClient()
        {
            // Arrange
            var userService = new UserService();
            
            // Act
            var result = userService.AddUser("Jan", "Kowalski", "jan@kowalski.pl", DateTime.Parse("1980-01-01"), 1);

            // Assert
            //Assert.True(result);
            Assert.Equals(true, result);
        }

        [Fact]
        public void AddUser_ReturnsTrueWhenImportantClient()
        {
            // Arrange
            var userService = new UserService();

            // Act
            var result = userService.AddUser("Jan", "Nowak", "jan@nowak.pl", DateTime.Parse("1980-01-01"), 1);

            // Assert
            //Assert.True(result);
            Assert.Equals(true, result);
        }

        [Fact]
        public void AddUser_ReturnsTrueWhenNormalClient()
        {
            // Arrange
            var userService = new UserService();

            // Act
            var result = userService.AddUser("Jan", "Normalny", "jan@normalny.pl", DateTime.Parse("1980-01-01"), 1);

            // Assert
           //Assert.True(result);
           Assert.Equals(true, result);
        }
}