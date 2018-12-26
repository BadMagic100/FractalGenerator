using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    //http://fractalcurves.com/all_curves/images/9E/o1.png
    public class UnnamedThing2 : FractalGenerator
    {
        public UnnamedThing2(Canvas c) : base(c) { }

        protected override void Init()
        {
            graphics.DrawLine(-300, -165, 300, -165);
        }

        protected override void IterateOn(Line l)
        {
            //this will involve some creative problem solving to work with line replacement
            //split in 3, verify it worked
            List<Line> segments = graphics.Split(l, 3);
            if (segments == null) return;
            //middle segment will be for measuring, but should be erased
            Line left = segments[0];
            Line center = segments[1];
            graphics.Erase(center);
            Line right = segments[2];
            //get a direction
            int sign = graphics.GetDirection(l);
            //rotate the segments "up"
            left = graphics.RotateAbout(left, center.X2, center.Y2, -sign * 60);
            right = graphics.RotateAbout(right, center.X1, center.Y1, sign * 60);
            //flip orientations in prep for connections
            left = graphics.SwapEndpoints(left);
            right = graphics.SwapEndpoints(right);
            //connect at the top and orient correctly
            Line top = graphics.DrawLine(left.X2, left.Y2, right.X1, right.Y1);
            top = graphics.SetDirectionFrom(top, l);
            //connect the tips to the original line
            Line conRight = graphics.DrawLine(l.X2, l.Y2, right.X2, right.Y2);
            conRight = graphics.SetDirectionFrom(conRight, l);
            Line conLeft = graphics.DrawLine(left.X1, left.Y1, l.X1, l.Y1);
            conLeft = graphics.SetDirectionFrom(conLeft, l);
        }
    }
}
