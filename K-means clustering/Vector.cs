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
        private List<int> location = new List<int>();
        private Cluster cluster;

        public List<int> Location
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

        public Cluster Cluster
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
