using System.ComponentModel;

namespace DiceGladiator.Domain.Models
{
	public class Enemy
	{
		/// <summary>
		/// Default constructor, generates a random non-elite enemy withouth a previous score increase.
		/// </summary>
		public Enemy() : this(0, false) { }

		/// <summary>
		/// Generates a random enemy.
		/// </summary>
		/// <param name="previousScore"></param>
		/// <param name="elite"></param>
		public Enemy(int previousScore, bool elite)
		{
			var random = new Random();
			Health = random.Next(4, 40) * (elite ? 10 : 1);
			Speed = IsEnabled(random, 5) ? random.Next(6, 20) : 0;

			if (Speed > Health)
			{
				Speed = 0;
			}

			WeakSpot = IsEnabled(random, 6) ? random.Next(1, 20) : 0;
			Duo = IsEnabled(random, 8);
			Elite = elite;
			PreviousScore = previousScore;

			DisplayHint = random.Next(0, 4);

			CalculateScore();
		}

		public int DisplayHint { get; set; }
		public int Health { get; set; }
		public string HealthTooltip => $"The gladiator's health indicates the total hit points it can take. To defeat this gladiator, your combined dice roll should be at least {Health}.";
		public int Speed { get; set; }
		public string SpeedTooltip => $"This gladiator is quick. Strong attacks may take too long to execute. Ensure that all your dice rolls are lower than {Speed} to maintain the advantage.";
		public bool Duo { get; set; }
		public string DuoTooltip => $"This gladiator is accompanied by another, adding an extra challenge. You are not allowed to use the same attack twice, meaning you cannot roll the same number with different dice.";
		public bool Poison { get; set; } //TODO
		public string PoisonTooltip => $"Beware! This gladiator's weapon is laced with poison. If defeated, you will be limited in the next 3 rounds, excluding your highest chosen dice from your total hit points combination.";
		public int WeakSpot { get; set; }
		public string WeakSpotTooltip => $"This gladiator carries a shield, but you notice a weak spot in their stance. Roll a {WeakSpot} with at least one dice to break their defence.";
		public bool Elite { get; set; }
		public string EliteTooltip => $"The arena champion has arrived with {Health} hit points. A critical hit might be necessary to defeat them. Good luck! (Note: The champion prevents previous point accumulation.)";
		public int Score { get; set; }
		public string ScoreTooltip => $"This gladiator is worth {Score + PreviousScore} score points in total. {PreviousScoreTooltip}";
		public string ScoreString => PreviousScore > 0 ? $"{Score + PreviousScore} ({Score} + {PreviousScore})" : $"{Score}";
		public int PreviousScore { get; set; }
		internal string PreviousScoreTooltip => PreviousScore > 0 ? $"The current score of {Score} includes an accumulation of {PreviousScore} points from the previous undefeated gladiator." : string.Empty;
		public int TotalScore => Score + PreviousScore;

		/// <summary>
		/// Calculate the enemy score based on its properties
		/// </summary>
		public void CalculateScore()
        {
			Score = Health + SpeedBonus + WeakSpotBonus + DuoBonus;
		}

        private int SpeedBonus => Speed > 0 ? Health - Speed : 0;
        private int WeakSpotBonus => WeakSpot;
        private int DuoBonus => Duo ? 10 : 0;

		private bool IsEnabled(Random random, int rate) => random.Next(1, 10) > rate;
    }
}
