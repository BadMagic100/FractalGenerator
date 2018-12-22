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
    public class FractalTree : FractalGenerator
    {
        private readonly int numSplit;
        private readonly double angleSplit;

        public FractalTree(Canvas c, int branchesPerSplit, double degreesOfSeparation)
            : base(c)
        {
            numSplit = branchesPerSplit;
            angleSplit = degreesOfSeparation;
        }

        public override string ToString()
        {
            return base.ToString() + $" ({numSplit} @ {angleSplit})";
        }

        protected override void Init()
        {
            //start with a nice vertical line
            graphics.DrawLine(0, -300, 0, -100);
        }

        protected override void IterateOn(Line l)
        {
            if (!l.Resources.Contains("visited"))
            {
                //each branch will be 2/3 the size of this branch
                //copy the branch
                Line copy = graphics.Copy(l);
                //split the copy into 3 parts. only the middle part will be used and only for template purposes
                List<Line> segments = graphics.Split(copy, 3);
                //make sure it worked
                if (segments == null) return;
                //clear out the first and last segment, we don't need them
                graphics.Erase(segments[0]);
                graphics.Erase(segments[2]);
                //get the middle segment to use as a measuring stick
                Line measure = segments[1];
                //find the leftmost angle position
                //if there are odd # branches, divide that number by 2, and multiply by the angular offset
                //if there are even # branches, divide by 2, multiply by angular offset, and subtract half that offset to center
                double angle = numSplit / 2 * angleSplit;
                if (numSplit % 2 == 0) angle -= angleSplit / 2;
                //we also need to rotate out angle up by 180 to get center in the right spot
                angle += 180;
                //rotate out measuring stick to that angle
                measure = graphics.RotateAbout(measure, l.X2, l.Y2, angle);
                for(int i = 0; i < numSplit; i++)
                {
                    //draw from the tip of the branch to the tip of the measuring stick
                    graphics.DrawLine(l.X2, l.Y2, measure.X1, measure.Y1);
                    //rotate the measuring stick to the next spot
                    measure = graphics.RotateAbout(measure, l.X2, l.Y2, -angleSplit);
                }
                //erase the measuring stick
                graphics.Erase(measure);
                //mark that this line is visited
                l.Resources["visited"] = true;
            }
        }
    }
}
