﻿using RobotNav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSearch
{
    [TestFixture]

    public class testBFS
    {
        string fileName = "sample.txt";

        private Agent agent;
        private Env env;
        private List<Wall> walls;
        private Goals goals;

        private BFS bfs;

        [SetUp]
        public void SetUp()
        {
            env = new Env("[10,10]");
            walls = new List<Wall>();

        }

        [Test]

        public void testMazeInit()
        {
            bfs = new BFS(fileName, new Agent("1,1"), env, walls, new Goals("1,1"));

            Assert.That(bfs.Maze.Height, Is.EqualTo(10));
            Assert.That(bfs.Maze.Width, Is.EqualTo(10));
        }

        [Test]

        public void testAgentOnGoal()                                           //if agent is on goal, not only should it find it, but it shouldnt explore any other nodes
        {
            agent = new Agent("1,1");
            goals = new Goals("1,1");

            bfs = new BFS(fileName, agent, env, walls, goals);
            bfs.startSearch();

            Assert.IsTrue(bfs.getGoalFound);

            foreach (bool nodeVisited in bfs.getVisited)
            {
                Assert.IsFalse(nodeVisited);
            }

            Assert.That(bfs.number_of_nodes == 0);
        }

        [Test]

        public void testGoal()                                                      //can goal be found
        {
            agent = new Agent("0,0");
            goals = new Goals("9,9");

            bfs = new BFS(fileName, agent, env, walls, goals);
            bfs.startSearch();

            Assert.IsTrue(bfs.getGoalFound);
        }

        [Test]

        public void testGoalOutOfReach()                                            //if goal is out of array, it should not be found
        {
            agent = new Agent("0,0");
            goals = new Goals("10,10");

            bfs = new BFS(fileName, agent, env, walls, goals);
            bfs.startSearch();

            Assert.IsFalse(bfs.getGoalFound);
        }

        [Test]

        public void testWallBlockGoal()                                 //if wall blocks goal, it shouldnt be found
        {
            agent = new Agent("0,0");
            goals = new Goals("9, 9");

            Wall w = new Wall("1,1,10,10");
            walls.Add(w);

            bfs = new BFS(fileName, agent, env, walls, goals);
            bfs.startSearch();

            Assert.IsFalse(bfs.getGoalFound);
        }

        [Test]

        public void testWallPlacedOnGoal()                              //if a wall is placed on goal, it should not be found
        {
            agent = new Agent("0,0");
            goals = new Goals("1, 1");

            Wall w = new Wall("1,1,1,1");
            walls.Add(w);

            bfs = new BFS(fileName, agent, env, walls, goals);
            bfs.startSearch();

            Assert.IsFalse(bfs.getGoalFound);
        }

    }
}
