using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FractalArrowhead
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            selector.Items.Add(new VanKochSnowflake(canvas));
            selector.Items.Add(new SierpinskiTriangle(canvas));
            selector.Items.Add(new FractalTree(canvas, 2, 45));
            selector.Items.Add(new FractalTree(canvas, 5, 20));
            selector.Items.Add(new DragonOfEve(canvas));
            selector.Items.Add(new MinkowskiCurve(canvas));
            selector.Items.Add(new LevyC(canvas));
            selector.Items.Add(new HeighwayDragon(canvas));
            selector.Items.Add(new MandelbrotCurve(canvas));
            selector.Items.Add(new UnnamedThing1(canvas));
            selector.Items.Add(new SnowflakeSweep(canvas));
            selector.Items.Add(new SierpinskiFamily(canvas));
            //todo
            //Gosper curve http://fractalcurves.com/all_curves/images/7E/750x240xa1.png.pagespeed.ic.CU17n7Bf-R.png
            //terdragon http://fractalcurves.com/all_curves/images/3E/8.png
            //peano sweep http://fractalcurves.com/all_curves/images/4G/10.png
            //scroll curve http://fractalcurves.com/all_curves/images/9E/d1.png
            //http://fractalcurves.com/all_curves/images/9E/o1.png

            selector.SelectedIndex = 0;
        }

        private void Iterate_Click(object sender, RoutedEventArgs e)
        {
            FractalGenerator gen = selector.SelectedItem as FractalGenerator;
            //just to be extra safe
            gen?.Iterate();
            //want: thread this so that that each line replacement is shown serially
            numLines.Text = canvas.Children.Count.ToString();
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvas.Children.Clear();
            FractalGenerator gen = selector.SelectedItem as FractalGenerator;
            //just to be extra safe
            gen?.Setup();
            numLines.Text = canvas.Children.Count.ToString();
        }
    }
}
