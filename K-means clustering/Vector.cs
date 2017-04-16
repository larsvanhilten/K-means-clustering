using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means_clustering
{
    class Vector
    {
        private int[] location = new int[32];
        private Cluster cluster;

        public int[] Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        internal Cluster Cluster
        {
            get
            {
                return cluster;
            }

            set
            {
                cluster = value;
            }
        }

        public void addOffer(int offer) {
            Location[Location.Length - 1] = offer;
        }

        public double getEuclideanDistance(float[] location) {
            double power = 0f;
            for (int i = 0; i < location.Length; i++)
            {
                power += Math.Pow(this.Location[i] - location[i], 2);
            }
            return Math.Sqrt(power);
        }
        
    }
}
