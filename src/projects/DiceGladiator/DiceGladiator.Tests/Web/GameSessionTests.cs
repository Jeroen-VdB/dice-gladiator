﻿using Blazored.LocalStorage;
using Bunit;
using DiceGladiator.Domain.Models;
using DiceGladiator.Domain.Services;

namespace DiceGladiator.Tests.Web
{
	public class GameSessionTests : Bunit.TestContext
	{
		private ILocalStorageService _localStorageService;

		[SetUp]
		public void Setup()
		{
			_localStorageService = this.AddBlazoredLocalStorage();
		}

		[Test]
		public async Task StoreAndGetGameService()
		{
			// Arrange
			var players = new List<Player> { new Player { Name = "Player1" } };
			var gameService = new GameService();
			gameService.Start(players, 1000);

			// Act
			await _localStorageService.SetItemAsync("gameSession", gameService);
			var gameSession = await _localStorageService.GetItemAsync<GameService>("gameSession");

			// Assert
			Assert.That(gameSession.Players.Any(), Is.True);
		}
	}
}
