using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    public class Env
    {

        //fields
        private int _height;
        private int _width;

        private int[,] _mazeArray;

        //constructors
        public Env(string arr)                                                      //gets width and height and makes a 2D array (matrix)
        {
            filterString fs = new filterString(arr);
            List<int> CoOrdinate = fs.regex_filter();

            _height = CoOrdinate[0];
            _width = CoOrdinate[1];

            _mazeArray = new int[_height, _width];                                          //creates 5x11 matrix
        }

        //getters + setters
        public int[,] MazeArray
        {
            get { return _mazeArray; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int Width
        {
            get { return _width; }
        }
    }
}
