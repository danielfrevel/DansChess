public class PieceList {
	
	//soll helfen über alle pieces einer art zu iterieren
	//vereinfach insgesamt das management aller pieces auf dem Board


	// Indices der Squares die von einem PieceType besetzt sind
	public int[] occupiedSquares;
	
	//map von Squares(a) zum occupiedSquares(b) in welchem sich das Piece von Square(a) befindet
	int[] map;
	int numPieces;

	public PieceList (int maxPieceCount = 16) { 
		occupiedSquares = new int[maxPieceCount]; 
		map = new int[64]; 
		numPieces = 0;
	}

	public int Count {
		get {
			return numPieces;
		}
	}
	
	public void AddPieceAtSquare(int square)
	{
		occupiedSquares[numPieces] = square;
		map[square] = numPieces;
		numPieces++;
	}

	public void RemovePieceAtSquare(int square)
	{
		int pieceIndex = map[square]; //index des elements im occupied squares array finden
		occupiedSquares[pieceIndex] = occupiedSquares[numPieces - 1]; //letztes element im array an die des entfernten setzen
		map[occupiedSquares[pieceIndex]] = pieceIndex; // map updaten
		numPieces--;
	}

	public void MovePiece(int startSquare, int targetSquare)
	{
		int pieceIndex = map[startSquare];
		occupiedSquares[pieceIndex] = targetSquare;
		map[targetSquare] = pieceIndex;
	}

	public int this [int index] => occupiedSquares[index];

}