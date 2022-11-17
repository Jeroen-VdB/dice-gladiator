namespace DiceGladiator.Domain.Models
{
    public class Enemy
    {
        /// <summary>
        /// Default constructor, generates a random non-elite enemy withouth a previous score increase.
        /// </summary>
        public Enemy() : this (0, false) {}

        /// <summary>
        /// Generates a random enemy.
        /// </summary>
        /// <param name="previousScore"></param>
        /// <param name="elite"></param>
        public Enemy(int previousScore, bool elite)
        {
            var random = new Random();
            Health = random.Next(4, 40) * (elite ? 10 : 1);
            Speed = IsEnabled(random) ? random.Next(6, 20) : 0;
            WeakSpot = IsEnabled(random) ? random.Next(1, 20) : 0;
            Duo = IsEnabled(random);
            Score = previousScore + Health + (Health - Speed) + WeakSpot + (Duo ? 10 : 0);
            Elite = elite;
        }

        public int Health { get; set; }
        public int Speed { get; set; }
        public bool Duo { get; set; }
        public int WeakSpot { get; set; }
        public bool Elite { get; set; }
        public int Score { get; set; }

        private bool IsEnabled(Random random) => random.Next(1, 10) > 8;
    }
}
