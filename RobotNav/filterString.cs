using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RobotNav
{

    //this class is invoked when the numbers of the text file string need to be extracted using regex,
    //this allows the code to look cleaner instead of using it in the main class and allows for less repitition

    public class filterString
    {
        private string inputS;
        public filterString(string s)
        {
            inputS = s;
        }
        public List<int> regex_filter()     //matches digits, then loops through matches and returns them in List
        {
            List<int> result = new List<int>();
            Regex r = new Regex(@"\d+");

            foreach(Match m in r.Matches(inputS))
            {
                result.Add(Int32.Parse(m.Value));
            }
            return result;
        }
    }
}
