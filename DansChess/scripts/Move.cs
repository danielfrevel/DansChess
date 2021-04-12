
namespace Generation
{
	public class Move
	{
		//Idee aber zu faul: auch über bits abbilden und den move in einem int abbilden und mit Masks filtern
		public int startSquare { get; set; }//gibt automatisch auch das Piece an
		public int targetSquare { get; set; }
		//protection später wieder private (nur zu testzwecken)
		public Move(int _startSquare, int _targetSquare)
		{
			startSquare = _startSquare;
			targetSquare = _targetSquare;
		}
		public Move()
		{
			
		}
	}
}
