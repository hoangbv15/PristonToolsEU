using System.Text.Json;
using PristonToolsEU.BossTiming.Dto;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        using (StreamReader r = new StreamReader("bossProps.json"))
        {
            var json = r.ReadToEnd();
            var boss = JsonSerializer.Deserialize<Boss>(json);
            Assert.NotNull(boss);
            Assert.Equals(boss.Name, "Valento");
            Assert.Equals(boss.ReferenceHour, 1);
            Assert.Equals(boss.IntervalHours, 2);
        }
    }
}