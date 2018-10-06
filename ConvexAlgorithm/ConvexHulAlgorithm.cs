using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvexHullAlgorithm
{
    public class ConvexHulAlgorithm
    {
        List<Vector> pointsCloud = new List<Vector>();
        public ConvexHulAlgorithm(List<Vector> pointsCloud) 
        {
            this.pointsCloud = pointsCloud;
        }

        public List<Vector> FindConvexHul()
        {
            List<Vector> convectHulPoints= new List<Vector>();

            //1. Find initial point
            //TODO : Lambda expression
            Vector startPoint;
            if (!Helper.GetPointWithMinX(this.pointsCloud, out startPoint))
                return convectHulPoints;

            convectHulPoints.Add(startPoint);

            //2. Find second point
            Vector secondPoint = new Vector();
            if (FindSecondPoint(startPoint, out secondPoint))
                convectHulPoints.Add(secondPoint);

            //3. Find further points
            if (convectHulPoints.Count < 2)
                return convectHulPoints;

            Vector nextPoint = new Vector();

            while (!nextPoint.Equals(startPoint))
            {
                Vector prevPoint = convectHulPoints.ElementAt(convectHulPoints.Count - 2);
                Vector currentPoint = convectHulPoints.ElementAt(convectHulPoints.Count - 1);

                List<Vector> remainingPoints = this.pointsCloud.ToList();
                remainingPoints.Remove(prevPoint);
                remainingPoints.Remove(currentPoint);

                if (FindNextPoint(prevPoint, currentPoint, remainingPoints, out nextPoint))
                {
                    convectHulPoints.Add(nextPoint);
                    //break;
                }
            }


            return convectHulPoints;
        }

        private bool FindNextPoint(Vector prevPoint, Vector currentPoint, List<Vector> remainingPoints, out Vector nextPoint)
        {
            nextPoint = new Vector();
            Vector prevCurrentVector = new Vector(currentPoint.X - prevPoint.X, currentPoint.Y - prevPoint.Y);
            prevCurrentVector.Normalize();

            double maxDotProduct = double.MinValue;
            foreach (Vector point in remainingPoints)
            {
                Vector currentNextVector = new Vector(point.X - currentPoint.X, point.Y - currentPoint.Y);
                currentNextVector.Normalize();

                double dotProduct = prevCurrentVector* currentNextVector;
                if (dotProduct == 0)
                {
                    nextPoint = point;
                    return true;
                }

                if (maxDotProduct < dotProduct)
                {
                    maxDotProduct = dotProduct;
                    nextPoint = point;
                }
            }

            if (maxDotProduct == double.MaxValue)
                return false;

            return true;
        }

        private bool FindSecondPoint(Vector startPoint, out Vector secondPoint)
        {
            List<Vector> remainingPoints = this.pointsCloud.ToList();
            remainingPoints.Remove(startPoint);
            bool isSecondPointFound = false;
            secondPoint = new Vector();
            while (!isSecondPointFound || remainingPoints.Count > 0)
            {
                Vector minXPoint = new Vector();
                if (Helper.GetPointWithMinX(remainingPoints, out minXPoint))
                {
                    if (Helper.AllPointsCrossProductIsInSameDirection(startPoint, minXPoint, remainingPoints))
                    {
                        isSecondPointFound = true;
                        secondPoint = minXPoint;
                        //convectHulPoints.Add(minXPoint);
                        break;
                    }
                }
            }

            return isSecondPointFound;
        }
    }
}
