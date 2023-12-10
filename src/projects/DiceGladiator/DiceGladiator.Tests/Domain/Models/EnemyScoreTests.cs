using DiceGladiator.Domain.Models;

namespace DiceGladiator.Tests.Domain.Models;

public class EnemyScore
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void ScoreHealthOnlyTest()
	{
		var enemy = new EnemyTestBuilder().WithHealth(10).Build();

		enemy.CalculateScore();

		Assert.That(enemy.Score, Is.EqualTo(10));
	}

	[Test]
	public void ScoreHealthAndWeakSpotTest()
	{
		var enemy = new EnemyTestBuilder().WithWeakSpot(10).WithHealth(40).Build();

		enemy.CalculateScore();

		Assert.That(enemy.Score, Is.EqualTo(73));
	}

	[Test]
	public void ScoreHealthAndSpeedTest()
	{
		var enemy = new EnemyTestBuilder().WithSpeed(10).WithMinHealth(46).Build();

		enemy.CalculateScore();

		Assert.That(enemy.Score, Is.EqualTo(66));
	}

	[Test]
	public void ScoreHealthAndDuoTest()
	{
		var enemy = new EnemyTestBuilder().IsDuo().WithHealth(15).Build();

		enemy.CalculateScore();

		Assert.That(enemy.Score, Is.EqualTo(35));
	}

	[Test]
	public void ScoreHealthAndPoisonTest()
	{
		var enemy = new EnemyTestBuilder().IsPoison().WithHealth(15).Build();

		enemy.CalculateScore();

		Assert.That(enemy.Score, Is.EqualTo(35));
	}

	[Test]
	public void ScoreEliteHealthOnlyTest()
	{
		var enemy = new EnemyTestBuilder().IsElite().Build();

		Assert.That(enemy.Elite, Is.True);
	}

	[Test]
	public void ScoreHealthAndPreviousScoreTest()
	{
		var enemy = new EnemyTestBuilder().WithPreviousScore(10).WithHealth(11).Build();

		Assert.That(enemy.Score, Is.EqualTo(11));
		Assert.That(enemy.PreviousScore, Is.EqualTo(10));
		Assert.That(enemy.TotalScore, Is.EqualTo(21));
	}
}
