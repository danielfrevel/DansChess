
using DansChess.api.game.Models;
using Microsoft.AspNetCore.Mvc;

namespace DansChess.api.game
{
    [Route("[controller]/[action]")]
    public class GameController : Controller
    {
        
        public GameController()
        {
            
        }


        [HttpGet]
        public IActionResult CreateNewGame(){
            
            
            return Ok(new BoardResultModel());
        }

        [HttpPost]
        public IActionResult MakeMove(MakeMovesPm parameter){

            

            return Ok(new BoardResultModel ());
        }


    }
}