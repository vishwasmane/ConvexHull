using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvexHullAlgorithm
{
    public static class Helper
    {
        public static bool GetPointWithMinX(List<Vector> points, out Vector minXPoint)
        {
            minXPoint = new Vector();

            if (points.Count() <= 0)
                return false;
            
            double minXValue = double.MaxValue;
            foreach (Vector point in points)
            {
                if (point.X < minXValue)
                {
                    minXValue = point.X;
                    minXPoint = point;
                }
            }

            return true;
        }

        public static void AllPointsCrossProductIsInSameDirection(Vector startPoint, Vector minXPoint, List<Vector> remainingPoints)
        {
            Vector currentVector = new Vector(minXPoint.X - startPoint.X, minXPoint.Y - startPoint.Y);
            currentVector.Normalize();
            
            foreach (Vector point in remainingPoints)
            {
                if (point.Equals(minXPoint))
                    continue;

                Vector nextVector = new Vector(point.X - minXPoint.X, point.Y - minXPoint.Y);
                
                nextVector.Normalize();
                double crossProduct = Vector.CrossProduct(currentVector, nextVector);
                

            }
        }
    }
}
