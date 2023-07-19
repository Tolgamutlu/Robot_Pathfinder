using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RobotNav
{
    public class Node
    {
        //fields
        private int _x;
        private int _y;

        

        //constructors
        public Node(int x, int y)                       //Each goal, wall and Agent has a Node which dictates where in the environment they are located
        {
            _x = x;
            _y = y;
        }

        //getters + setters
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
