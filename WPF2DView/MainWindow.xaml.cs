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
using ConvexHullAlgorithm;

namespace WPF2DView
{
    /// <summary>
    /// Interaction logic for MainWindow.Xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Vector> pointsCloud = new List<Vector>(); 
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);
            Vector positionPoint = new Vector((float)position.X, (float)position.Y);
            pointsCloud.Add(positionPoint);

            DrawPoint(positionPoint, Colors.White);
        }

        private void ComputeConvexHull_Click(object sender, RoutedEventArgs e)
        {
            ConvexHulAlgorithm convexHul = new ConvexHulAlgorithm(this.pointsCloud);
            List<Vector> convexHulPoints = convexHul.FindConvexHul();

            ClearCanvas();
            DrawPoints(this.pointsCloud, Colors.White);
            this.pointsCloud.Clear();

            DrawPoints(convexHulPoints, Colors.Green);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();
        }

        void DrawPoints(List<Vector> points, Color pointColor)
        {
            foreach (Vector point in points)
            {
                DrawPoint(point, pointColor);
            }
        }

        void DrawPoint(Vector point, Color pointColor)
        {
            var ellipse = new Ellipse() { Width = 5.0, Height = 5.0, Stroke = new SolidColorBrush(pointColor) };
            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);
            Canvas2D.Children.Add(ellipse);
        }

        void ClearCanvas()
        {
            this.Canvas2D.Children.Clear();
        }
    }
}
