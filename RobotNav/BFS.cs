using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Runtime.InteropServices.ComTypes;

namespace RobotNav
{
    public class BFS
    {
        //VARIALBES THAT WILL ONLY BE USED FOR OUTPUT & TESTING
        public int number_of_nodes = 0;
        public string fileName;
        //fields
        private Agent Robot;
        private Env _maze;
        private List<Wall> _wall;
        private Goals _goals;

        private Queue<string> Frontier;

        private bool[,] visited;
        private bool goalFound;
        //constructors
        public BFS(string fn, Agent a, Env e, List<Wall> w, Goals g)
        {
            fileName = fn;

            Robot = a;
            _maze = e;
            _wall = w;
            _goals = g;

            Frontier = new Queue<string>();                                                     //creates Queue, Enqueue() adds to end of queue, Dequeue() removes first element
            Frontier.Enqueue("");

            visited = new bool[Maze.Height, Maze.Width];                                        //initalises 2d array which is the same size as environment, each node in visited is false by default
            goalFound = false;
        }

        //methods
        public bool Valid(string moves)
        {
            //setting Node X and Y positions to i and j. COULD NOT CREATE A NEW NODE() WITH ROBOT COORDINATES AS CHANGES IN NEW NODE() CAUSES CHANGES IN THE POSITION OF ROBOT
            int i = Robot.Node.X;
            int j = Robot.Node.Y;
            foreach(char move in moves)                                                         //Increment/Decrement variables i and j based on moves parsed in Valid()
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
            if(i < 0 || i > _maze.Width - 1 || j < 0|| j > _maze.Height - 1)                    //checks that the CoOrdinates given are still on the board
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
                while (!FinishSearch(path))                                                          //only returns true if goalFound = true
                {
                    path = Frontier.Dequeue();                                                      //take the current path off the queue and set the return value into variable path
                    //Console.WriteLine(path);
                    number_of_nodes += 1;
                    List<char> Direction = new List<char> { 'U', 'L', 'D', 'R' };

                    foreach (char c in Direction)
                    {
                        string enqueue = path + c;                                                  //add the character onto the end of the current path, these will be read as individual moves in FinishSearch() if valid

                        if (Valid(enqueue))
                        {
                            Frontier.Enqueue(enqueue);                                              //If the node is valid, add the current set of directions onto the queue
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error! path cannot be found!");
            }
        }

        public bool FinishSearch(string moves)
        {
            int i = Robot.Node.X;                                                               //robots default Node
            int j = Robot.Node.Y;                                                               //COULD NOT DO | new Node original = Robot.Node | BECAUSE EVERYTIME Robot.Node CHANGED, IT WOULD ALSO CHANGE ORIGINAL
            foreach (char move in moves)
            {
                if (move == 'U')
                {
                    Robot.Node.Y--;
                }
                if (move == 'L')
                {
                    Robot.Node.X--;                                                             //moves robot in directions that were parsed
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


            if (_goals.isAtGoal(Robot.Node))                                                    //once the robot has moved, if its at the goal node, write it into the console
            {
                goalFound = true;
                Console.WriteLine(fileName + " BFS " + number_of_nodes);
                Console.WriteLine("Path : " + moves);
            }
            else
            {                                                                                   //If Robot is not on goal
                visited[Robot.Node.Y, Robot.Node.X] = true;                                     //mark node it is currently on as Visited
                Robot.Node = new Node(i,j);                                                     //sets the Robot node back to the starting node
            }

            return goalFound;                                                                   //returns false unless goal was reached
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
