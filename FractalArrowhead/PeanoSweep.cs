using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    //http://fractalcurves.com/all_curves/images/4G/10.png
    public class PeanoSweep : FractalGenerator
    {
        public PeanoSweep(Canvas c) : base(c) { }

        protected override void Init()
        {
            //start with a line
            graphics.DrawLine(-300, -300, 300, -300);
        }

        protected override void IterateOn(Line l)
        {
            //get a new line with flipped orientation
            //this is ok to do before return case; if it fails it will flip but it will fail to continue again
            Line n = graphics.SwapEndpoints(l);
            //split in 2 and verify it worked
            List<Line> segments = graphics.Split(n, 2);
            if (segments == null) return;
            //because we reversed the orientation of the line. assign good names to correct intuition
            Line right = segments[0];
            Line left = segments[1];
            //get the rotation direction
            int sign = graphics.GetDirection(l);
            //rotate as needed
            left = graphics.RotateAbout(left, left.X2, left.Y2, sign * 90);
            right = graphics.RotateAbout(right, right.X1, right.Y1, -sign * 90);
            //connect the left and right portion by a line with same direction as original
            Line center = graphics.DrawLine(left.X1, left.Y1, right.X2, right.Y2);
            center = graphics.SetDirectionFrom(center, l);
            //split that up
            graphics.Split(center, 2);
        }
    }
}
