using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means_clustering
{
    class CSVReader
    {   
        private string filePath;
        private char splitSymbol;

        public CSVReader(string filePath, char splitSymbol) {
            this.filePath = filePath;
            this.splitSymbol = splitSymbol;
        }
        public int[,] read() {
            using (StreamReader sr = new StreamReader(filePath))
            {
                int[,] offers = new int[100,32];
                int rowCounter = 0;
                string[] line;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split(splitSymbol);
                    for (int i = 0; i < line.Length; i++)
                    {
                        offers[i, rowCounter] = Int32.Parse(line[i]);
                    }
                    rowCounter++;
                }
                return offers;
            }
        }
    }
}
