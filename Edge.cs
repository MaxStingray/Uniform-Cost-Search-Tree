using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//edges between nodes
namespace LowestCostSearch
{
    class Edge
    {
        public City point1;
        public City point2;

        public float cost;

        public Edge(City p1, City p2, float Cost)
        {
            point1 = p1; point2 = p2; cost = Cost;
        }
    }
}
