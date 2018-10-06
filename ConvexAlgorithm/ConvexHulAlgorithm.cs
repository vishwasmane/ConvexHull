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

            //Find initial point
            Vector startPoint;
            if (!Helper.GetPointWithMinX(this.pointsCloud, out startPoint))
                return convectHulPoints;
            //Lambda expression
            //startPoint = this.pointsCloud.Min(p => p.X);

            List<Vector> remainingPoints = this.pointsCloud.ToList();
            remainingPoints.Remove(startPoint);
            bool isSecondPointFound = false;
            Vector secondPoint = new Vector();
            while (!isSecondPointFound)
            {
                Vector minXPoint = new Vector();
                if (Helper.GetPointWithMinX(remainingPoints, out minXPoint))
                {
                    Helper.AllPointsCrossProductIsInSameDirection(startPoint, minXPoint, remainingPoints);
                }
            }
            return convectHulPoints;
        }
        
    }
}
