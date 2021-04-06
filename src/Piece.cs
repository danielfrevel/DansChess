namespace Generation
{
    public static class Piece
    {
  
        //Bits 0-3 geben immer das Piece an
        //Bits 4-5 geben immer die Farbe an

        public const int None = 0;
        public const int King = 1;
        public const int Pawn = 2;
        public const int Knight = 3;
        public const int Bishop = 5;
        public const int Rook = 6;
        public const int Queen = 7;

        public const int White = 8;
        public const int Black = 16;

        private const int typeMask = 0b00_111; 
        private const int blackMask = 0b10_000;
        private const int whiteMask = 0b01_000;
        private const int colourMask = whiteMask | blackMask;

        public static string ToString(int piece)
        {
            piece = PieceType(piece);
            switch (piece)
            {
                case 0:
                    return "None";
                case 1:
                    return "King";
                case 2:
                    return "Pawn";
                case 3:
                    return "Knight";
                case 5:
                    return "Bishop";
                case 6:
                    return "Rook";
                case 7:
                    return "Queen";
                default:
                    return "invalid";
            }
        }

        public static bool IsColour(int piece, int colour)
        {
            return (piece & colourMask) == colour;
        }

        public static int PieceType(int piece)
        {
            return piece & typeMask;
        }

        public static bool IsSlidingPiece(int piece)
        {
            return (piece & 0b100) != 0;
        }
    }
}
