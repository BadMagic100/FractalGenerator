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
    /// <summary>
    /// An abstract iterative fractal generator
    /// </summary>
    public abstract class FractalGenerator
    {
        protected readonly Canvas canvas;
        protected readonly LineGraphics graphics;

        public FractalGenerator(Canvas c)
        {
            canvas = c;
            graphics = new LineGraphics(canvas);
        }

        /// <summary>
        /// Performs calls to the graphics engine for initial fractal setup
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// Sets up and renders the start of the fractal
        /// </summary>
        public void Setup()
        {
            Init();
            graphics.Render();
        }

        /// <summary>
        /// Performs the iterative step on a single line
        /// </summary>
        /// <param name="l">The line to replace</param>
        protected abstract void IterateOn(Line l);

        /// <summary>
        /// Performs the next step of the fractal generation
        /// </summary>
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
