using System;

namespace CSExtended.Design.Tetris {

    public class Controller {


        private Board GameBoard = null;

        public Controller (int boardHeight=10, int boardWidth=10) {
            GameBoard = new Board(boardHeight, boardWidth);
        } 

        public void StartGame() {
            Piece currentPiece = null;
            bool runnable = true;
            bool isInitialMove = true;
            MoveRequest moveRequest = null;

            while(runnable) {
                
                if (isInitialMove) {
                    currentPiece = new Piece(Colors.Yellow);
                    moveRequest = GetInitialMove();
                    isInitialMove = false;
                }
                else {
                    moveRequest = GetRequestedMove();
                }

                if (moveRequest.RequestedShape != null) {
                    currentPiece.ChangeShape((Piece.ShapeType)moveRequest.RequestedShape);
                }

                Board.MoveResult moveResult = GameBoard.MoveIfValid(currentPiece, 
                                                (Board.Move)moveRequest.RequestedMove);
                
                if (moveResult == Board.MoveResult.Stuck) {
                    currentPiece.MotionStatus = Piece.MotionStatusType.Settled;
                    GameBoard.ClearRows();

                    if (GameBoard.IsGameOver()) {
                        runnable = false;
                    }
                    else {
                        currentPiece = null;
                        isInitialMove = true;
                    }
                }

                // re-render Gameboard.
                // do whatever logic to adjust speed, if necessary
                // sleep for some period based on speed.
                

            }


        }

        //Get user request for move AND shapeChange, if any
        public MoveRequest GetRequestedMove() {
            throw new NotImplementedException();
        }

        //Get a (randomized) initialize move
        public MoveRequest GetInitialMove() {
            throw new NotImplementedException();
        }
    }

    
}