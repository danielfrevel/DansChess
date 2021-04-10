namespace DansChess.api.game.Models
{
    public class MakeMovesPm
    {
        public Generation.Move Move { get; set; }
        public int[] Board { get; set; }

        public MakeMovesPm()
        {
            
        }
    }
}