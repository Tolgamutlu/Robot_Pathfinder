using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RobotNav
{
    public class Agent
    {
        //fields
        public Node _node;                                                          //stores the node the agent is on

        //constructors
        public Agent(string arr)
        {
            filterString fs = new filterString(arr);                                //using regex to get digits from text file
            List<int> coOrdinate = fs.regex_filter();
            _node = new Node(coOrdinate[0], coOrdinate[1]);
        }
        //getters + setters
        public Node Node
        {
            get { return _node; }
            set { _node = value; }
        }
    }
}
