using NUnit.Framework;
using Newtonsoft.Json;

public class InvalidPortParserTest
{
    [Test]
    public void ParsePort_InvalidJson_ThrowsException()
    {
        // Arrange
        string json = @"{ ""Port"": not_a_number }";

        // Act & Assert
        Assert.Throws<JsonException>(() => PortParser.ParsePort(json));
    }
}
