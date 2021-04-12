
namespace Generation
{

	public class Board
	{
		public const int WhiteIndex = 0;
		public const int BlackIndex = 1;
		public int[] KingSquare;


		public bool WhiteToMove;
		public int ColourToMove;
		public int OpponentColour;
		public int ColourToMoveIndex;

		public PieceList[] rooks;
		public PieceList[] bishops;
		public PieceList[] queens;
		public PieceList[] knights;
		public PieceList[] pawns;

		public PieceList[] allPieceLists;
	
		public int[] Square;
		//Beispiel: Square[59] = Piece.Black | Piece.Queen; Black Queen auf d8
		PieceList GetPieceList(int pieceType, int colourIndex)
		{
			return allPieceLists[colourIndex * 8 + pieceType];
		}

		public Board()
		{
			LoadPosition();
		}

		public void MakeMove(Move move)
		{
			int opponentColourIndex = 1 - ColourToMoveIndex;
			int moveFrom = move.startSquare;
			int moveTo = move.targetSquare;

			int capturedPieceType = Piece.PieceType(Square[moveTo]);
			int movePiece = Square[moveFrom];
			int movePieceType = Piece.PieceType(movePiece);

			// Handle captures
			if (capturedPieceType != 0)
			{
				GetPieceList(capturedPieceType, opponentColourIndex).RemovePieceAtSquare(moveTo);
			}

			// Move pieces in piece lists
			if (movePieceType == Piece.King)
			{
				KingSquare[ColourToMoveIndex] = moveTo;
			}
			else
			{
				GetPieceList(movePieceType, ColourToMoveIndex).MovePiece(moveFrom, moveTo);
			}

			int pieceOnTargetSquare = movePiece;



			// update board:
			Square[moveTo] = pieceOnTargetSquare;
			Square[moveFrom] = 0;

			WhiteToMove = !WhiteToMove;
			ColourToMove = (WhiteToMove) ? Piece.White : Piece.Black;
			OpponentColour = (WhiteToMove) ? Piece.Black : Piece.White;
			ColourToMoveIndex = 1 - ColourToMoveIndex;
		}

			public void LoadPosition(string fen = Fen.startFen) 
		
			{
			Initialize();
			var loadedPositionInfo = Fen.FromFen(fen);
			var loadedPosition =  loadedPositionInfo.squares;

			// pieces in board und lists einladen
			for (int squareIndex = 0; squareIndex < 64; squareIndex++)
			{
				int piece = loadedPosition[squareIndex];
				Square[squareIndex] = piece;

				if (piece != Piece.None)
				{
					int pieceType = Piece.PieceType(piece);
					int pieceColourIndex = (Piece.IsColour(piece, Piece.White)) ? WhiteIndex : BlackIndex;
					if (Piece.IsSlidingPiece(piece))
					{
						if (pieceType == Piece.Queen)
						{
							queens[pieceColourIndex].AddPieceAtSquare(squareIndex);
						}
						else if (pieceType == Piece.Rook)
						{
							rooks[pieceColourIndex].AddPieceAtSquare(squareIndex);
						}
						else if (pieceType == Piece.Bishop)
						{
							bishops[pieceColourIndex].AddPieceAtSquare(squareIndex);
						}
					}
					else if (pieceType == Piece.Knight)
					{
						knights[pieceColourIndex].AddPieceAtSquare(squareIndex);
					}
					else if (pieceType == Piece.Pawn)
					{
						pawns[pieceColourIndex].AddPieceAtSquare(squareIndex);
					}
					else if (pieceType == Piece.King)
					{
						KingSquare[pieceColourIndex] = squareIndex;
					}
				}
			}

			//Side to move
			WhiteToMove = loadedPositionInfo.whiteToMove;
			ColourToMove = (WhiteToMove) ? Piece.White : Piece.Black;
			OpponentColour = (WhiteToMove) ? Piece.Black : Piece.White;
			ColourToMoveIndex = (WhiteToMove) ? 0 : 1;

		}
		void Initialize()
		{
			//erstellt leeres Board mit Listen für jede Art Piece
			Square = new int[64];
			KingSquare = new int[2];
			knights = new PieceList[] { new PieceList(10), new PieceList(10) };
			pawns = new PieceList[] { new PieceList(8), new PieceList(8) };
			rooks = new PieceList[] { new PieceList(10), new PieceList(10) };
			bishops = new PieceList[] { new PieceList(10), new PieceList(10) };
			queens = new PieceList[] { new PieceList(9), new PieceList(9) };
			PieceList emptyList = new PieceList(0);

			allPieceLists = new PieceList[] {
				emptyList,
				emptyList,
				pawns[WhiteIndex],
				knights[WhiteIndex],
				emptyList,
				bishops[WhiteIndex],
				rooks[WhiteIndex],
				queens[WhiteIndex],
				emptyList,
				emptyList,
				pawns[BlackIndex],
				knights[BlackIndex],
				emptyList,
				bishops[BlackIndex],
				rooks[BlackIndex],
				queens[BlackIndex],
			};
		}
	}
}

