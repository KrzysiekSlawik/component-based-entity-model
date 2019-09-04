using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class Node
    {
        public Node(Vector3Int pos)
        {
            this.pos = pos;
        }
        public Node(int x, int y)
        {
            this.pos.x = x;
            this.pos.y = y;
        }
        public Vector3Int pos;
        public Node ancestor;
    }
    [Serializable]
    public class Graph
    {
        private bool[,] walkable;
        private int xSize;
        private int ySize;
        
        public Graph(bool[,] canWalk, int x, int y)
        {
            walkable = canWalk;
            xSize = x;
            ySize = y;
            Debug.Log(xSize);
            Debug.Log(ySize);
        }
        //from a to b
        public Node BFS(Vector3Int a, Vector3Int b)
        {
            Debug.Log("BFS");
            bool[,] visited = new bool[xSize, ySize];
            Queue<Node> q = new Queue<Node>();
            visited[a.x, a.y] = true;
            q.Enqueue(new Node(a.x, a.y));
            while(q.Count > 0)
            {
                Node v = q.Dequeue();
                if(v.pos.Equals(b))
                {
                    return v;
                }
                else
                {
                    Stack<Node> adjNodes = AdjecentEdges(v);
                    while (adjNodes.Count > 0)
                    {
                        Node n = adjNodes.Pop();
                        if(!visited[n.pos.x, n.pos.y])
                        {
                            visited[n.pos.x, n.pos.y] = true;
                            n.ancestor = v;
                            q.Enqueue(n);
                        }
                    }
                }
            }
            return new Node(a.x, a.y);
        }
        private Stack<Node> AdjecentEdges(Node v)
        {
            Stack<Node> edges = new Stack<Node>();
            for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    if(!(x==0 && y==0) && xSize > v.pos.x && v.pos.x + x > 0 && ySize > v.pos.y && v.pos.y + y > 0)
                    {
                        if (walkable[v.pos.x + x, v.pos.y + y])
                        {
                            if(x != 0 && y != 0)
                            {
                                if(walkable[v.pos.x + x, v.pos.y] && walkable[v.pos.x, v.pos.y + y])
                                {
                                    edges.Push(new Node(v.pos.x + x, v.pos.y + y));
                                }
                            }
                            else
                            {
                                edges.Push(new Node(v.pos.x + x, v.pos.y + y));
                            }
                        }
                    }
                }
            }
            return edges;
        }

    }
}
