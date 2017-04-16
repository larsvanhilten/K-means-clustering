using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means_clustering
{
    class Program
    {
        private static int k = 4;
        private static int iterations = 5000;

        static void Main(string[] args)
        {
            Vector[] vectors = initiateVectors();
            Cluster[] clusters = initiateClusters(vectors);

            int counter = 0;
            while (counter < iterations) {
                vectors = assignVectorsToClosestCluster(vectors, clusters);
                clusters = recomputeCentroids(clusters, vectors);

                Console.WriteLine(counter);
                counter++;
            }

            for (int i = 0; i < clusters.Length; i++)
            {
                int[] clusterCount = new int[k];

                for (int j = 0; j < vectors.Length; j++)
                {
                    if (vectors[j].Cluster == clusters[i]) {
                        clusterCount[i]++;
                    }
                }
                
                Console.WriteLine("Done: " + clusterCount[i]);
            }

            //readLine to prevent console from closing
            Console.ReadLine();
        }

        private static Vector[] assignVectorsToClosestCluster(Vector[] vectors, Cluster[] clusters) {
            for (int i = 0; i < vectors.Length; i++)
            {
                double closestDistance = Double.PositiveInfinity;
                for (int j = 0; j < clusters.Length; j++)
                {
                    double distance = vectors[i].getEuclideanDistance(clusters[j].Centroid);
                    if (distance < closestDistance) {
                        closestDistance = distance;
                        vectors[i].Cluster = clusters[j];
                    }
                }
            }

            return vectors;
        }

        private static Cluster[] recomputeCentroids(Cluster[] clusters, Vector[] vectors) {
            for (int i = 0; i < clusters.Length; i++)
            {
                float[] mean = new float[32];
                int counter = 0;

                for (int j = 0; j < vectors.Length; j++)
                {
                    if (vectors[j].Cluster == clusters[i])
                    {
                        int[] location = vectors[j].Location;
                        for (int h = 0; h < location.Length; h++)
                        {
                            mean[h] += location[h];
                        }
                        counter++;
                    }

                }

                for (int j = 0; j < mean.Length; j++)
                {
                    if (mean[j] == 0 || counter == 0)
                    {
                        mean[j] = 0;
                    }
                    else {
                        mean[j] = (float) mean[j] / (float) counter;
                    }
                }

                clusters[i].Centroid = mean;
            }

            return clusters;
        }

        private static Vector[] initiateVectors()
        {
            Vector[] vectors = new Vector[100];
            CSVReader reader = new CSVReader("WineData.csv", ',');
            int[,] offers = reader.read();

            for (int i = 0; i < offers.GetLength(0); i++)
            {
                int[] offer = new int[100];
                Vector vector = new Vector();
                for (int j = 0; j < offers.GetLength(1); j++)
                {
                    vector.addOffer(offers[i, j]);
                }
                vectors[i] = vector;

            }
            return vectors;
        }

        private static Cluster[] initiateClusters(Vector[] vectors) {
            Cluster[] clusters = new Cluster[k];
            for (int i = 0; i < k; i++)
            {
                float[] location = Array.ConvertAll(vectors[i].Location, n => (float) n );
                Cluster cluster = new Cluster(location);
                clusters[i] = cluster;
            }
            return clusters;
        }
    }
}
