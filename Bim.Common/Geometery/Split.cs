﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bim.Common.Geometery
{
    public class Split
    {

        public static List<double> Distance(double distance, double maxDistance)
        {
            // double full
            return Distance(0, distance, maxDistance);

        }

        public static List<double> Distance(double start, double distance, double maxDistance, double minValue=.000000001)
        {
            // double full
            List<double> res = new List<double>();
            double crntDistance = start;
            res.Add(crntDistance);

            while (distance > crntDistance && crntDistance + maxDistance < distance)
            {
                crntDistance += maxDistance;
                res.Add(crntDistance);
            };

            if (distance > crntDistance && distance-crntDistance > minValue)
            {
                res.Add(crntDistance + distance - crntDistance);
            }
            return res;

        }


    }
}