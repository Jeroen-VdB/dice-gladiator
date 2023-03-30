using DiceGladiator.Domain.Models;
using DiceGladiator.Domain.Services;

namespace DiceGladiator.Tests.Domain.Services
{
	public class GameServiceTests
	{
		private const int ScoreLimit = 10;

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void NextEnemyMaxHealth()
		{
			GameService gameService = new GameService();
			List<Player> _players = new List<Player> { new Player { Name = "Player1", Score = 10 } };
			gameService.Start(_players, ScoreLimit);

			for (int i = 0; i < 1000; i++)
			{
				gameService.NextEnemy(_players.FirstOrDefault());
				Assert.That(gameService.CurrentEnemy.Health, Is.LessThan(40));
			}
		}

		[Test]
		public void ResumePreviousGameTest()
		{
			GameService gameService = new GameService();
			List<Player> _players = new List<Player> { new Player { Name = "Player1", Score = 10 } };
			gameService.Start(_players, ScoreLimit);
			var newSession = new GameService();

			newSession.ResumePreviousGame(gameService);

			Assert.That(newSession.Players.First().Name, Is.EqualTo("Player1"));
			Assert.That(newSession.Players.First().Score, Is.EqualTo(10));
		}

		[Test]
		public void GetPreviousPlayersTest()
		{
			GameService gameService = new GameService();
			List<Player> _players = new List<Player> { new Player { Name = "Player1", Score = 10 } };
			gameService.Start(_players, ScoreLimit);

			var previousPlayers = gameService.GetPreviousPlayers();

			Assert.That(previousPlayers.First().Name, Is.EqualTo("Player1"));
			Assert.That(previousPlayers.First().Score, Is.EqualTo(0));
		}
	}
}
