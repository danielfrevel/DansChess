using System;
using Generation;
namespace Ausgabe
{
    class Program
    {
        static void Main(string[] args)
        {
            //REINES TESTPROGRAMM (nur um zu sehen was überhaupt passiert)
            var Board = new Board();
            for (int i = 0; i < 64; i++) 
            {
                Console.WriteLine($"Square[{Board.Square}]");
                Console.WriteLine($"Piece = {Piece.ToString(Board.Square[i])}");
                Console.WriteLine("------------------");
            }
           

            var moveGen = new MoveGenerator();

            var moves = moveGen.GenerateMoves(Board);

            foreach(var move in moves)
            {
                Console.WriteLine($"{Piece.ToString(Board.Square[move.startSquare])}");
                Console.WriteLine($"{move.targetSquare}");
            }
            Console.ReadKey();


        }
    }
}

