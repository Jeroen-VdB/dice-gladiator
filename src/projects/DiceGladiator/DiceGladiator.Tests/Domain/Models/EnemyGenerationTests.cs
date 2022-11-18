using DiceGladiator.Domain.Models;

namespace DiceGladiator.Tests.Domain.Models
{
	public class EnemyGenerationTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void MaxHealth()
		{
			for (int i = 0; i < 1000; i++)
			{
				var enemy = new Enemy();
				Assert.That(enemy.Health, Is.LessThan(40));
			}
		}


		[Test]
		public void ScoreTests()
		{
			var enemy = new Enemy
			{
				Health = 10
			};

			Assert.That(enemy.Score, Is.EqualTo(10));
		}
	}
}
