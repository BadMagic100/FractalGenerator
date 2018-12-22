using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    public class MinkowskiCurve : FractalGenerator
    {
        public MinkowskiCurve(Canvas c) : base(c) { }

        protected override void Init()
        {
            //start with horizontal line
            graphics.DrawLine(-400, 0, 400, 0);
        }

        //from http://www.tgmdev.be/applications/acheron/curves/curveminkowski.php
        protected override void IterateOn(Line l)
        {
            //split into 4 sections and ensure it worked
            List<Line> segments = graphics.Split(l, 4);
            if (segments == null) return;
            //the leftmost and rightmost segments stay in place.
            //copy the 2 center segments to prepare for the transform
            Line cl1 = segments[1];
            Line cl2 = graphics.Copy(cl1);
            Line cr1 = segments[2];
            Line cr2 = graphics.Copy(cr1);
            //rotate the left segments up and the right segments down
            cl1 = graphics.RotateAbout(cl1, cl1.X1, cl1.Y1, 90);
            cl2 = graphics.RotateAbout(cl2, cl2.X2, cl2.Y2, -90);
            cr1 = graphics.RotateAbout(cr1, cr1.X1, cr1.Y1, -90);
            cr2 = graphics.RotateAbout(cr2, cr2.X2, cr2.Y2, 90);
            //connect the tips of the left and right segments
            graphics.DrawLine(cl1.X2, cl1.Y2, cl2.X1, cl2.Y1);
            graphics.DrawLine(cr1.X2, cr1.Y2, cr2.X1, cr2.Y1);
        }
    }
}
