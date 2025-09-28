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
        public void NetworkUtility_GetNetworkInfo_ReturnsExpectedString()
        {
            // Arrange
            NetworkUtility networkUtility = new NetworkUtility();
            string expected = "Network Info";

            // Act
            var result = NetworkUtility.GetNetworkInfo();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
