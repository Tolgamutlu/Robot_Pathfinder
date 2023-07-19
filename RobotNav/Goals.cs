using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    public class Goals
    {
        //fields
        private List<Node> _node = new List<Node>();                                                    //multiple goals, therefore make a list of each node its on

        //constructors
        public Goals(string arr)
        {
            filterString fs = new filterString(arr);
            List<int> coOrdinate = fs.regex_filter();                                                   //using regex to get digits from text file
            for (int i = 0; i < coOrdinate.Count; i++)
            {
                if(i % 2 == 1)                                                                          //IF i IS NOT EVEN
                {
                    Node loc = new Node(coOrdinate[i - 1], coOrdinate[i]);                              //make a new Node then add it to the node list
                    _node.Add(loc); 
                }
            }
        }
        //methods
        public bool isAtGoal(Node n)                                                                    //Goal checks whether any of the nodes in the list has the same X and Y as Node parsed
        {
            foreach (Node node in _node)
            {
                if(node.X == n.X && node.Y == n.Y)
                {
                    return true;
                }
            }
            return false;
        }

        //getters + setters
        public List<Node> getGoalNodes
        {
            get { return _node; }
        }
    }
}
