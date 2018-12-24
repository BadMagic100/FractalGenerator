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
            
        }

        protected override void IterateOn(Line l)
        {
            throw new NotImplementedException();
        }
    }
}
