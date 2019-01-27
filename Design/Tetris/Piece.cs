using System;

namespace CSExtended.Design.Tetris {

    public class Piece {

        public enum ShapeType {/* ... */};
        public enum MotionStatusType {New, Moving, Settled};

        public Colors Color { get; set; }
        public Block[] Blocks { get; set ;}
        public MotionStatusType MotionStatus { get; set; }

        public Piece(Colors color) {
            Blocks = new Block[4]; //shape chosen randomly for simplicity.
            this.Color = color;
            MotionStatus = MotionStatusType.New;
        }

        public bool ChangeShape(ShapeType newShape) {
            throw new NotImplementedException();
        }
    } 
}