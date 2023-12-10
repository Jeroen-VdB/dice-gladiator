using DiceGladiator.Domain.Models;

namespace DiceGladiator.Tests.Domain.Models;

public class EnemyTestBuilder
{
	private readonly EnemyDifficulty _enemyDifficulty;
	private bool _elite = false;
	private int _previousScore = 0;

	public EnemyTestBuilder()
	{
		_enemyDifficulty = new EnemyDifficulty();
		_enemyDifficulty.SpeedRate = 10;
		_enemyDifficulty.WeakSpotRate = 10;
		_enemyDifficulty.PoisonRate = 10;
		_enemyDifficulty.DuoRate = 10;
	}

	public EnemyTestBuilder WithMinHealth(int health)
	{
		_enemyDifficulty.MinHealth = health;

		return this;
	}

	public EnemyTestBuilder WithMaxHealth(int health)
	{
		_enemyDifficulty.MaxHealth = health;

		return this;
	}

	public EnemyTestBuilder WithHealth(int health)
	{
		_enemyDifficulty.MinHealth = health;
		_enemyDifficulty.MaxHealth = health;

		return this;
	}

	public EnemyTestBuilder WithSpeed(int speed)
	{
		_enemyDifficulty.SpeedRate = 0;
		_enemyDifficulty.MinSpeed = speed;
		_enemyDifficulty.MaxSpeed = speed;

		return this;
	}

	public EnemyTestBuilder WithWeakSpot(int weakspot)
	{
		_enemyDifficulty.WeakSpotRate = 0;
		_enemyDifficulty.MinWeakSpot = weakspot;
		_enemyDifficulty.MaxWeakSpot = weakspot;

		return this;
	}

	public EnemyTestBuilder IsPoison()
	{
		_enemyDifficulty.PoisonRate = 0;

		return this;
	}

	public EnemyTestBuilder IsDuo()
	{
		_enemyDifficulty.DuoRate = 0;

		return this;
	}

	public EnemyTestBuilder IsElite()
	{
		_elite = true;
		return this;
	}

	public EnemyTestBuilder WithPreviousScore(int previousScore)
	{
		_previousScore = previousScore;
		return this;
	}

	public Enemy Build()
	{
		return new Enemy(_previousScore, _elite, _enemyDifficulty);
	}
}
