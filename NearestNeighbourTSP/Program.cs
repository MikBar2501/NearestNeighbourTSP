using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace NearestNeighbourTSP
{
    class Program
    {
        public static List<String> toFile = new List<String>();

        static void Main(string[] args)
        {
            string pathFile = @"TSP\kroA100.tsp";
            Graph.Point [] points = TSPFileReader.ReadTspFile(pathFile);
            Graph.Graph graph = new Graph.Graph(points);
            Console.WriteLine("Start Algorithm:");
            toFile.Add("Nearest Neighbour Algorithm for " + pathFile);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            double bestLength;
            for (int i = 0; i < graph.dimension; i++)
            {
                float pathLength = FindPath(i, graph);
                if(i == 0)
                {
                    bestLength = pathLength;
                    toFile.Add("-|New best |" + i + "|" + pathLength);
                } 
                
                if(pathLength < bestLength)
                {
                    bestLength = pathLength;
                    toFile.Add("New best |" + i + "|" + pathLength);
                }
                
                Console.WriteLine("For vertex {0} path is: {1}", i, pathLength);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            toFile.Add("Time: " + elapsedTime);
            SaveToFile(toFile, "NN", "kroA100");
            Console.ReadLine();

        }

        public static float FindPath(int startPoint, Graph.Graph graph)
        {
            float pathLength = 0;
            int actualPoint = startPoint;
            //List<int> visited = new List<int>();
            List<int> unVisited = new List<int>();
            for(int i = 0; i < graph.dimension; i++)
            {
                unVisited.Add(i);
            }
            unVisited.Remove(actualPoint);            
            //visited.Add(actualPoint);
            while (unVisited.Count != 0)
            {
                float nearestPath = 0;
                int nearestPoint = 0;

                foreach (int vert in unVisited)
                {
                    if (nearestPath == 0)
                    {
                        nearestPath = graph.edgeWeight[actualPoint, vert];
                        nearestPoint = vert;
                    }

                    if (nearestPath > graph.edgeWeight[actualPoint, vert])
                    {
                        nearestPath = graph.edgeWeight[actualPoint, vert];
                        nearestPoint = vert;
                    }
                }

                pathLength += nearestPath;
                actualPoint = nearestPoint;
                unVisited.Remove(actualPoint);
            }
            pathLength += graph.edgeWeight[actualPoint, startPoint];

            return pathLength;
        }

        public static void SaveToFile(List<String> tofile, string algorithm, string startFile)
        {
            DateTime dt = DateTime.Now;
            string fileName = String.Format("{0:y yy yyy yyyy}", dt) + "-" + algorithm + "-" + startFile;
            using (StreamWriter sw = new StreamWriter(@"Files\"+ fileName +".txt"))
            {
                foreach (string line in tofile)
                {
                    sw.WriteLine(line);
                }
            }
        }
   
    }
}
