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
    public class DragonOfEve : FractalGenerator
    {
        public DragonOfEve(Canvas c) : base(c) { }

        protected override void Init()
        {
            //start with horizontal line
            graphics.DrawLine(-300, -100, 300, -100);
        }

        //from http://www.fractalcurves.com/Dragon_of_Eve/
        protected override void IterateOn(Line l)
        {
            //split the line in two equal parts
            List<Line> segments = graphics.Split(l, 2);
            //make sure it worked
            if (segments == null) return;
            //naming is not necessarily correct but is true for the first one
            Line left = segments[0];
            Line right = segments[1];
            //rotate the left segment 90 degrees
            left = graphics.RotateAbout(left, left.X1, left.Y1, 90);
            //connect the left and right segments
            graphics.DrawLine(left.X2, left.Y2, right.X1, right.Y1);
            //flip the direction of the right segment so it faces down
            graphics.Flip(right);
        }
    }
}
