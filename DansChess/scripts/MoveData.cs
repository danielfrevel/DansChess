	using System.Collections.Generic;
	public static class PrecomputedMoveData {
	/**
	 * enthält Daten die im laufe der Move Generation immer wieder gebraucht werden
	 * zu großen Teilen aus dem Internet kopiert
	 */


														//(N, S, W, E, NW, SE, NE, SW)
		public static readonly int[] directionOffsets = { 8, -8, -1, 1, 7, -7, 9, -9 };
		
		//berechnet wie viele Moves für jede richtung möglich sind
		//if availableSquares[0][1] == 7... -> heißt 7 squares nach norden von b1 
		public static readonly int[][] numSquaresToEdge; //N, S, W, E, NW, SE, NE, SW


		// Beispiel: knightMoves[0] = {10, 17}, knight auf a1 kann zu c2 and b3
		public static readonly byte[][] knightMoves;
		public static readonly byte[][] kingMoves;

	    // (NW, NE; SW SE)
		public static readonly byte[][] pawnAttackDirections = {
			new byte[] { 4, 6 },
			new byte[] { 7, 5 }
		};
	
		public static readonly int[] directionLookup;


	static PrecomputedMoveData()
	{
		numSquaresToEdge = new int[8][];
		knightMoves = new byte[64][];
		kingMoves = new byte[64][];
		numSquaresToEdge = new int[64][];

		int[] allKnightJumps = { 15, 17, -17, -15, 10, -6, 6, -10 };

		for (int squareIndex = 0; squareIndex < 64; squareIndex++)
		{

			int y = squareIndex / 8;
			int x = squareIndex - y * 8;

			int north = 7 - y;
			int south = y;
			int west = x;
			int east = 7 - x;
			numSquaresToEdge[squareIndex] = new int[8];
			numSquaresToEdge[squareIndex][0] = north;
			numSquaresToEdge[squareIndex][1] = south;
			numSquaresToEdge[squareIndex][2] = west;
			numSquaresToEdge[squareIndex][3] = east;
			numSquaresToEdge[squareIndex][4] = System.Math.Min(north, west);
			numSquaresToEdge[squareIndex][5] = System.Math.Min(south, east);
			numSquaresToEdge[squareIndex][6] = System.Math.Min(north, east);
			numSquaresToEdge[squareIndex][7] = System.Math.Min(south, west);


			//knight precompute (kopiert)
			var legalKnightJumps = new List<byte>();
			ulong knightBitboard = 0;
			foreach (int knightJumpDelta in allKnightJumps)
			{
				int knightJumpSquare = squareIndex + knightJumpDelta;
				if (knightJumpSquare >= 0 && knightJumpSquare < 64)
				{
					int knightSquareY = knightJumpSquare / 8;
					int knightSquareX = knightJumpSquare - knightSquareY * 8;
					int maxCoordMoveDst = System.Math.Max(System.Math.Abs(x - knightSquareX), System.Math.Abs(y - knightSquareY));
					if (maxCoordMoveDst == 2)
					{
						legalKnightJumps.Add((byte)knightJumpSquare);
						knightBitboard |= 1ul << knightJumpSquare;
					}
				}
			}
			
			knightMoves[squareIndex] = legalKnightJumps.ToArray();
			//king precompute (kopiert)
			var legalKingMoves = new List<byte>();
			foreach (int kingMoveDelta in directionOffsets)
			{
				int kingMoveSquare = squareIndex + kingMoveDelta;
				if (kingMoveSquare >= 0 && kingMoveSquare < 64)
				{
					int kingSquareY = kingMoveSquare / 8;
					int kingSquareX = kingMoveSquare - kingSquareY * 8;

					int maxCoordMoveDst = System.Math.Max(System.Math.Abs(x - kingSquareX), System.Math.Abs(y - kingSquareY));
					if (maxCoordMoveDst == 1)
					{
						legalKingMoves.Add((byte)kingMoveSquare);
					}
				}
			}
			kingMoves[squareIndex] = legalKingMoves.ToArray();
		
	}

	}
}
		
	
