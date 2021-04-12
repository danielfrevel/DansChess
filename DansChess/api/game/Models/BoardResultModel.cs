using System.Collections.Generic;
using Generation;

namespace DansChess.api.game.Models
{
    public class BoardResultModel
    {
        public int[] BoardRepresentation { get; set; }
        public IEnumerable<Move> Moves { get; set; }
        public BoardResultModel()
        {
            
        }
    }
}