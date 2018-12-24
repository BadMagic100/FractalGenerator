using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    //from http://fractalcurves.com/all_curves/images/9E/a1.png
    public class SnowflakeSweep : FractalGenerator
    {
        public SnowflakeSweep(Canvas c): base(c) { }

        protected override void Init()
        {
            graphics.DrawLine(-300, -175, 300, -175);
        }

        protected override void IterateOn(Line l)
        {
            //split the line in 3 parts, make sure it works
            List<Line> segments = graphics.Split(l, 3);
            if (segments == null) return;
            //get the segments and clone the middle one twice
            Line left = segments[0];
            //this will stay put
            Line center1 = segments[1];
            //this will rotate with the left
            Line center2 = graphics.Copy(center1);
            //this will rotate with the right
            Line center3 = graphics.Copy(center1);
            Line right = segments[2];
            //get a rotation direction
            int sign = graphics.GetDirection(l);
            //assuming the line was facing up, per the graphic:
            //rotate the left segment and one of the middle segments up
            left = graphics.RotateAbout(left, left.X1, left.Y1, sign * 60);
            center2 = graphics.RotateAbout(center2, left.X1, left.Y1, sign * 60);
            //rotate the other middle segment up to the right
            center3 = graphics.RotateAbout(center3, right.X2, right.Y2, -sign * 60);
            //get correct directions and orientations at this point
            left = graphics.FlipDirection(left);
            center1 = graphics.FlipDirection(center1);
            center2 = graphics.FlipDirection(center2);
            center2 = graphics.SwapEndpoints(center2);
            center3 = graphics.FlipDirection(center3);
            center3 = graphics.SwapEndpoints(center3);
            right = graphics.FlipDirection(right);
            right = graphics.SwapEndpoints(right);
            //connect the starting points of center1 and center3. this has same orientation as original line
            Line connector1 = graphics.DrawLine(center1.X1, center1.Y1, center3.X1, center3.Y1);
            connector1 = graphics.SetDirectionFrom(connector1, l);
            //connect center3 and center2 tail-to-tip. this has opposite orientation from original line
            Line connector2 = graphics.DrawLine(center3.X2, center3.Y2, center2.X1, center2.Y1);
            connector2 = graphics.SetDirectionFrom(connector2, l);
            connector2 = graphics.FlipDirection(connector2);
        }
    }
}
