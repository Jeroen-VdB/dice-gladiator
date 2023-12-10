using System.ComponentModel;

namespace DiceGladiator.Domain.Models;

public class Enemy
{
	/// <summary>
	/// Default constructor, generates a random non-elite enemy withouth a previous score increase at the beginning of the game.
	/// </summary>
	public Enemy() : this(0, false) { }

	/// <summary>
	/// Generates a random enemy for the next round of the game.
	/// </summary>
	/// <param name="previousScore">GameService has to pass on the previous enemy score because the Enemy is stateless every new round</param>
	/// <param name="elite">GameSerice decides on an Elite enemy or not to ensure only 1 elite per game is generated</param>
	public Enemy(int previousScore, bool elite) : this (previousScore, elite, new EnemyDifficulty()) { }

	/// <summary>
	/// Generates a determined enemy. Useful for unit testing.
	/// </summary>
	public Enemy(int previousScore, bool elite, EnemyDifficulty enemyDifficulty)
	{
		var random = new Random();

		PreviousScore = previousScore;
		Elite = elite;
		Speed = IsEnabled(random, enemyDifficulty.SpeedRate) ? random.Next(enemyDifficulty.MinSpeed, enemyDifficulty.MaxSpeed) : 0;
		WeakSpot = IsEnabled(random, enemyDifficulty.WeakSpotRate) ? random.Next(enemyDifficulty.MinWeakSpot, enemyDifficulty.MaxWeakSpot) : 0;
		Duo = IsEnabled(random, enemyDifficulty.DuoRate);
		Poison = IsEnabled(random, enemyDifficulty.PoisonRate);
		Health = GetHealth(random, enemyDifficulty.MinHealth, enemyDifficulty.MaxHealth, enemyDifficulty.HealthDividerRate);
		DisplayHint = random.Next(0, 5);

		CalculateScore();
	}

	public int DisplayHint { get; private set; }
	public int Health { get; private set; }
	public string HealthTooltip => $"The gladiator's health indicates the total hit points it can take. To defeat this gladiator, your combined dice roll should be at least {Health}.";
	public int Speed { get; private set; }
	public string SpeedTooltip => $"This gladiator is quick. Strong attacks may take too long to execute. Ensure that all your dice rolls are lower than {Speed} to maintain the advantage.";
	public bool Duo { get; private set; }
	public string DuoTooltip => $"This gladiator is accompanied by another, adding an extra challenge. You are not allowed to use the same attack twice, meaning you cannot roll the same number with different dice.";
	public bool Poison { get; private set; }
	public string PoisonTooltip => $"Beware! This gladiator's weapon is laced with poison. If you are defeated, you will be limited in the next 3 rounds, excluding your highest chosen dice from your total hit points combination. (Note: if someone else defeated the gladiator before it was your turn, you do not get poisoned.)";
	public int WeakSpot { get; private set; }
	public string WeakSpotTooltip => $"This gladiator carries a shield, but you notice a weak spot in their stance. Roll a {WeakSpot} with at least one dice to break their defence.";
	public bool Elite { get; private set; }
	public string EliteTooltip => $"The arena champion has arrived with {Health} hit points. A critical hit might be necessary to defeat them. Good luck! (Note: The champion prevents previous point accumulation.)";
	public int Score { get; private set; }
	public string ScoreTooltip => $"This gladiator is worth {Score + PreviousScore} score points in total. {PreviousScoreTooltip}";
	public string ScoreString => PreviousScore > 0 ? $"{Score + PreviousScore} ({Score} + {PreviousScore})" : $"{Score}";
	public int PreviousScore { get; private set; }
	internal string PreviousScoreTooltip => PreviousScore > 0 ? $"The current score of {Score} includes an accumulation of {PreviousScore} points from the previous undefeated gladiator." : string.Empty;
	public int TotalScore => Score + PreviousScore;

	/// <summary>
	/// Calculate the enemy score based on its properties
	/// </summary>
	public void CalculateScore()
	{
		Score = Health + SpeedBonus + WeakSpotBonus + DuoBonus + PoisonBonus;
	}

	private int SpeedBonus => Speed > 0 ? 20 : 0;
	private int WeakSpotBonus => (int)Math.Round((double)WeakSpot / 60 * 200);
	private int DuoBonus => Duo ? 20 : 0;
	private int PoisonBonus => Poison ? 20 : 0;

	// Determin a rate on how often the attribute should be applied
	private bool IsEnabled(Random random, int rate) => random.Next(1, 10) > rate;

	public int GetHealth(Random random, int minHealth, int maxHealth, int healthDividerRate)
	{
		var maxHealthPossibilities = new List<int>
		{
			maxHealth,
			Speed > 0 ? ReduceHealthBySpeed(maxHealth) : maxHealth,
			WeakSpot > 0 ? 56 : maxHealth
		};
		maxHealth = maxHealthPossibilities.Min();

		var dividedHealthWithEliteModifier = (random.Next(minHealth, maxHealth) * (Elite ? 10 : 1)) / random.Next(1, healthDividerRate);

		return Math.Max(Math.Max(6, dividedHealthWithEliteModifier), Speed);
	}

	private int ReduceHealthBySpeed(int health)
	{
		foreach (int diceMaxAmount in ((DiceSet[])Enum.GetValues(typeof(DiceSet))).Select(v => (int)v))
		{
			if (diceMaxAmount > Speed)
			{
				health -= diceMaxAmount - (Speed - 1);
			}
		}

		return health;
	}
}
