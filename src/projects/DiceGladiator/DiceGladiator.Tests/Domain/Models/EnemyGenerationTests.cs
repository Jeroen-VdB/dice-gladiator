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
	public void MaxHealthWithSpecifiedSpeedTest()
	{
		var enemy = new EnemyTestBuilder().WithSpeed(6).WithMinHealth(30).WithMaxHealth(60).Build();
		Assert.That(enemy.Health, Is.EqualTo(30));
	}

	[Test]
	public void MinHealthTest()
	{
		var enemy = new EnemyTestBuilder().WithHealth(1).WithHealthDividerRate(3).Build();
		Assert.That(enemy.Health, Is.EqualTo(6));
		Assert.That(enemy.Score, Is.EqualTo(6));
	}

	[Test]
	public void MinHealthWithSpecifiedSpeedTest()
	{
		var enemy = new EnemyTestBuilder().WithMinHealth(6).WithSpeed(7).Build();
		Assert.That(enemy.Health, Is.EqualTo(7));
	}
}
