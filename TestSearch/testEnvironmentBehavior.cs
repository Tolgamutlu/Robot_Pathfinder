using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RobotNav;

namespace TestSearch
{
    [TestFixture]
    public class testEnvironmentBehavior
    {
        private Agent agent;
        private Env env;
        private Wall wall;
        private Goals goals;
        private filterString fs;
        private Node n;

        [Test]

        public void testFilterString()                      //we should be able to extract all digits no matter what was parsed
        {
            fs = new filterString("[1]2[3]33}{");
            List<int> regex = fs.regex_filter();

            string extractRegex = "";
            foreach(int i in regex)
            {
                extractRegex += i.ToString();
            }

            Assert.That(extractRegex, Is.EqualTo("12333"));

        }

        [Test]

        public void testNode()                              //Node for objects can be initialised and assigns correct info to x and y var
        {
            Node n = new Node(1, 1);
            n.X = 1;
            n.Y = 1;
        }

        [Test]

        public void testAgentMovement()                     //tests if agent movements update nodes
        {
            agent = new Agent("[0,0]");

            Assert.That(agent.Node.X, Is.EqualTo(0));
            Assert.That(agent.Node.Y, Is.EqualTo(0));

            agent.Node.X += 4;
            agent.Node.Y += 4;

            Assert.That(agent.Node.X, Is.EqualTo(4));
            Assert.That(agent.Node.Y, Is.EqualTo(4));

        }

        [Test]

        public void testEnv()                               //tests if height and width are correctly initialised
        {
            env = new Env("[10,8]");

            Assert.That(env.Height, Is.EqualTo(10));
            Assert.That(env.Width, Is.EqualTo(8));

        }

        [Test]

        public void testGoal()                              //tests whether all goals specified are made and agent is able to move on and off goals
        {
            goals = new Goals("[10,10],|[1,1] [2,5]");
            agent = new Agent("10,10");
            Assert.That(goals.getGoalNodes.Count, Is.EqualTo(3));
            Assert.True(goals.isAtGoal(agent.Node));

            agent.Node.X = 1;

            Assert.IsFalse(goals.isAtGoal(agent.Node));

            agent.Node.Y = 1;

            Assert.IsTrue(goals.isAtGoal(agent.Node));
        }

        [Test]

        public void testWall()                              //tests whether the wall is able to detect when an agent is inside of it
        {
            wall = new Wall("(2,0,2,2)");
            
            agent = new Agent("[2][2]");

            Assert.IsFalse(wall.isAtWall(agent.Node));

            agent.Node.Y--;

            Assert.IsTrue(wall.isAtWall(agent.Node));


        }
    }
}
