using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    public class HeighwayDragon : FractalGenerator
    {
        public HeighwayDragon(Canvas c) : base(c) { }

        protected override void Init()
        {
            //start with a horizontal line
            graphics.DrawLine(-300, 100, 300, 100);
        }

        protected override void IterateOn(Line l)
        {
            //split a copy of the current line in half for measuring sticks
            List<Line> segments = graphics.Split(graphics.Copy(l), 2);
            //check that it worked
            if (segments == null) return;
            //get rid of one of the segments; we don't need it
            graphics.Erase(segments[1]);
            Line measure = segments[0];
            //rotate the measuring stick down
            measure = graphics.RotateAbout(measure, measure.X2, measure.Y2, 90);
            //connect the line endpoints to the measuring stick to form an isosceles triangle
            graphics.DrawLine(l.X1, l.Y1, measure.X1, measure.Y1);
            graphics.DrawLine(l.X2, l.Y2, measure.X1, measure.Y1);
            //erase the original line and the measuring stick
            graphics.Erase(l);
            graphics.Erase(measure);
        }
    }
}
