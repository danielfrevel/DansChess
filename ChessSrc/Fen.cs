using System.Collections.Generic;

namespace Generation
{
	public class LoadedPositionInfo
	{
		public int[] squares;
		public bool whiteToMove;

		public LoadedPositionInfo()
		{
			squares = new int[64];
		}
	}
	public static class Fen
	{
		// Wikipedia: "Forsyth-Edwards-Notation ist eine Kurznotation mit der jede beliebige Brettstellung im Schach niedergeschrieben werden kann"
		// soll helfen in wenigen Codezeilen ein Schachbrett zu generieren 

		static Dictionary<char, int> pieceTypeFromSymbol = new Dictionary<char, int>()
		{
			['k'] = Piece.King,
			['p'] = Piece.Pawn,
			['n'] = Piece.Knight,
			['b'] = Piece.Bishop,
			['r'] = Piece.Rook,
			['q'] = Piece.Queen
		};

		public const string startFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"; //castlen wird vorerst ignoriert (Kqkq)

		public static LoadedPositionInfo FromFen(string fen)
		{

			LoadedPositionInfo loadedPositionInfo = new LoadedPositionInfo();
			string[] sections = fen.Split(' ');

			int file = 0;
			int rank = 7;

			foreach (char symbol in sections[0])
			{
				if (symbol == '/')
				{
					file = 0;
					rank--;
				}
				else
				{
					if (char.IsDigit(symbol))
					{
						file += (int)char.GetNumericValue(symbol);
					}
					else
					{
						int pieceColour = (char.IsUpper(symbol)) ? Piece.White : Piece.Black;
						int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
						loadedPositionInfo.squares[rank * 8 + file] = pieceType | pieceColour;
						file++;
					}
				}
			}

			loadedPositionInfo.whiteToMove = (sections[1] == "w");

			
			return loadedPositionInfo;
		}

		// return: fen string des eingegeben Boards
		public static string CurrentFen(Board board)
		{
			string fen = "";
			for (int rank = 7; rank >= 0; rank--)
			{
				int numEmptyFiles = 0;
				for (int file = 0; file < 8; file++)
				{
					int i = rank * 8 + file;
					int piece = board.Square[i];
					if (piece != 0)
					{
						if (numEmptyFiles != 0)
						{
							fen += numEmptyFiles;
							numEmptyFiles = 0;
						}
						bool isBlack = Piece.IsColour(piece, Piece.Black);
						int pieceType = Piece.PieceType(piece);
						char pieceChar = ' ';
						switch (pieceType)
						{
							case Piece.Rook:
								pieceChar = 'R';
								break;
							case Piece.Knight:
								pieceChar = 'N';
								break;
							case Piece.Bishop:
								pieceChar = 'B';
								break;
							case Piece.Queen:
								pieceChar = 'Q';
								break;
							case Piece.King:
								pieceChar = 'K';
								break;
							case Piece.Pawn:
								pieceChar = 'P';
								break;
						}
						fen += (isBlack) ? pieceChar.ToString().ToLower() : pieceChar.ToString();
					}
					else
					{
						numEmptyFiles++;
					}

				}
				if (numEmptyFiles != 0)
				{
					fen += numEmptyFiles;
				}
				if (rank != 0)
				{
					fen += '/';
				}
			}

			fen += ' ';
			fen += (board.WhiteToMove) ? 'w' : 'b';

			return fen;
		}


	}
}