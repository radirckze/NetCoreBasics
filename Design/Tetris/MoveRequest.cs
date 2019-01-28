using System;

namespace NetCoreBasics.Design.Tetris {

    public class MoveRequest
    {
        public Nullable<Board.Move> RequestedMove { get; set ;}

        public Nullable<Piece.ShapeType> RequestedShape { get; set; }
        
    }
}