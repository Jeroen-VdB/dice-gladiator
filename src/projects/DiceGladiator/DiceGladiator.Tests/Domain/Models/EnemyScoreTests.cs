using DiceGladiator.Domain.Models;

namespace DiceGladiator.Tests.Domain.Models
{
	public class EnemyScore
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ScoreHealthOnlyTest()
		{
			var enemy = new Enemy();
			enemy.Health = 10;
			enemy.Speed = 0;
			enemy.Duo = false;
			enemy.WeakSpot = 0;

			enemy.CalculateScore();

			Assert.That(enemy.Score, Is.EqualTo(10));
		}

		[Test]
		public void ScoreHealthAndSpeedTest()
		{
			var enemy = new Enemy();
			enemy.Health = 15;
			enemy.Speed = 10;
			enemy.Duo = false;
			enemy.WeakSpot = 0;

			enemy.CalculateScore();

			Assert.That(enemy.Score, Is.EqualTo(20));
		}

		[Test]
		public void ScoreHealthAndDuoTest()
		{
			var enemy = new Enemy();
			enemy.Health = 15;
			enemy.Speed = 0;
			enemy.Duo = true;
			enemy.WeakSpot = 0;

			enemy.CalculateScore();

			Assert.That(enemy.Score, Is.EqualTo(25));
		}

		[Test]
		public void ScoreEliteHealthOnlyTes()
		{
			var enemy = new Enemy(0, true);

			Assert.That(enemy.Elite, Is.True);
		}

		[Test]
		public void ScoreHealthAndPreviousScoreTest()
		{
			var enemy = new Enemy(10, false);
			enemy.Health = 10;
			enemy.Speed = 0;
			enemy.Duo = false;
			enemy.WeakSpot = 0;

			enemy.CalculateScore();

			Assert.That(enemy.Score, Is.EqualTo(10));
			Assert.That(enemy.PreviousScore, Is.EqualTo(10));
			Assert.That(enemy.TotalScore, Is.EqualTo(20));
		}
	}
}
