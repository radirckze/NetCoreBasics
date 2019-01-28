using System;

namespace NetCoreBasics.Design.Tetris {

    public class Board {

        public enum Move {Left, Right, Down};
        public enum MoveResult {AsRequested, Default, Stuck }

        private int Height = 0;
        private int Width = 0;

        public Block[,] Gameboard = null;

        public Board(int height, int width) {
            this.Height = height;
            this.Width = width;
            this.Gameboard = new Block[Height, Width];
        }

        // If requested move is not valid, then fallback to the default
        // which is drop down. If both fail then the piece is stuck.
        public MoveResult MoveIfValid (Piece piece, Move requestedMove, 
                                 Move defaultMove=Move.Down) 
        {
            throw new NotImplementedException();
        }

        public bool ClearRows() {
            throw new NotImplementedException();
        }

        public bool IsGameOver() {
            throw new NotImplementedException();
        }


    }

    
}