using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    public class LineGraphics
    {
        private readonly Canvas canvas;
        private readonly List<Line> pendingAdd;
        private readonly List<Line> pendingRemove;

        public LineGraphics(Canvas target)
        {
            canvas = target;
            pendingAdd = new List<Line>();
            pendingRemove = new List<Line>();
        }

        public Line DrawLine(double x1, double y1, double x2, double y2)
        {
            Line l = new Line() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, StrokeThickness = 1 };
            l.Stroke = new SolidColorBrush(Colors.Black);
            l.Resources["dir"] = "up";
            pendingAdd.Add(l);
            return l;
        }

        public Line Copy(Line l)
        {
            Line copy = DrawLine(l.X1, l.Y1, l.X2, l.Y2);
            copy.Resources["dir"] = l.Resources["dir"];
            return copy;
        }

        public void Erase(Line l)
        {
            pendingRemove.Add(l);
        }

        public Line RotateAbout(Line l, double x, double y, double degrees)
        {
            //the distance from the rotation point to the line's start point
            double distStart = Math.Sqrt(Math.Pow(l.X1 - x, 2) + Math.Pow(l.Y1 - y, 2));
            //the angular offset of the line's start point from the pivot point, in radians
            double angleStart = Math.Atan2(l.Y1 - y, l.X1 - x);
            //correct the actual angle. if the x difference is -, rotate by pi
            //no need, atan2 apparently fixes this
            //if (l.X1 - x < 0) angleStart += Math.PI;
            //add the angle we are rotating by
            angleStart += degrees * Math.PI / 180.0;
            //calculate the new start point. x1=rcos(angle) + x, y1=rsin(angle) + y
            double nX1 = distStart * Math.Cos(angleStart) + x;
            double nY1 = distStart * Math.Sin(angleStart) + y;

            //the distance from the rotation point to the line's end point
            double distEnd = Math.Sqrt(Math.Pow(l.X2 - x, 2) + Math.Pow(l.Y2 - y, 2));
            //the angular offset of the line's end point from the pivot point, in radians
            double angleEnd = Math.Atan2(l.Y2 - y, l.X2 - x);
            //add the angle we are rotating by
            angleEnd += degrees * Math.PI / 180.0;
            //calculate the new end point. x2=rcos(angle) + x, y2=rsin(angle) + y
            double nX2 = distEnd * Math.Cos(angleEnd) + x;
            double nY2 = distEnd * Math.Sin(angleEnd) + y;

            Erase(l);
            Line n = DrawLine(nX1, nY1, nX2, nY2);
            n.Resources["dir"] = l.Resources["dir"];
            return n;
        }

        public List<Line> Split(Line l, int numSegments)
        {
            //don't want to use yield syntax because we need to make sure all cases are completed
            List<Line> ret = new List<Line>();
            //the full length of the line
            double len = Math.Sqrt(Math.Pow(l.X2 - l.X1, 2) + Math.Pow(l.Y2 - l.Y1, 2));
            //the angle of the line assuming the start point is the origin
            double angle = Math.Atan2(l.Y2 - l.Y1, l.X2 - l.X1);
            //the interval by which the distance should increment each time
            double lenInterval = len / numSegments;
            //if our length interval ever gets below 1, GIVE UP! we can't even draw that...
            if(lenInterval < 1)
            {
                //user should be able to handle this. nothing is removed and this function was worthless to them
                return null;
            }
            //the end point of the previous line, or start point of the current line
            double prevX = l.X1;
            double prevY = l.Y1;
            for(int i = 0; i < numSegments; i++)
            {
                double endX = lenInterval * Math.Cos(angle) + prevX;
                double endY = lenInterval * Math.Sin(angle) + prevY;
                Line segment = DrawLine(prevX, prevY, endX, endY);
                segment.Resources["dir"] = l.Resources["dir"];
                ret.Add(segment);
                prevX = endX;
                prevY = endY;
            }
            Erase(l);
            return ret;
        }

        public Line SwapEndpoints(Line l)
        {
            Erase(l);
            Line n = DrawLine(l.X2, l.Y2, l.X1, l.Y1);
            n.Resources["dir"] = l.Resources["dir"];
            return n;
        }

        public int GetDirection(Line l)
        {
            return (string)l.Resources["dir"] == "up" ? 1 : -1;
        }

        public Line SetDirectionFrom(Line l1, Line l2)
        {
            l1.Resources["dir"] = l2.Resources["dir"];
            return l1;
        }

        public Line FlipDirection(Line l)
        {
            string direction = (string)l.Resources["dir"] == "up" ? "down" : "up";
            l.Resources["dir"] = direction;
            return l;
        }

        public void Render()
        {
            foreach(Line l in pendingAdd)
            {
                canvas.Children.Add(l);
            }
            pendingAdd.Clear();
            //it's possible that we need to remove items that were added this frame, so the remove pass comes second
            foreach(Line l in pendingRemove)
            {
                canvas.Children.Remove(l);
            }
            pendingRemove.Clear();
        }
    }
}
