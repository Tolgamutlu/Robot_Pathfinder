using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobotNav
{
    public class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            
            StreamReader reader = new StreamReader(fileName);

            Env floor = new Env(reader.ReadLine());

            Agent agent = new Agent(reader.ReadLine());

            Goals goal = new Goals(reader.ReadLine());                                                  //reading in all the coordinates in file and parsing it through each objects respective constructors

            List<Wall> walls = new List<Wall>();
            while (!reader.EndOfStream)
            {
                Wall wall = new Wall(reader.ReadLine());
                walls.Add(wall);
            }

            switch (args[1].ToUpper())
            {
                case "BFS":
                    Console.WriteLine("BFS Running: ");
                    BFS bfs = new BFS(fileName, agent, floor, walls, goal);
                    bfs.startSearch();
                    break;
                case "DFS":
                    Console.WriteLine("DFS Running: ");
                    DFS dfs = new DFS(fileName, agent, floor, walls, goal);
                    dfs.startSearch();
                    break;
                case "GBFS":
                    Console.WriteLine("GBFS Running: ");
                    GBFS gbfs = new GBFS(fileName, agent, floor, walls, goal);
                    gbfs.startSearch(); 
                    break;
                case "A*":
                    Console.WriteLine("A STAR Running: ");
                    AS aStar = new AS(fileName, agent, floor, walls, goal);
                    aStar.startSearch();
                    break;
                case "DLS":
                    Console.Write("Algorithm depth limit: ");
                    int depth = Int32.Parse(Console.ReadLine());
                    DLS dls = new DLS(fileName, agent, floor, walls, goal, depth);
                    dls.startSearch();
                    break;
                case "A*L":
                    Console.Write("Algorithm depth limit: ");
                    int depth2 = Int32.Parse(Console.ReadLine());
                    ASL asl = new ASL(fileName, agent, floor, walls, goal, depth2);
                    asl.startSearch();
                    break;
                default:
                    Console.WriteLine("INVALID INPUT");
                    break;
            }
        }
    }
}
