﻿using System;
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
        Values values;

        static void Main(string[] args)
        {
            string pathFile = @"TSP\kroA100.tsp";
            Graph.Point [] points = TSPFileReader.ReadTspFile(pathFile);
            Graph.Graph graph = new Graph.Graph(points);

            Console.WriteLine("Start Algorithm:");
            values - new Values(pathFile,"NN");

            values.StarTime();

            double bestLength;
            for (int i = 0; i < graph.dimension; i++)
            {
                float pathLength = FindPath(i, graph);
                if(i == 0)
                {
                    bestLength = pathLength;
                    Values.AddNewValues(i, pathLength);
                } 
                
                if(pathLength < bestLength)
                {
                    bestLength = pathLength;
                    Values.AddNewValues(i, pathLength);
                }
                
                Console.WriteLine("For vertex {0} path is: {1}", i, pathLength);
            }


            values.StopTime();
            Console.WriteLine("End");

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
   
    }
}
