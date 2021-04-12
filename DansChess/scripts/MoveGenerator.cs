using System.Collections.Generic;
using static PrecomputedMoveData;


namespace Generation { 
public class MoveGenerator
{
	

	public List<Move> moves;
	int friendlyColour;
	int opponentColour;
	int friendlyKingSquare; //wird noch nicht gebraucht
	//Idee ist jedoch: wenn ich den King Square im Auge behalte könnte es später einfacher sein alle Checks im Auge zu behalten
	int friendlyColourIndex;

	Board board;

	public List<Move> GenerateMoves(Board board)
	{
		this.board = board;

		Init();
		GenerateKingMoves();
		GenerateSlidingMoves();
		GenerateKnightMoves();
		GeneratePawnMoves();

		return moves;
	}

	void Init()
	{
		moves = new List<Move>(64);

		friendlyKingSquare = board.KingSquare[board.ColourToMoveIndex];
		friendlyColour = board.ColourToMove;
		opponentColour = board.OpponentColour;
		friendlyColourIndex = (board.WhiteToMove) ? Board.WhiteIndex : Board.BlackIndex;
	}
	bool isCapture(int targetSquare){
		return Piece.IsColour(board.Square[targetSquare], opponentColour);
	}
	
	bool isKingCapture(int targetSquare){
		int  targetPiece = board.Square[targetSquare];
		//wenn gegner und piece dann king square
		if (Piece.IsColour(targetPiece, opponentColour) && Piece.King == Piece.PieceType(targetPiece)){
			return true;
		}
		return false;
	}

	void GenerateKingMoves()
	{
		for (int i = 0; i < kingMoves[friendlyKingSquare].Length; i++)
		{
			int targetSquare = kingMoves[friendlyKingSquare][i];
			int pieceOnTargetSquare = board.Square[targetSquare];

			// wenn friendly piece auf square nicht generieren
			if (Piece.IsColour(pieceOnTargetSquare, friendlyColour))
			{
				continue;
			}
			
			moves.Add(new Move(friendlyKingSquare, targetSquare));
		}
	}			
			

		void GenerateSlidingMoves()
		{
			PieceList rooks = board.rooks[friendlyColourIndex];
			for (int i = 0; i < rooks.Count; i++)
			{
				GenerateSlidingPieceMoves(rooks[i], 0, 4);
			}

			PieceList bishops = board.bishops[friendlyColourIndex];
			for (int i = 0; i < bishops.Count; i++)
			{
				GenerateSlidingPieceMoves(bishops[i], 4, 8);
			}

			PieceList queens = board.queens[friendlyColourIndex];
			for (int i = 0; i < queens.Count; i++)
			{
				GenerateSlidingPieceMoves(queens[i], 0, 8);
			}

		}

		void GenerateSlidingPieceMoves(int startSquare, int startDirIndex, int endDirIndex)
		{
		//Verhalten von Queen, Rook, Bishop ist relativ ähnlich das hier sollte deshalb funktionieren
			for (int directionIndex = startDirIndex; directionIndex < endDirIndex; directionIndex++)
			{
				int currentDirOffset = directionOffsets[directionIndex];

				for (int n = 0; n < numSquaresToEdge[startSquare][directionIndex]; n++)
				{
					int targetSquare = startSquare + currentDirOffset * (n + 1);
					int targetSquarePiece = board.Square[targetSquare];

					// von friendly blockiert -> richtung skippen
					if (Piece.IsColour(targetSquarePiece, friendlyColour))
					{
						break;
					}
					if(isKingCapture(targetSquare)){
						moves.Add(new Move(99,99));
						break;
					}
					moves.Add(new Move(startSquare, targetSquare));
				}
			}
		}

		void GenerateKnightMoves()
		{
			PieceList myKnights = board.knights[friendlyColourIndex];

			for (int i = 0; i < myKnights.Count; i++)
			{
				int startSquare = myKnights[i];

				for (int knightMoveIndex = 0; knightMoveIndex < knightMoves[startSquare].Length; knightMoveIndex++)
				{
					int targetSquare = knightMoves[startSquare][knightMoveIndex];
					int targetSquarePiece = board.Square[targetSquare];
					if (Piece.IsColour(targetSquarePiece, friendlyColour))
					{
						continue;
					}
					if(isKingCapture(targetSquare)){
						moves.Add(new Move(99,99));
						break;
					}
					moves.Add(new Move(startSquare, targetSquare));
				}
			}
		}

		void GeneratePawnMoves()
		{
			PieceList myPawns = board.pawns[friendlyColourIndex];
			int pawnOffset = (friendlyColour == Piece.White) ? 8 : -8;
			int startRank = (board.WhiteToMove) ? 1 : 6;

			for (int i = 0; i < myPawns.Count; i++)
			{
				int startSquare = myPawns[i];
				int squareOneForward = startSquare + pawnOffset;
				
				// kann nur vor wenn nicht von anderem Piece blockiert
				if (board.Square[squareOneForward] == Piece.None)
				{
					moves.Add(new Move(startSquare, squareOneForward));
				}
				bool isBlackStartRank = startSquare >= 8 && startSquare <= 15;
				bool isWhiteStartRank = startSquare >= 48 && startSquare <= 55;

				if (isBlackStartRank || isWhiteStartRank)
			{
					int squareTwoForward = squareOneForward + pawnOffset;
					if (board.Square[squareTwoForward] == Piece.None)
					{
						moves.Add(new Move(startSquare, squareTwoForward));
					}

				}
				for (int j = 0; j < 2; j++)
				{
					if (numSquaresToEdge[startSquare][pawnAttackDirections[friendlyColourIndex][j]] > 0)
					{
						int pawnCaptureDir = directionOffsets[pawnAttackDirections[friendlyColourIndex][j]];
						int targetSquare = startSquare + pawnCaptureDir;
						int targetPiece = board.Square[targetSquare];

						if(isKingCapture(targetSquare)){
						moves.Add(new Move(99,99));
						break;
						}
						if (Piece.IsColour(targetPiece, opponentColour))
						{
							moves.Add(new Move(startSquare, targetSquare));
						}
					}
				}
			}
		}

	}

}