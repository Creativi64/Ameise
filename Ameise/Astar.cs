﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace AStarSharp
{
    public class Node
    {
        // Change this depending on what the desired size is for each element in the grid
        public static readonly int NODE_SIZE = 1;

        public Node Parent;

        public Vector2 Position;

        public Vector2 Center
        {
            get
            {
                return new Vector2(Position.X + NODE_SIZE / 2, Position.Y + NODE_SIZE / 2);
            }
        }

        public float DistanceToTarget;

        public float Cost;

        public float Weight;

        public float F
        {
            get
            {
                if (DistanceToTarget != -1 && Cost != -1)
                    return DistanceToTarget + Cost;
                else
                    return -1;
            }
        }

        public bool Walkable;

        public Node(Vector2 pos, bool walkable, float weight = 1)
        {
            Parent = null;
            Position = pos;
            DistanceToTarget = -1;
            Cost = 1;
            Weight = weight;
            Walkable = walkable;
        }
    }

    public class Astar
    {
        private readonly List<List<Node>> Grid;

        private int GridRows
        {
            get
            {
                return Grid[0].Count;
            }
        }

        private int GridCols
        {
            get
            {
                return Grid.Count;
            }
        }

        public Astar(List<List<Node>> grid)
        {
            Grid = grid;
        }

        public Stack<Node> FindPath(Vector2 Start, Vector2 End)
        {
            Node start = new(new Vector2((int)(Start.X / Node.NODE_SIZE), (int)(Start.Y / Node.NODE_SIZE)), true);
            Node end = new(new Vector2((int)(End.X / Node.NODE_SIZE), (int)(End.Y / Node.NODE_SIZE)), true);

            Stack<Node> Path = new();
            List<Node> OpenList = new();
            List<Node> ClosedList = new();
            List<Node> adjacencies;
            Node current = start;

            // add start node to Open List
            OpenList.Add(start);

            while (OpenList.Count != 0 && !ClosedList.Exists(x => x.Position == end.Position))
            {
                current = OpenList[0];
                OpenList.Remove(current);
                ClosedList.Add(current);
                adjacencies = GetAdjacentNodes(current);

                foreach (Node n in adjacencies)
                {
                    if (!ClosedList.Contains(n) && n.Walkable)
                    {
                        if (!OpenList.Contains(n))
                        {
                            n.Parent = current;
                            n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                            n.Cost = n.Weight + n.Parent.Cost;
                            OpenList.Add(n);
                            OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (!ClosedList.Exists(x => x.Position == end.Position))
            {
                return null;
            }

            // if all good, return path
            Node temp = ClosedList[ClosedList.IndexOf(current)];
            if (temp == null) return null;

            do
            {
                Path.Push(temp);
                temp = temp.Parent;
            } while (temp != start && temp != null);

            return Path;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> temp = new();

            int row = (int)n.Position.Y;
            int col = (int)n.Position.X;

            if (row + 1 < GridRows)
            {
                temp.Add(Grid[col][row + 1]);
            }
            if (row - 1 >= 0)
            {
                temp.Add(Grid[col][row - 1]);
            }
            if (col - 1 >= 0)
            {
                temp.Add(Grid[col - 1][row]);
            }
            if (col + 1 < GridCols)
            {
                temp.Add(Grid[col + 1][row]);
            }

            return temp;
        }
    }
}