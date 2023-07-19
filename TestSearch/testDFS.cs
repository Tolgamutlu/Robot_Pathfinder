using RobotNav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSearch
{
    [TestFixture]

    public class testDFS
    {
        string fileName = "sample.txt";

        private Agent agent;
        private Env env;
        private List<Wall> walls;
        private Goals goals;

        private DFS dfs;

        [SetUp]
        public void SetUp()
        {
            env = new Env("[10,10]");
            walls = new List<Wall>();

        }

        [Test]

        public void testMazeInit()
        {
            dfs = new DFS(fileName, new Agent("1,1"), env, walls, new Goals("1,1"));

            Assert.That(dfs.Maze.Height, Is.EqualTo(10));
            Assert.That(dfs.Maze.Width, Is.EqualTo(10));
        }

        [Test]

        public void testAgentOnGoal()                                           //if agent is on goal, not only should it find it, but it shouldnt explore any other nodes
        {
            agent = new Agent("1,1");
            goals = new Goals("1,1");

            dfs = new DFS(fileName, agent, env, walls, goals);
            dfs.startSearch();

            Assert.IsTrue(dfs.getGoalFound);

            foreach (bool nodeVisited in dfs.getVisited)
            {
                Assert.IsFalse(nodeVisited);
            }

            Assert.That(dfs.number_of_nodes == 0);
        }

        [Test]

        public void testGoal()                                                      //can goal be found
        {
            agent = new Agent("0,0");
            goals = new Goals("9,9");

            dfs = new DFS(fileName, agent, env, walls, goals);
            dfs.startSearch();

            Assert.IsTrue(dfs.getGoalFound);
        }

        [Test]

        public void testGoalOutOfReach()                                            //if goal is out of array, it should not be found
        {
            agent = new Agent("0,0");
            goals = new Goals("10,10");

            dfs = new DFS(fileName, agent, env, walls, goals);
            dfs.startSearch();

            Assert.IsFalse(dfs.getGoalFound);
        }

        [Test]

        public void testWallBlockGoal()                                 //if wall blocks goal, it shouldnt be found
        {
            agent = new Agent("0,0");
            goals = new Goals("9, 9");

            Wall w = new Wall("1,1,10,10");
            walls.Add(w);

            dfs = new DFS(fileName, agent, env, walls, goals);
            dfs.startSearch();

            Assert.IsFalse(dfs.getGoalFound);
        }

        [Test]

        public void testWallPlacedOnGoal()                              //if a wall is placed on goal, it should not be found
        {
            agent = new Agent("0,0");
            goals = new Goals("1, 1");

            Wall w = new Wall("1,1,1,1");
            walls.Add(w);

            dfs = new DFS(fileName, agent, env, walls, goals);
            dfs.startSearch();

            Assert.IsFalse(dfs.getGoalFound);
        }

    }
}
