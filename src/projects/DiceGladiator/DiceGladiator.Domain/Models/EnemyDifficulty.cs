namespace DiceGladiator.Domain.Models;

/// <summary>
/// Determin enemy attribute boundaries and enabled ratio to change the difficulty. Also useful to manipulate for unit testing.
/// </summary>
public class EnemyDifficulty
{
	public int MinHealth { get; set; } = 6;
	public int MaxHealth { get; set; } = 60;
	public int MinSpeed { get; set; } = 6;
	public int MaxSpeed { get; set; } = 20;
	public int SpeedRate { get; set; } = 5;
	public int MinWeakSpot { get; set; } = 1;
	public int MaxWeakSpot { get; set; }	= 21;
	public int WeakSpotRate { get; set; } = 6;
	public int DuoRate { get; set; }	= 7;
	public int PoisonRate { get; set; } = 7;
}
