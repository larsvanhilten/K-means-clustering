using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means_clustering
{
    class Cluster
    {
        private float[] centroid = new float[32];

        public Cluster(float[] centroid) {
            this.Centroid = centroid;
        }

        public float[] Centroid
        {
            get
            {
                return centroid;
            }

            set
            {
                centroid = value;
            }
        }
    }
}
