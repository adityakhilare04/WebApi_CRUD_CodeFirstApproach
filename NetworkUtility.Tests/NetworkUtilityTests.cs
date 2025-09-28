using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Tests
{
    
    public class NetworkUtilityTests
    {
        [Fact]
        //Function Naming convention --> classname_methodname_expectedbehavior
        public void NetworkUtility_GetNetworkInfo_ReturnsExpectedString() 
        {
            // Arrange
            NetworkUtility networkUtility = new NetworkUtility();
            string expected = "Network Info";

            // Act
            var result = networkUtility.GetNetworkInfo();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NetworkUtility_Add_ReturnsCorrectSum()
        {
            //Arrange
            NetworkUtility networkUtility = new NetworkUtility();
            int a = 5;
            int b = 10;
            int expectedSum = 15;
            // Act
            var result = networkUtility.Add(a, b);
            // Assert
            Assert.Equal(expectedSum, result);
        }

        [Theory]
        [InlineData(5, 10, 15)]
        [InlineData(-5, -10, -15)]
        [InlineData(10, -5, 5)]
        public void NetworkUtility_Add_ExpectsCorrectSum(int a, int b, int expectedResult)
        {
            //Arrange
            NetworkUtility networkUtility = new NetworkUtility();

            // Act
            var result = networkUtility.Add(a, b);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
