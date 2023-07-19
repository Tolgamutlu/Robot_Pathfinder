using RobotNav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSearch
{
    [TestFixture]

    public class testAS
    {
        string fileName = "sample.txt";

        private Agent agent;
        private Env env;
        private List<Wall> walls;
        private Goals goals;

        private AS aStar;

        [SetUp]
        public void SetUp()
        {
            env = new Env("[5,5]");
            walls = new List<Wall>();

        }

        [Test]

        public void testMazeInit()
        {
            aStar = new AS(fileName, new Agent("1,1"), env, walls, new Goals("1,1"));

            Assert.That(aStar.Maze.Height, Is.EqualTo(5));
            Assert.That(aStar.Maze.Width, Is.EqualTo(5));
        }

        [Test]

        public void testAgentOnGoal()                                           //if agent is on goal, not only should it find it, but it shouldnt explore any other nodes
        {
            agent = new Agent("1,1");
            goals = new Goals("1,1");

            aStar = new AS(fileName, agent, env, walls, goals);
            aStar.startSearch();

            Assert.IsTrue(aStar.getGoalFound);

            foreach (bool nodeVisited in aStar.getVisited)
            {
                Assert.IsFalse(nodeVisited);
            }

            Assert.That(aStar.number_of_nodes == 0);
        }

        [Test]

        public void testGoal()                                                      //can goal be found
        {
            agent = new Agent("0,0");
            goals = new Goals("4,4");

            aStar = new AS(fileName, agent, env, walls, goals);
            aStar.startSearch();

            Assert.IsTrue(aStar.getGoalFound);
        }

        [Test]

        public void testGoalOutOfReach()                                            //if goal is out of array, it should not be found
        {
            agent = new Agent("0,0");
            goals = new Goals("10,10");

            aStar = new AS(fileName, agent, env, walls, goals);
            aStar.startSearch();

            Assert.IsFalse(aStar.getGoalFound);
        }

        [Test]

        public void testWallBlockGoal()                                 //if wall blocks goal, it shouldnt be found
        {
            agent = new Agent("0,0");
            goals = new Goals("4,4");

            Wall w = new Wall("1,1,10, 10");
            walls.Add(w);

            aStar = new AS(fileName, agent, env, walls, goals);
            aStar.startSearch();

            Assert.IsFalse(aStar.getGoalFound);
        }

        [Test]

        public void testWallPlacedOnGoal()                              //if a wall is placed on goal, it should not be found
        {
            agent = new Agent("0,0");
            goals = new Goals("1, 1");

            Wall w = new Wall("1,1,1,1");
            walls.Add(w);

            aStar = new AS(fileName, agent, env, walls, goals);
            aStar.startSearch();

            Assert.IsFalse(aStar.getGoalFound);
        }

        [Test]

        public void testHeuristic()                                             //test whether the Heuristic is correct
        {
            agent = new Agent("3,3");
            goals = new Goals("4,2");

            aStar = new AS(fileName, agent, env, walls, goals);

            Assert.That(aStar.GetHeuristic(""), Is.EqualTo(2));

            Assert.That(aStar.GetHeuristic("U"), Is.EqualTo(1));

            Assert.That(aStar.GetHeuristic("UR"), Is.EqualTo(0));
        }
    }
}
