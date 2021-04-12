
using DansChess.api.game.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Generation;

namespace DansChess.api.game
{
    [Route("[controller]/[action]")]
    public class GameController : Controller
    {
        
        public GameController()
        {
        }
        public static Board currentBoard {get; set;} //vielleicht sollte man das anders handeln aber geht schnell

        

        [HttpGet]
        public IActionResult GetBoard(bool createNewBoard = false){

        
        var model = new BoardResultModel();
        
        if(GameController.currentBoard == null || createNewBoard){
            GameController.currentBoard = new Board();
        }

        MoveGenerator generator = new MoveGenerator();
        model.BoardRepresentation = GameController.currentBoard.Square;
        model.Moves = generator.GenerateMoves(GameController.currentBoard);
        var mateMove =new Move(99,99);
        if (model.Moves.Contains(mateMove)){
            model.Moves.Append(new Move(99,99));
        }
    
        return Ok(model);
        }

        [HttpPost]
        public IActionResult MakeMove([FromBody]Move move){
            GameController.currentBoard?.MakeMove(move);
            return Ok();
        }
    }
}
