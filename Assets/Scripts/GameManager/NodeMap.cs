using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Node(int x, int y, bool isWall)
    {
        this.x = x;
        this.y = y;
        this.isWall = isWall;
    }

    public bool isWall;
    public int x, y;

    public Node parent;
    public int G, H;
    public int F
    {
        get
        {
            return G + H;
        }
    }
}

public class NodeMap : MonoBehaviour
{
    public Node[,] nodeMap;
    public int w, h;
    public Vector2Int bottomLeft, topRight;

    private void OnEnable()
    {
        bottomLeft = Vector2Int.RoundToInt(Vector2Int.CeilToInt(this.transform.position) + Vector2.left * (w / 2) + Vector2.down * (h / 2));
        topRight = Vector2Int.RoundToInt(Vector2Int.CeilToInt(this.transform.position) + Vector2.right * (w / 2) + Vector2.up * (h / 2));
        GetNodeMap();
    }

    public void GetNodeMap()
    {
        nodeMap = new Node[w, h];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Vector2 origin = new Vector2(bottomLeft.x + x, bottomLeft.y + y);
                bool hit = Physics2D.Raycast(origin, Vector3.forward, 1f, LayerMask.GetMask("Wall"));
                if(hit)
                {
                    nodeMap[x, y] = new Node((int)origin.x, (int)origin.y, true);
                }
                else
                {
                    nodeMap[x, y] = new Node((int)origin.x, (int)origin.y, false);
                }
            }
        }
    }
}
