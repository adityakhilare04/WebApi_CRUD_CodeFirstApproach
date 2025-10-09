using FakeItEasy;
using NetworkUtility.Interface;

namespace NetworkUtility.Tests;

public class NetworkUtilityTests
{
    private readonly NetworkUtility _networkUtility;
    private readonly IDnsRepository _dnsRepository;
    public NetworkUtilityTests()
    {
        _dnsRepository = A.Fake<IDnsRepository>();
        _networkUtility = new NetworkUtility(_dnsRepository);
    }

    [Fact]
    //Function Naming convention --> classname_methodname_expectedbehavior
    public void NetworkUtility_GetNetworkInfo_ReturnsExpectedString() 
    {
        // Arrange
        string expected = "Network Info";

        // Act
        var result = _networkUtility.GetNetworkInfo();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void NetworkUtility_Add_ReturnsCorrectSum()
    {
        //Arrange
        int a = 5;
        int b = 10;
        int expectedSum = 15;
        // Act
        var result = _networkUtility.Add(a, b);
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

        // Act
        var result = _networkUtility.Add(a, b);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void NetworkUtility_GetDateTime_ReturnsCurrentDateTime()
    {
        // Arrange
        DateTime beforeCall = DateTime.Now;
        // Act
        var result = _networkUtility.GetDateTime();
        DateTime afterCall = DateTime.Now;
        // Assert
        Assert.True(result >= beforeCall && result <= afterCall);
        Assert.InRange(result, beforeCall, afterCall);
        Assert.IsType<DateTime>(result);
    }

    [Fact]
    public void NetworkUtility_GetEmployee_ReturnsExpectedEmployee()
    {
        // Arrange
        int expectedId = 1;
        string expectedName = "Virat Kohli";
        // Act
        var result = _networkUtility.GetEmployee();
        // Assert
        Assert.NotNull(result);
        Assert.IsType<Employee>(result);
        Assert.Equal(expectedId, result.Id);
        Assert.Equal(expectedName, result.Name);
    }

    [Fact]
    public void NetworkUtility_GetStatus_ReturnsSuccessWhenDnsRepositoryReturnsTrue()
    {
        // Arrange
        A.CallTo(() => _dnsRepository.GetStatus()).Returns(true);
        string expected = "Success";
        // Act
        var result = _networkUtility.GetStatus();
        // Assert
        Assert.Equal(expected, result);
    }
}
