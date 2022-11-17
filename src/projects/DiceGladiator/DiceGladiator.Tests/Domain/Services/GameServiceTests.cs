using DiceGladiator.Domain.Models;
using DiceGladiator.Domain.Services;

namespace DiceGladiator.Tests.Domain.Services
{
	public class GameServiceTests
	{
		private const int ScoreLimit = 1000;
		private GameService _gameService;
		private List<Player> _players = new List<Player> { new Player { Name = "Player1", Score = 10 } };

		[SetUp]
		public void Setup()
		{
			_gameService = new GameService();
			_gameService.Start(_players, ScoreLimit);
		}

		[Test]
		public void NextEnemyMaxHealth()
		{
			for (int i = 0; i < 1000; i++)
			{
				_gameService.NextEnemy(_players.FirstOrDefault());
				Assert.That(_gameService.CurrentEnemy.Health, Is.LessThan(40));
			}
		}

		[Test]
		public void ResumePreviousGameTest()
		{
			var newSession = new GameService();

			newSession.ResumePreviousGame(_gameService);

			Assert.That(newSession.Players.First().Name, Is.EqualTo("Player1"));
			Assert.That(newSession.Players.First().Score, Is.EqualTo(10));
		}
	}
}
