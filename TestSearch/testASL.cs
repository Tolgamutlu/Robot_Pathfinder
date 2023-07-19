using RobotNav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSearch
{
    [TestFixture]

    public class testASL
    {
        string fileName = "sample.txt";

        private Agent agent;
        private Env env;
        private List<Wall> walls;
        private Goals goals;

        private ASL asl;

        [SetUp]
        public void SetUp()
        {
            env = new Env("[5,5]");
            walls = new List<Wall>();

        }

        [Test]

        public void testMazeInit()
        {
            asl = new ASL(fileName, new Agent("1,1"), env, walls, new Goals("1,1"), 5);

            Assert.That(asl.Maze.Height, Is.EqualTo(5));
            Assert.That(asl.Maze.Width, Is.EqualTo(5));
        }

        [Test]

        public void testAgentOnGoal()                                           //if agent is on goal, not only should it find it, but it shouldnt explore any other nodes
        {
            agent = new Agent("1,1");
            goals = new Goals("1,1");

            asl = new ASL(fileName, agent, env, walls, goals, 5);
            asl.startSearch();

            Assert.IsTrue(asl.getGoalFound);

            Assert.That(asl.number_of_nodes == 0);
        }

        [Test]

        public void testGoal()                                                      //can goal be found
        {
            agent = new Agent("0,0");
            goals = new Goals("3,3");

            asl = new ASL(fileName, agent, env, walls, goals, 10);
            asl.startSearch();

            Assert.IsTrue(asl.getGoalFound);
        }

        [Test]

        public void testGoalOutOfReach()                                            //if goal is out of array, it should not be found
        {
            agent = new Agent("0,0");
            goals = new Goals("10,10");

            asl = new ASL(fileName, agent, env, walls, goals, 10);
            asl.startSearch();

            Assert.IsFalse(asl.getGoalFound);
        }

        [Test]

        public void testWallBlockGoal()                                 //if wall blocks goal, it shouldnt be found
        {
            agent = new Agent("0,0");
            goals = new Goals("4,4");

            Wall w = new Wall("1,1,10,10");
            walls.Add(w);

            asl = new ASL(fileName, agent, env, walls, goals, 10);
            asl.startSearch();

            Assert.IsFalse(asl.getGoalFound);
        }

        [Test]

        public void testWallPlacedOnGoal()                              //if a wall is placed on goal, it should not be found
        {
            agent = new Agent("0,0");
            goals = new Goals("1, 1");

            Wall w = new Wall("1,1,1,1");
            walls.Add(w);

            asl = new ASL(fileName, agent, env, walls, goals, 10);
            asl.startSearch();

            Assert.IsFalse(asl.getGoalFound);
        }

    }
}
