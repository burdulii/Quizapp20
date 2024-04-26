namespace QuizApp.Models
{
    public class Player
    {
        public Player()
        {
            HighestScore = 0;
            Wons = 0;
            Rounds = new List<Round>();
        }
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public float HighestScore { get; set; }
        public int Wons { get; set; }
        public List<Round> Rounds { get; set; }
    }
}