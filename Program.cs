using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace LowestCostSearch
{
    class Program
    {
        public static bool firstItem = true;

        public static bool searchComplete = false;

        public static List<City> locations = new List<City>();//complete list of cities in the collection
        public static List<Edge> edges = new List<Edge>();
        public static List<Edge> potentialEdges = new List<Edge>();//list of current potential paths to explore
        public static List<City> potentialNodes = new List<City>();
        public static List<Edge> exploredEdges = new List<Edge>(); //list of previously explored paths
        public static List<City> exploredNodes = new List<City>();
        
        public static City startPoint;
        public static City endPoint;
        
        static void Main(string[] args)
        {

            //intialise nodes

            City n1 = new City("Frankfurt");
            City n2 = new City("Mannheim");
            City n3 = new City("Wurzburg");
            City n4 = new City("Kassell");
            City n5 = new City("Karlsruhe");
            City n6 = new City("Erfurt");
            City n7 = new City("Nurnberg");
            City n8 = new City("stuttgart");
            City n9 = new City("Augsberg");
            City n10 = new City("Munchen");
            //add all into locations list
            locations.AddMany(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10);
            //initialise edges
            //frankfurt node edges
            Edge e1 = new Edge(n1, n2, 85f);
            Edge e2 = new Edge(n1, n3, 217f);
            Edge e3 = new Edge(n1, n4, 173f);
            //Mannheim node edges
            Edge e4 = new Edge(n2, n5, 250f);
            //Wurzburg node edges
            Edge e5 = new Edge(n3, n6, 186f);
            Edge e6 = new Edge(n3, n7, 103f);
            //Nurnberg node edges
            Edge e7 = new Edge(n7, n8, 183f);
            Edge e8 = new Edge(n7, n10, 167f);
            //Kassel node edges
            Edge e9 = new Edge(n4, n10, 502f);
            //Augsberg node edges
            Edge e10 = new Edge(n9, n10, 84f);
            //add to collection of edges
            edges.AddMany(e1, e2, e3, e4, e5, e6, e7, e8, e9, e10);
            Console.WriteLine("cities in collection: " + locations.Count);
            Console.WriteLine("edges in collection: " + edges.Count);

            SetStartPoint();


        }

        //finds next set of potential nodes to check
        //TODO: Pass in starting node as an argument and reset every call until final node is reached
        public static void Examine(City nextNode)
        {
            //search list of edges for any paths where the parent node's name matches our start point name
            potentialNodes.Add(nextNode);//add start point to OPEN list
            bool examinationComplete = false;

            if (!examinationComplete)
            {
                foreach (City potential in potentialNodes.ToList())
                {
                    foreach (Edge edge in edges)
                    {
                        if (edge.point1.name == potential.name)
                        {                           
                            potentialNodes.Add(edge.point2);
                            potentialEdges.Add(edge);//add potential edge to collection as well as nodes
                            Console.WriteLine("potential next node " + edge.point2.name);
                        }
                    }
                }
                examinationComplete = true;
                foreach (City item in potentialNodes.ToList())//much cleaner way of doing this
                {
                    if (item.name == nextNode.name)
                    {
                        exploredNodes.Add(nextNode);
                        potentialNodes.Remove(nextNode);
                    }
                }
                //exploredNodes.Add(potentialNodes.First());//add last added node to the CLOSED list
                //potentialNodes.Remove(potentialNodes.First());//remove last added node

                //TODO: create new method to explore. Use: Orderby COST in ascending order and use whatever is at the top
                //to define shortest currently open path
            }
            Compare(nextNode);//pass the explored node into the compare method so we can find the shortest route
            //Console.ReadLine();           
        }

        static void Compare(City option)
        {
            if (!searchComplete)
            {
                potentialEdges.OrderBy(x => x.cost).ToList();//order the List in ascending order by cost
                Console.WriteLine("shortest route: " + potentialEdges.First().point2.name);//so far so good
                potentialEdges.Remove(potentialEdges.First());//PLEEEEEASE
                if (potentialEdges.First().point2.name == endPoint.name)
                {
                    Console.WriteLine("shortest route found! Arrived at " + potentialEdges.First().point2.name);
                    Console.ReadLine();
                }
                else
                {
                    Examine(potentialEdges.First().point2);
                }
                //Console.ReadLine();
            }
            else
            {
                potentialEdges.Clear();
                Console.WriteLine("shortest route found");
            }
        }

        static void SetStartPoint()
        {
            Console.WriteLine("Enter name of start node (case sensitive)");
            string response;
            response = Console.ReadLine();
            var item = locations.FirstOrDefault(o => o.name == response);
            if (item != null)
            {
                Console.WriteLine(item.name + " is the start point!");
                startPoint = item;
                SetEndPoint();
            }
            else
            {
                Console.WriteLine("No Match");
                SetStartPoint();
            }
        }

        static void SetEndPoint()
        {
            Console.WriteLine("Enter name of end node (case sensetive)");
            string response;
            response = Console.ReadLine();
            var item = locations.FirstOrDefault(o => o.name == response);
            if (item != null)
            {
                Console.WriteLine(item.name + " is the end point!");
                endPoint = item;
                Console.WriteLine("Type start to start");
                response = Console.ReadLine();
                if (response == "start")
                {
                    Examine(startPoint);
                }
            }
            else
            {
                Console.WriteLine("No Match");
                SetEndPoint();
            }
        }

        

        
    }
}
