﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace NearestNeighbourTSP
{
    class TSPFileReader
    {
        public static Graph.Point [] ReadTspFile(string tspFilePath)
        {
            var file = File.ReadLines(tspFilePath);
            List<Graph.Point> points = new List<Graph.Point>();

            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            bool readData = false;
            foreach (var item in file)
            {
                if (item.Contains("NODE_COORD_SECTION"))
                {
                    readData = true;
                    continue;
                }
                if (item.Contains("EOF"))
                {
                    readData = false;
                }
                if (readData)
                {
                    var splited = item.Split(' ');
                    points.Add(new Graph.Point(int.Parse(splited[0]), new Vector2(float.Parse(splited[1]), float.Parse(splited[2]))));
                }
            }

            return points.ToArray();
        }
    }
}
