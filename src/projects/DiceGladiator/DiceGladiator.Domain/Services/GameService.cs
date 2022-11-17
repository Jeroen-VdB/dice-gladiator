using DiceGladiator.Domain.Models;

namespace DiceGladiator.Domain.Services
{
    public class GameService
    {
        private bool eliteHasPassed;

        ///// <summary>
        ///// Default constructor, initializes an empty list of players and a new default enemy.
        ///// </summary>
        public GameService()
        {
            Players = new List<Player>();
            CurrentEnemy = new Enemy();
        }

        public List<Player> Players { get; set; }
        public Enemy CurrentEnemy { get; set; }
        public int ScoreLimit { get; set; }

        /// <summary>
        /// Initialize a new game, starting with a provided list of players.
        /// </summary>
        /// <param name="players">List of players that will fight in the arena.</param>
        public void Start(List<Player> players, int scoreLimit)
        {
            Players = players;
            ScoreLimit = scoreLimit;
        }

        /// <summary>
        /// Add the CurrentEnemy score to the provided player's score and generate a new enemy.
        /// </summary>
        /// <param name="player">The player that won the fight, if null, no player's score will increase and the score will be accumulated to the next enemy.</param>
        public void NextEnemy (Player? player)
        {
            player?.AddScore(CurrentEnemy.Score);

            var previousScore = player == null ? CurrentEnemy.Score : 0;
            CurrentEnemy = new Enemy(previousScore, MakeElite());
        }

        /// <summary>
        /// Determines wether a player has reached the score limit determining that the game is finished or not
        /// </summary>
        /// <returns>Bool if a player has reache the score limit.</returns>
        public bool HasAPlayerWon() => Players.Any(p => p.Score > ScoreLimit);

        /// <summary>
        /// Override the public properties of this game with the previous ones
        /// </summary>
        /// <param name="previousGameSession">Previous game isntance</param>
        public void ResumePreviousGame(GameService previousGameSession)
        {
            Players = previousGameSession.Players;
            ScoreLimit = previousGameSession.ScoreLimit;
            CurrentEnemy = previousGameSession.CurrentEnemy;
        }

        private bool MakeElite()
        {
            if (!eliteHasPassed)
            {
                var makeElite = new Random().Next(1, 10) > 9;
                eliteHasPassed = makeElite;
                return makeElite;
            }

            return false;
        }
    }
}
