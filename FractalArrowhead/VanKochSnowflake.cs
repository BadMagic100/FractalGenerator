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
    public class VanKochSnowflake : FractalGenerator
    {
        public VanKochSnowflake(Canvas c) : base(c) { }

        protected override void Init()
        {
            //setup equilateral triangle for koch snowflake
            Line top = graphics.DrawLine(-300, 200, 300, 200);
            //make an equal length copy of the top segment
            Line left = graphics.Copy(top);
            //rotate about its left endpoint clockwise 60 degrees
            left = graphics.RotateAbout(left, left.X1, left.Y1, -60);
            //make it face outwards
            left = graphics.Flip(left);
            //make an equal length copy of the right segment
            Line right = graphics.Copy(top);
            //rotate about its right endpoint counterclockwise 60 degrees
            right = graphics.RotateAbout(right, right.X2, right.Y2, 60);
            //make it face outwards
            right = graphics.Flip(right);
        }

        protected override void IterateOn(Line l)
        {
            //split each line into 3 segments
            List<Line> segments = graphics.Split(l, 3);
            //make sure it worked
            if (segments == null) return;
            //make a copy of the middle one
            Line middle = segments[1];
            Line otherMiddle = graphics.Copy(middle);
            //rotate the middle segments outward by 60 degrees. done correctly, both still face outward
            graphics.RotateAbout(middle, middle.X1, middle.Y1, 60);
            graphics.RotateAbout(otherMiddle, otherMiddle.X2, otherMiddle.Y2, -60);
        }
    }
}
