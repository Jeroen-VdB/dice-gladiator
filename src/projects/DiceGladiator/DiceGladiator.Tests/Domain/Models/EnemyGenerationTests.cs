using DiceGladiator.Domain.Models;

namespace DiceGladiator.Tests.Domain.Models;

public class EnemyGenerationTests
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void MaxHealthTest()
	{
		for (int i = 0; i < 1000; i++)
		{
			var enemy = new Enemy();
			Assert.That(enemy.Health, Is.LessThan(60));
		}
	}

	[Test]
	public void MaxHealthWithSpecificSpeedTest()
	{
		var enemy = new EnemyTestBuilder().WithSpeed(6).WithMinHealth(30).WithMaxHealth(60).Build();
		Assert.That(enemy.Health, Is.EqualTo(30));
	}
}
