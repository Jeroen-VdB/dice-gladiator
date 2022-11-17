namespace DiceGladiator.Domain.Models
{
    public class Player
    {
        public string? Name { get; set; }
        public int Score { get; set; }

        public void AddScore (int score) => Score += score;
    }
}
