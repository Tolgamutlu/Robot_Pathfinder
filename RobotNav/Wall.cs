using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    public class Wall
    {
        //fields
        private Node _node;

        private int _width;
        private int _height;
        
        //constructors
        public Wall(string arr)
        {
            filterString fs = new filterString(arr);
            List<int> coOrdinate = fs.regex_filter();

            _node = new Node(coOrdinate[0], coOrdinate[1]);                                                 //using regex to get digits from text file
            _width = coOrdinate[2];
            _height = coOrdinate[3];
        }

        //methods
        public bool isAtWall(Node n)                                                                        //wall returns whether the node parsed is within itself
        {
            if (n.X > _node.X + _width - 1|| n.Y > _node.Y + _height - 1)
            {
                return false;
            }
            else if(n.X < _node.X || n.Y < _node.Y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //getters + setters
        public int Width
        {
            get { return _width; }
        }
        
        public int Height
        {
            get { return _height; }
        }
    }
}
