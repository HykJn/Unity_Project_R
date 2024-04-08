using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public Node(bool isWall, int x, int y)
    {
        this.isWall = isWall;
        this.x = x;
        this.y = y;
    }

    public bool isWall;
    public int x, y, G, H;
    public int F
    {
        get
        {
            return G + H;
        }
    }
    public Node parent;
}

public class AstarPathFinding : MonoBehaviour
{
    public List<Node> openList, closedList, pathNodes;

    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    int w, h;
    Node[,] Grids;
    Node startNode, targetNode, curNode;
    
    public void DrawGrid()
    {
        w = topRight.x - bottomLeft.x + 1;
        h = topRight.y - bottomLeft.y + 1;

        Grids = new Node[w, h];
    }

    public void CheckWall()
    {
        for(int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(bottomLeft.x + i, bottomLeft.y + j), Vector3.forward, 1f, LayerMask.GetMask("Wall"));
                if(!hit)
                {
                    Grids[i, j] = new Node(false, bottomLeft.x + i, bottomLeft.y + j);
                }
                else
                {
                    Grids[i, j] = new Node(true, bottomLeft.x + i, bottomLeft.y + j);
                }
                
            }
        }
    }

    public void PathFinding()
    {
        startNode = Grids[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        targetNode = Grids[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];
        openList = new List<Node>() { startNode };
        closedList= new List<Node>();
        pathNodes = new List<Node>();

        while(openList.Count > 0)
        {
            curNode = openList[0];
            foreach (Node n in openList)
            {
                if (n.F < curNode.F) curNode = n;
                else if (n.F == curNode.F && n.H < curNode.H) curNode = n;
            }

            openList.Remove(curNode);
            closedList.Add(curNode);

            if (curNode == targetNode)
            {
                Node tempNode = curNode;
                while(tempNode != startNode)
                {
                    pathNodes.Add(tempNode);
                    tempNode = tempNode.parent;
                }
                pathNodes.Add(startNode);
                break;
            }
        }
    }

    private void Start()
    {
        DrawGrid();
        CheckWall();
    }
}
