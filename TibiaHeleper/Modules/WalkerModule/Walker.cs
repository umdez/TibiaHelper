﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TibiaHeleper.MemoryOperations;
using TibiaHeleper.Simulators;
using TibiaHeleper.Windows;

namespace TibiaHeleper.Modules.WalkerModule
{
    [Serializable]
    class Walker : Module
    {
        public List<WalkerStatement> list;

        private Stack<Waypoint> _wayBack;

        public bool stopped { get; set; }
        public bool working { get; set; }
        public int actualStatementIndex;
        public int startStatementIndex;
        public int attemptsToRandomDirection;
        private Random _rand;
        bool trackerWorking;

        public int tolerance;

        private int _direction;
      //  private int lastDirection;
        //like in numeric block
        //8 - north
        //2 - south
        //4 - west
        //6 - east
        //1 - south-west
        //3 - south-east
        //7 - north-west
        //9 - north-east



        public Walker()
        {
            stopped = true;
            list = new List<WalkerStatement>();
            _wayBack = new Stack<Waypoint>();
            tolerance = 0;
            attemptsToRandomDirection = 15;
            _rand = new Random();
            actualStatementIndex = startStatementIndex = 0;
        }

        public void Run()
        {
            WalkerStatement statement;
            int listCount;
            lock (list) { listCount = list.Count(); }
            int temp;
 //           startTracking();
            actualStatementIndex = startStatementIndex;
            startStatementIndex = 0;

            while (working && actualStatementIndex < listCount)
            {
                lock (list)
                {
                    listCount = list.Count();
                    statement = list[actualStatementIndex];
                }
                //getting and setting on window list

                if (statement.type == StatementType.getType["Waypoint"] || statement.type == StatementType.getType["Stand"])//walk
                {
                    temp = tolerance;
                    if (statement.type == StatementType.getType["Stand"])
                        tolerance = 0;
                    if (!goToCoordinates((Waypoint)statement))// goes to the coordinates
                    {
                        tryTogetBack(); //if player is in not good position then goes back to the last reached waypoint
                    }
                    tolerance = temp;
                }
                else if (statement.type == StatementType.getType["Action"])//do action
                {
                    try
                    {
                        Action action = (Action)statement;
                        action.DoAction();
                    }
                    catch (Exception) { }
                }
                Thread.Sleep(50);

                actualStatementIndex++;
            }
            actualStatementIndex = 0;
            working = false;
            stopped = true;
            WindowsManager.menu.Update();
        }

        private bool tryTogetBack()
        {
            Waypoint waypoint;
            bool isStackEmpty;
            lock (_wayBack)
            {
                isStackEmpty = _wayBack.Count() == 0;


                if (isStackEmpty) return getNewDirection();

                while (!isStackEmpty && working && !ModulesManager.targeting.attacking)
                {
                    
                    waypoint = _wayBack.Pop();

                    if (!goToCoordinates(waypoint)) //if it is impossible to get back
                    {
                        if (!getNewDirection())
                            return false;
                        break;
                    }
                    isStackEmpty = _wayBack.Count() == 0;
                }
            }
            return true;
        }

        /// <summary>
        /// trying to go to the coordinates, returns true if waypoint has been reached.
        /// </summary>
        /// <param name="waypoint"></param>
        /// <returns></returns>
        private bool goToCoordinates(Waypoint waypoint)
        {
            int attempt = 0;
            while (!hasWaypointBeenReached(waypoint) && GetData.isOnScreen(waypoint.xPos, waypoint.yPos, waypoint.floor) && working)
            {
                attempt++;
                while (ModulesManager.targeting.attacking)
                    Thread.Sleep(500); // waits for targetting

                _direction = 0;
                if (waypoint.xPos > GetData.MyXPosition)
                    _direction += 1;
                else if (waypoint.xPos < GetData.MyXPosition)
                    _direction -= 1;
                if (waypoint.yPos > GetData.MyYPosition)
                    _direction += 2;
                else if (waypoint.yPos < GetData.MyYPosition)
                    _direction += 8;
                else _direction += 5;
                if (attempt == attemptsToRandomDirection)
                {
                    _direction = _rand.Next(1, 9);
                    attempt = 0;
                }

                go(waypoint);
                Thread.Sleep(200);
            }
            return hasWaypointBeenReached(waypoint);
        }

        /// <summary>
        /// return true if character is standing far enough to waypoint
        /// </summary>
        /// <param name="waypoint"></param>
        /// <returns></returns>
        private bool hasWaypointBeenReached(Waypoint waypoint)
        {
            return Math.Abs(GetData.MyXPosition - waypoint.xPos) + Math.Abs(GetData.MyYPosition - waypoint.yPos) <= tolerance;
        }

        /// <summary>
        /// simple attempt to go to the waypoint. It is not certain to get there!
        /// </summary>
        /// <param name="waypoint"></param>
        public void go(Waypoint waypoint, int? dir = null)
        {
            if (dir != null)
                _direction = (int)dir;

            if (_direction == 8)
                KeyboardSimulator.Press("up");
            else if (_direction == 6)
                KeyboardSimulator.Press("right");
            else if (_direction == 4)
                KeyboardSimulator.Press("left");
            else if (_direction == 2)
                KeyboardSimulator.Press("down");
            else
                MouseSimulator.clickOnField(waypoint.xPos, waypoint.yPos);

            // KeyboardSimulator.Press("NUM" + direction);

            //MouseSimulator.clickOnField(waypoint.xPos, waypoint.yPos);
        }

        private bool getNewDirection()
        {
            int distance = 99, temp;
            Waypoint waypoint;
            int nextStatementID = actualStatementIndex;

            for (int i = actualStatementIndex + 1; i != actualStatementIndex; i++) //checking waypoints and gets the closest one
            {
                lock (list)
                {
                    if (i >= list.Count())
                    {
                        i = 0;
                        if (i == actualStatementIndex)
                            break;
                    }

                    if (list[i].type == StatementType.getType["Waypoint"]) // if statement is waypoint
                    {
                        waypoint = (Waypoint)list[i];
                        if (GetData.isOnScreen(waypoint.xPos, waypoint.yPos, waypoint.floor))
                        {
                            if (distance > (temp =GetData.GetDistance(waypoint.xPos, waypoint.yPos) + (int)Math.Abs(i-actualStatementIndex)/10) )//setting the new result
                            {
                                distance = temp;
                                nextStatementID = i - 1;
                            }
                        }
                    }
                    else if (list[i].type == StatementType.getType["Action"])// if statement is action
                    {
                        //throw new NotImplementedException();
                        //when all conditions are good then go to label
                    }
                }
            }
            actualStatementIndex = nextStatementID;
            return distance < 99;
        }

        public List<WalkerStatement> CopyList()
        {
            return Storage.Storage.Copy(Way.changeWaypointListToWay(list)) as List<WalkerStatement>;

        }

        public void SetList(List<WalkerStatement> newList)
        {
            lock (list)
            {
                list = newList;
                actualStatementIndex = 0;
            }
        }

        private void startTracking()
        {
            trackerWorking = true;
            Thread t = new Thread(tracker);
            t.Start();
        }

        private void tracker()
        {
            int lastX=GetData.MyXPosition, lastY=GetData.MyYPosition, LastFloor=GetData.MyFloor;
            int actualX, actualY, actualFloor;
            Waypoint waypoint;
            while (working)
            {
                if (trackerWorking)
                {
                    actualX = GetData.MyXPosition;
                    actualY = GetData.MyYPosition;
                    actualFloor = GetData.MyFloor;
                    //if (actualX != lastX || actualY != lastY || actualFloor != LastFloor)
                    {
                        for(int i=actualStatementIndex; i<10; i++)
                        {
                            lock (list)
                            {
                                if (list[i].type == StatementType.getType["Waypoint"])//checking if waypoint has been reached
                                {
                                    waypoint = (Waypoint)list[i];
                                    if (actualX == waypoint.xPos && actualY == waypoint.yPos && actualFloor == waypoint.floor)//if wayppoint has been reached then reset wayBack
                                    {
                                        actualStatementIndex = i;
                                        lock (_wayBack)
                                        {
                                            _wayBack = new Stack<Waypoint>();
                                            continue;
                                        }
                                    }
                                }
                            }
                            
                        }
                        

                        lastX = actualX;
                        lastY = actualY;
                        LastFloor = actualFloor;
                        lock (_wayBack)
                        {
                            _wayBack.Push(new Waypoint(actualX, actualY, actualFloor, true));
                        }

                    }

                }
                Thread.Sleep(200);

            }


        }

    }
}
