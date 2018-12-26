using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    public class SierpinskiFamily : FractalGenerator
    {
        public SierpinskiFamily(Canvas c) : base(c) { }

        protected override void Init()
        {
            //start with horizontal line
            graphics.DrawLine(-300, -100, 300, -100);
        }

        //http://fractalcurves.com/all_curves/images/4E/11.png
        protected override void IterateOn(Line l)
        {
            //split in 2 and verify it worked
            List<Line> segments = graphics.Split(l, 2);
            if (segments == null) return;
            //copy the first segment
            Line left = segments[0];
            Line center = graphics.Copy(left);
            //don't need the right segment, totally fine as is
            //the first line gets flipped upside-down but maintains orientation
            left = graphics.FlipDirection(left);
            //the second line gets reversed but maintains direction
            center = graphics.SwapEndpoints(center);
            //get the direction of the original to see which direction to rotate
            int sign = graphics.GetDirection(l);
            //rotate up if line is up, down if line is down
            graphics.RotateAbout(left, left.X1, left.Y1, sign * 60);
            graphics.RotateAbout(center, center.X1, center.Y1, -sign * 60);
        }
    }
}
