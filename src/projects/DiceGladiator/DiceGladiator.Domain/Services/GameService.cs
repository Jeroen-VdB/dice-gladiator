using DiceGladiator.Domain.Models;

namespace DiceGladiator.Domain.Services;

public class GameService
{
	private bool eliteHasPassed;

	///// <summary>
	///// Default constructor, initializes an empty list of players and a new default enemy.
	///// </summary>
	public GameService()
	{
		Players = new List<Player>();
		CurrentEnemy = new Enemy();
	}

	public List<Player> Players { get; set; }
	public Enemy CurrentEnemy { get; set; }
	public int RoundLimit { get; set; } = 10;
	public int CurrentRound { get; set; } = 1;

	/// <summary>
	/// Initialize a new game, starting with a provided list of players.
	/// </summary>
	/// <param name="players">List of players that will fight in the arena.</param>
	public void Start(List<Player> players, int roundLimit)
	{
		Players = players;
		CurrentEnemy = new Enemy();
		eliteHasPassed = false;
		RoundLimit = roundLimit;
		CurrentRound = 1;
	}

	/// <summary>
	/// Add the CurrentEnemy score to the provided player's score and generate a new enemy.
	/// </summary>
	/// <param name="player">The player that won the fight, if null, no player's score will increase and the score will be accumulated to the next enemy.</param>
	public void NextEnemy(Player? player)
	{
		player?.AddScore(CurrentEnemy.TotalScore);

		var previousScore = player == null ? CurrentEnemy.TotalScore : 0;
		CurrentEnemy = new Enemy(previousScore, MakeElite());
		CurrentRound++;
	}

	/// <summary>
	/// Returns true when CurrentRound > RoundLimit, otherwise returns false
	/// </summary>
	/// <returns>Bool if round limit has been reached</returns>
	public bool RoundLimitReached() => CurrentRound > RoundLimit;

	/// <summary>
	/// Override the public properties of this game with the previous ones
	/// </summary>
	/// <param name="previousGameSession">Previous game isntance</param>
	public void ResumePreviousGame(GameService previousGameSession)
	{
		Players = previousGameSession.Players;
		RoundLimit = previousGameSession.RoundLimit;
		CurrentRound = previousGameSession.CurrentRound;
		CurrentEnemy = previousGameSession.CurrentEnemy;
	}

	/// <summary>
	/// Get a list of the current players to initiate a new player list for the next game. Contains 1 empty player when no previous game was started
	/// </summary>
	public List<Player> GetPreviousPlayers()
	{
		return Players?.Any() ?? false
			? new List<Player>(Players.Select(p => p.Copy()).ToList())
			: new List<Player> { new() };
	}

	private bool MakeElite()
	{
		if (!eliteHasPassed)
		{
			var makeElite = new Random().Next(1, 10) == 9;
			eliteHasPassed = makeElite;
			return makeElite;
		}

		return false;
	}
}
