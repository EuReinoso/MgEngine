﻿using Microsoft.Xna.Framework;

namespace MgEngine.Interface
{
    internal interface IBox2D
    {
        public Vector2 Pos { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
