using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Net;

namespace RobotNav
{
    public class AS
    {
        //VARIALBES THAT WILL ONLY BE USED FOR OUTPUT
        public int number_of_nodes = 0;
        private string fileName;
        //fields
        private Agent Robot;
        private Env _maze;
        private List<Wall> _wall;
        private Goals _goals;

        private List<string> Frontier;

        private bool[,] visited;
        private bool goalFound;
        //constructors
        public AS(string fn, Agent a, Env e, List<Wall> w, Goals g)
        {
            fileName = fn;

            Robot = a;
            _maze = e;
            _wall = w;
            _goals = g;

            Frontier = new List<string>();                                                      //creates a list of strings
            Frontier.Add("");

            visited = new bool[Maze.Height, Maze.Width];                                        //initalises 2d array which is the same size as environment, each node in visited is false by default
            goalFound = false;
        }

        //methods
        public bool Valid(string moves)
        {
            //setting Node X and Y positions to i and j. COULD NOT CREATE A NEW NODE() WITH ROBOT COORDINATES AS CHANGES IN NEW NODE() CAUSES CHANGES IN THE POSITION OF ROBOT
            int i = Robot.Node.X;
            int j = Robot.Node.Y;
            foreach (char move in moves)                                                         //Increment/Decrement variables i and j based on moves parsed in Valid()
            {
                if (move == 'U')
                {
                    j--;
                }
                if (move == 'L')
                {
                    i--;
                }

                if (move == 'D')
                {
                    j++;
                }

                if (move == 'R')
                {
                    i++;
                }
            }

            //checks validity of new Node, once it parses all these tests then it can be queued
            if (i < 0 || i > _maze.Width - 1 || j < 0 || j > _maze.Height - 1)                    //checks that the CoOrdinates given are still on the board
            {
                return false;
            }

            if (visited[j, i])
            {
                return false;                                                                   //if the square has been visited, its not valid
            }

            foreach (Wall w in _wall)                                                           //each wall checks whether the Node that the CoOrdinates land on is not inside them
            {
                if (w.isAtWall(new Node(i, j)))
                {
                    return false;
                }
            }

            return true;                                                                        //if the Node is not in the wall, out of the environment and has not been visited, the Node is valid
        }

        public void startSearch()
        {
            try
            {
                string path = "";
                while (!FinishSearch(path))                                                         //only returns true if goalFound = true
                {
                    if (!Frontier.Any())                                                            //RETURNS TRUE EVEN IF FRONTIER HAS EMPTY STRINGS
                    {
                        throw new IndexOutOfRangeException();                                       //CODE WILL ENTER INFINATE LOOP IF NOT SPECIFIED
                    }

                    path = GetBestNode();                                                           //path = lowest node number_of_nodes
                    //Console.WriteLine(path);
                    number_of_nodes += 1;
                    List<char> Direction = new List<char> { 'U', 'L', 'D', 'R' };

                    foreach (char c in Direction)
                    {
                        string enqueue = path + c;                                                  //add the character onto the end of the current path, these will be read as individual moves in FinishSearch() if valid

                        if (Valid(enqueue))
                        {
                            Frontier.Add(enqueue);                                                  //If the node is valid, add the current set of directions onto the queue
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! path cannot be found!");
            }

        }

        public string GetBestNode()
        {
            string bestNode = "";
            int minCost = 99999;

            foreach (string node in Frontier)
            {
                int cost = node.Length + GetHeuristic(node);                                    //gives each node in frontier a cost using Heuristic
                if (cost < minCost)                                                             //The node with the lowest cost is stored as BestNode
                {
                    minCost = cost;
                    bestNode = node;
                }
            }

            Frontier.Remove(bestNode);                                                          //the node is then removed from the frontier, then returned

            return bestNode;
        }

        public int GetHeuristic(string moves)
        {
            int i = Robot.Node.X;                                                               //robots default location
            int j = Robot.Node.Y;                                                               //COULD NOT DO | new Node original = Robot.Node | BECAUSE EVERYTIME Robot.Node CHANGED, IT WOULD ALSO CHANGE ORIGINAL

            foreach (char move in moves)
            {
                if (move == 'U')
                {
                    j--;
                }
                if (move == 'L')                                                                //moves robot in directions that were parsed
                {
                    i--;
                }

                if (move == 'D')
                {
                    j++;
                }
                if (move == 'R')
                {
                    i++;
                }
            }

            int heuristic = 99999;

            foreach (Node goalNode in _goals.getGoalNodes)                                      //from the current node, parse in each goal node
            {                                                                                   //and calculate the distance from the node to the goal node
                int result = Math.Abs(goalNode.X - i) + Math.Abs(goalNode.Y - j);               //using Manhattans distance as the Heuristic

                if (result < heuristic)
                {
                    heuristic = result;                                                         //the goal thats closest will be used as the heuristic
                }
            }

            return heuristic;
        }

        public bool FinishSearch(string moves)                                  //checks if the path reaches goal
        {
            int i = Robot.Node.X;                                               //robots default location
            int j = Robot.Node.Y;                                               //COULD NOT DO | new Node original = Robot.Node | BECAUSE EVERYTIME Robot.Node CHANGED, IT WOULD ALSO CHANGE ORIGINAL
            foreach (char move in moves)
            {
                if (move == 'U')
                {
                    Robot.Node.Y--;
                }
                if (move == 'L')
                {
                    Robot.Node.X--;                                             //moves robot in directions that were parsed
                }

                if (move == 'D')
                {
                    Robot.Node.Y++;
                }

                if (move == 'R')
                {
                    Robot.Node.X++;
                }
            }


            if (_goals.isAtGoal(Robot.Node))                                    //if the Robot Node is at the Goal
            {
                goalFound = true;
                Console.WriteLine(fileName + " A* " + number_of_nodes);
                Console.WriteLine("Path : " + moves);
            }
            else
            {
                visited[Robot.Node.Y, Robot.Node.X] = true;                     //makes square visited
                Robot.Node = new Node(i, j);                                    //sets the robots location back to the start
            }

            return goalFound;                                                   //returns false unless goal was reached
        }

        //getter
        public Env Maze
        {
            get { return _maze; }
        }

        public bool getGoalFound
        {
            get { return goalFound; }
        }

        public bool[,] getVisited
        {
            get { return visited; }
        }
    }
}
