using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means_clustering
{
    class Program
    {
        private static int k = 3;
        private static int iterations = 5;

        static void Main(string[] args)
        {
            Vector[] vectors = initiateVectors();
            Cluster[] clusters = initiateClusters(vectors);

            int counter = 0;
            while (counter < iterations) {
                vectors = assignVectorsToClosestCluster(vectors, clusters);
                clusters = recomputeCentroids(clusters, vectors);

                counter++;
            }

            printClusterCount(vectors, clusters);
            printSSE(vectors, clusters);

            //readLine to prevent console from closing
            Console.ReadLine();
        }

        private static void printClusterCount(Vector[] vectors, Cluster[] clusters) {
            for (int i = 0; i < clusters.Length; i++)
            {
                int[] clusterCount = new int[k];

                for (int j = 0; j < vectors.Length; j++)
                {
                    if (vectors[j].Cluster == clusters[i])
                    {
                        clusterCount[i]++;
                    }
                }

                Console.WriteLine("\nCluster " + (i + 1) + " : " + clusterCount[i] + "\n");
                printOfferCount(vectors, clusters);
                
            }
        }

        private static void printSSE(Vector[] vectors, Cluster[] clusters) {

            float tempSum;
            float result = 0;

            for (int i = 0; i < clusters.Length; i++)
            {
                tempSum = 0;
                for (int j = 0; j < vectors.Length; j++)
                {
                    if (vectors[j].Cluster == clusters[i])
                    {
                        tempSum += (float)Math.Pow(vectors[j].getEuclideanDistance(clusters[i].Centroid), 2);
                    }
                }
                result += tempSum;
            }

            Console.WriteLine("\nSSE: " + result);
        }
        private static void printOfferCount(Vector[] vectors, Cluster[] clusters) {
            for (int i = 0; i < clusters.Length; i++)
            {
                int[] offerCount = new int[32];
                for (int j = 0; j < vectors.Length; j++)
                {
                    if (vectors[j].Cluster == clusters[i])
                    {
                        for (int k = 0; k < vectors[j].Location.Count; k++)
                        {
                            offerCount[k] += vectors[j].Location[k];
                        }
                    }
                }

                List<int> finalList = new List<int>(offerCount).OrderByDescending(r => r).ToList(); ;
                for (int j = 0; j < offerCount.Length; j++)
                {
                    Console.WriteLine("Offer " + j + " : " + finalList[j]); 
                }
            }
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
                        List<int> location = vectors[j].Location;
                        for (int h = 0; h < location.Count; h++)
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
                    vector.Location.Add(offers[i, j]);
                }
                vectors[i] = vector;


            }
            return vectors;
        }

        private static Cluster[] initiateClusters(Vector[] vectors) {
            Cluster[] clusters = new Cluster[k];

            
            Random r = new Random();
            List<int> randomIndices = new List<int>();
            for (int i = 0; i < k; i++)
            {
                int randomIndex = r.Next(0, 100);
                while (randomIndices.Contains(randomIndex)) {
                    randomIndex = r.Next(0, 100);
                }
                randomIndices.Add(randomIndex);

                float[] location = vectors[randomIndex].Location.Select<int, float>(x => x).ToArray();
                Cluster cluster = new Cluster(location);
                clusters[i] = cluster;
            }
            return clusters;
        }
    }
}
