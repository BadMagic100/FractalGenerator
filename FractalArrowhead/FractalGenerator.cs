using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    public abstract class FractalGenerator
    {
        protected readonly Canvas canvas;
        protected readonly LineGraphics graphics;

        public FractalGenerator(Canvas c)
        {
            canvas = c;
            graphics = new LineGraphics(canvas);
        }

        protected abstract void Init();

        public void Setup()
        {
            Init();
            graphics.Render();
        }

        protected abstract void IterateOn(Line l);

        public void Iterate()
        {
            foreach(UIElement u in canvas.Children)
            {
                if (u is Line l)
                {
                    IterateOn(l);
                }
            }
            graphics.Render();
        }

        public override string ToString()
        {
            return ToTitleCase(GetType().Name);
        }

        private string ToTitleCase(string str)
        {
            return Regex.Replace(str, @"[a-zA-Z]([A-Z]|\d)", m => $"{m.Value[0]} {m.Value[1]}");
        }
    }
}
