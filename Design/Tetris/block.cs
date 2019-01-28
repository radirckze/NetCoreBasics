using System;

namespace NetCoreBasics.Design.Tetris {

    public enum Colors {Red, Yellow, Blue} 

    public class Block {
        
        public Colors Color { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    }

    
}