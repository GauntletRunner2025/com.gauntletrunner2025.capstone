using NUnit.Framework;
using Newtonsoft.Json;

public class ValidPortParserTest
{
    [Test]
    public void ParsePort_ValidJson_ReturnsPort()
    {
        // Arrange
        string json = @"{ ""Port"": 8080 }";

        // Act
        var port = PortParser.ParsePort(json);

        // Assert
        Assert.That(port.Value, Is.EqualTo(8080));
    }
}
