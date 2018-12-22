using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    public class MandelbrotCurve : FractalGenerator
    {
        public MandelbrotCurve(Canvas c) : base(c) { }

        protected override void Init()
        {
            //start with horizontal line
            graphics.DrawLine(-300, 0, 300, 0);
        }

        //from http://www.tgmdev.be/applications/acheron/curves/curvemandelbrot.php
        protected override void IterateOn(Line l)
        {
            //split into 3 parts and ensure it worked
            List<Line> segments = graphics.Split(l, 3);
            if (segments == null) return;
            //copy each segment, and copy the middle segment a second time
            Line left = graphics.Copy(segments[0]);
            Line center1 = graphics.Copy(segments[1]);
            Line center2 = graphics.Copy(center1);
            Line right = graphics.Copy(segments[2]);
            //rotate the outer segments down
            graphics.RotateAbout(left, left.X2, left.Y2, 90);
            graphics.RotateAbout(right, right.X1, right.Y1, -90);
            //rotate the center segments up
            center1 = graphics.RotateAbout(center1, center1.X1, center1.Y1, 90);
            center2 = graphics.RotateAbout(center2, center2.X2, center2.Y2, -90);
            //connect them
            graphics.DrawLine(center1.X2, center1.Y2, center2.X1, center2.Y1);
        }
    }
}
