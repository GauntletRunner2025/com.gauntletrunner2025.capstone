using NUnit.Framework;
using Newtonsoft.Json;
using UnityEngine.TestTools;
using UnityEngine;

public class DefaultPortParserTest
{
    [Test]
    public void ParsePort_DefaultValue_LogsWarning()
    {
        // Arrange
        string json = @"{ ""Port"": 0 }";

        // Act
        LogAssert.Expect(LogType.Warning, "Port value is 0 (default). This may indicate a configuration issue.");
        var port = PortParser.ParsePort(json);

        // Assert
        Assert.That(port.Value, Is.EqualTo(0));
    }
}
