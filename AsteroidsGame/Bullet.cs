﻿using System;
using System.Drawing;
//Брижак Андрей Домашнее задание по курсу C# уровень 2 урок 2

namespace AsteroidsGame
{
    /// <summary>
    /// класс Bullet, наследуемый от BaseObject
    /// </summary>
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + 3;
        }
    }
}
