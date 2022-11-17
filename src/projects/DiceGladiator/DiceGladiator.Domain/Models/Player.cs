namespace DiceGladiator.Domain.Models
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public int Score { get; private set; }

        public void AddScore (int score) => Score += score;
    }
}
