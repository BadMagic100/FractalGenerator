using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    //Based on:
    //http://fractalcurves.com/all_curves/images/4E/8.png
    //L-system (I'm only 90% sure this is correct don't sue me):
    //Axiom: A
    //Angle = 60 degree
    //Replacement rules:
    //  A -> +B--BB++B
    //  B -> -A++AA--A
    public class UnnamedThing1 : FractalGenerator
    {
        public UnnamedThing1(Canvas c) : base(c) { }

        protected override void Init()
        {
            Line l = graphics.DrawLine(-300, 0, 300, 0);
        }

        protected override void IterateOn(Line l)
        {
            //split in 2, make sure it works
            List<Line> segments = graphics.Split(l, 2);
            if (segments == null) return;
            //copy both segments
            Line l1 = segments[0];
            Line l2 = graphics.Copy(l1);
            Line r1 = segments[1];
            Line r2 = graphics.Copy(r1);
            //if the line is up, rotate the left up and right down
            //and vice-versa if the line is down
            int sign = graphics.GetDirection(l);
            l1 = graphics.RotateAbout(l1, l1.X1, l1.Y1, sign * 60);
            l2 = graphics.RotateAbout(l2, l2.X2, l2.Y2, -sign * 60);
            r1 = graphics.RotateAbout(r1, r1.X1, r1.Y1, -sign * 60);
            r2 = graphics.RotateAbout(r2, r2.X2, r2.Y2, sign * 60);
            //invert all the new lines' direction
            graphics.FlipDirection(l1);
            graphics.FlipDirection(l2);
            graphics.FlipDirection(r1);
            graphics.FlipDirection(r2);
        }
    }
}
