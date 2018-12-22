using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    public class SierpinskiTriangle : FractalGenerator
    {
        public SierpinskiTriangle(Canvas c) : base(c) { }

        protected override void Init()
        {
            //start at the bottom. make lots of space, as we will build up
            graphics.DrawLine(-300, -300, 300, -300);
        }

        protected override void IterateOn(Line l)
        {
            //split the line into 2 equal parts
            List<Line> segments = graphics.Split(l, 2);
            //make sure it worked
            if (segments == null) return;
            //these directions are pretty arbitrary, but they are correct for the first line
            //the same operations apply though (that's the whole point)
            Line left = segments[0];
            Line right = segments[1];
            //rotate both segments outward 60 degrees
            left = graphics.RotateAbout(left, left.X1, left.Y1, 60);
            right = graphics.RotateAbout(right, right.X2, right.Y2, -60);
            //make a copy of the left portion. flip it up so that it connects the segments
            Line center = graphics.Copy(left);
            center = graphics.RotateAbout(center, center.X2, center.Y2, 120);
            //finally, flip the left and right segments to make them face inward...
            graphics.Flip(left);
            graphics.Flip(right);
            //... and the center segment to face outward
            graphics.Flip(center);
        }
    }
}
