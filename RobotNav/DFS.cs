using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    public class DFS
    {
        //VARIALBES THAT WILL ONLY BE USED FOR OUTPUT
        public int number_of_nodes = 0;
        private string fileName;
        //fields
        private Agent Robot;
        private Env _maze;
        private List<Wall> _wall;
        private Goals _goals;

        private Stack<string> Frontier;                                                     //Stack in C# allows methods .Pop() which removes from start of stack and returns value and .Push() which pushes value onto the start of the stack

        private bool[,] visited;                                                            //checks whether each tile has been visited, default boolean value is False
        private bool goalFound;
        //constructors
        public DFS(string fn, Agent a, Env e, List<Wall> w, Goals g)
        {
            fileName = fn;

            Robot = a;
            _maze = e;
            _wall = w;
            _goals = g;

            Frontier = new Stack<string>();
            Frontier.Push("");

            goalFound = false;
            visited = new bool[Maze.Height, Maze.Width];                                    //initalises 2d array which is the same size as environment, each node in visited is false by default
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
                while (!FinishSearch(path))                                                     //only returns true if goalFound = true                               
                {
                    path = Frontier.Pop();                                                      //takes last value of Frontier sets path as its return value
                    //Console.WriteLine(path);
                    number_of_nodes += 1;
                    List<char> Direction = new List<char> { 'R', 'D', 'L', 'U' };               //FLIPPED AS PUSH() ADDS TO THE START OF THE STACK

                    foreach (char c in Direction)
                    {
                        string push = path + c;                                                 //add the character onto the end of the current path, these will be read as individual moves in FinishSearch() if valid

                        if (Valid(push))
                        {
                            Frontier.Push(push);                                                //If the Node is valid, add the current set of directions onto the Stack
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
            int i = Robot.Node.X;                                                           //robots default node
            int j = Robot.Node.Y;                                                           //COULD NOT DO | new Node original = Robot.Node | BECAUSE EVERYTIME Robot.Node CHANGED, IT WOULD ALSO CHANGE ORIGINAL
            foreach (char move in moves)
            {
                if (move == 'U')
                {
                    Robot.Node.Y--;
                }
                if (move == 'L')
                {
                    Robot.Node.X--;
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


            if (_goals.isAtGoal(Robot.Node))                                                //once the robot has moved, if its at the goal node, write it into the console
            {
                goalFound = true;
                Console.WriteLine(fileName + " DFS " + number_of_nodes);
                Console.WriteLine("Path : " + moves);
            }
            else
            {
                visited[Robot.Node.Y, Robot.Node.X] = true;                                 //If Robot is not on goal
                Robot.Node = new Node(i, j);                                                //mark node it is currently on as Visited
            }                                                                               //sets the Robot node back to the starting node

            return goalFound;                                                               //returns false unless goal was reached
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
