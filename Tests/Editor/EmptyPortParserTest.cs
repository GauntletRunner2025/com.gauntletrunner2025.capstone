using NUnit.Framework;
using Newtonsoft.Json;

public class EmptyPortParserTest
{
    [Test]
    public void ParsePort_EmptyJson_ThrowsException()
    {
        // Arrange
        string json = "";

        // Act & Assert
        var ex = Assert.Throws<JsonException>(() => PortParser.ParsePort(json));
        Assert.That(ex.Message, Is.EqualTo("JSON content is null or empty"));
    }
}
