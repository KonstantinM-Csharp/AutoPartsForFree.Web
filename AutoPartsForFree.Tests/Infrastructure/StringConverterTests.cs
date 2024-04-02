using AutoPartsForFree.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsForFree.Tests.Infrastructure
{
    public class StringConverterTests
    {
        [Fact]
        public void ParseCount_ReturnsExpectedResult_WhenCountStringIsNumeric()
        {
            // Arrange
            string countString = "123";
            int expectedResult = 123;

            // Act
            int result = StringConverter.ParseCount(countString);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseCount_ReturnsExpectedResult_WhenCountStringContainsHyphen()
        {
            // Arrange
            string countString = "10-20";
            int expectedResult = 20;

            // Act
            int result = StringConverter.ParseCount(countString);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseCount_ReturnsExpectedResult_WhenCountStringContainsGreaterThanSign()
        {
            // Arrange
            string countString = ">10";
            int expectedResult = 10;

            // Act
            int result = StringConverter.ParseCount(countString);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseCount_ReturnsExpectedResult_WhenCountStringContainsLessThanSign()
        {
            // Arrange
            string countString = "<13";
            int expectedResult = 13;

            // Act
            int result = StringConverter.ParseCount(countString);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void RemoveSpecialCharactersAndToUpper_ReturnsExpectedResult_WhenInputContainsSpecialCharacters()
        {
            // Arrange
            string input = "ABcd123!@#";
            string expectedResult = "ABCD123";

            // Act
            string result = StringConverter.RemoveSpecialCharactersAndToUpper(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void RemoveSpecialCharactersAndToUpper_ReturnsExpectedResult_WhenInputIsMixedCase()
        {
            // Arrange
            string input = "Test123";
            string expectedResult = "TEST123";

            // Act
            string result = StringConverter.RemoveSpecialCharactersAndToUpper(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void RemoveSpecialCharactersAndToUpper_ReturnsEmptyString_WhenInputContainsOnlySpecialCharacters()
        {
            // Arrange
            string input = "!!!";
            string expectedResult = "";

            // Act
            string result = StringConverter.RemoveSpecialCharactersAndToUpper(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
