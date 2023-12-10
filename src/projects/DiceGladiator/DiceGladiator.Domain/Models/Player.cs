namespace DiceGladiator.Domain.Models;

public class Player
{
	public string? Name { get; set; }
	public int Score { get; set; }
	public bool Poisoned { get; set; } = false;
	public int PoisonedOver { get; set; } = 0;

	public void AddScore(int score) => Score += score;

	public Player Copy() => new Player { Name = Name };

	public override int GetHashCode() => Name?.GetHashCode() ?? 0;

	public override string ToString() => Name ?? string.Empty;

	public void NextRound()
	{
		if (Poisoned)
		{
			PoisonedOver = 3;
		}
		else
		{
			PoisonedOver = Math.Max(0, PoisonedOver - 1);
		}

		Poisoned = false;
	}

	public override bool Equals(object? obj)
	{
		var other = obj as Player;
		return other?.Name == Name;
	}
}
