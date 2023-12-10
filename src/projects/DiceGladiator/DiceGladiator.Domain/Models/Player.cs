namespace DiceGladiator.Domain.Models
{
    public class Player
    {
        public string? Name { get; set; }
        public int Score { get; set; }

        public void AddScore (int score) => Score += score;

		public Player Copy() => new Player { Name = Name };

		public override int GetHashCode() => Name?.GetHashCode() ?? 0;

		public override string ToString() => Name ?? string.Empty;

		public override bool Equals(object? obj)
		{
			var other = obj as Player;
			return other?.Name == Name;
		}
	}
}
