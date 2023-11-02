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
		public string HealthTooltip => $"This is the amount of hit points a gladiator can take. Your total combined dice roll should be {Health} at least to defeat this gladiator.";
		public int Speed { get; set; }
		public string SpeedTooltip => $"This is a quick gladiator. Strong attacks will take too long to execute. Make sure that all your dice rolls are lower than {Speed}.";
		public bool Duo { get; set; }
		public string DuoTooltip => $"As an extra challange, a second gladiator joined the fight. This prevents you from using the same attack twice, meaning you are not allowed to roll the same amount with different dice.";
		public bool Poison { get; set; } //TODO
		public string PoisonTooltip => $"This gladiator's weapon is laced with poison. If you are defeated, you will be limited in the next 3 rounds. This means you will have to exlcude your highest chosen dice from your total hit points combination.";
		public int WeakSpot { get; set; }
		public string WeakSpotTooltip => $"The gladiator is equipped with a shiled. But as you study the gladiator's stance, you notice a weakspot. You need to roll a {WeakSpot} with at least one dice to break his defence.";
		public bool Elite { get; set; }
		public string EliteTooltip => $"The arena champion with {Health} hit points has arrived. You'll probably need a critical hit to defeat this one, good luck! (The champion prevents previous point accumulation.)";
		public int Score { get; set; }
		public string ScoreTooltip => $"In total, this gladiator is worth {Score + PreviousScore} score points. {PreviousScoreTooltip}";
		public string ScoreString => PreviousScore > 0 ? $"{Score + PreviousScore} ({Score} + {PreviousScore})" : $"{Score}";
		public int PreviousScore { get; set; }
		internal string PreviousScoreTooltip => PreviousScore > 0 ? $"The current score of {Score} is accumulated with {PreviousScore} because the previous gladiator was not defeated." : string.Empty;
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
