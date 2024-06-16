using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public GameObject targetObj;
    public List<Node> openList, closedList, pathList;
    public Vector2Int startPos, targetPos;
    NodeMap mapInfo;
    Node[,] nodeMap;
    Node startNode, targetNode, curNode;

    private void OnEnable()
    {
        if (this.transform.parent != null)
        {
            //mapInfo = this.transform.parent.GetComponent<NodeMap>();
        }
        targetObj = GameObject.FindWithTag("Player");
    }

    public void PathFinding()
    {
        mapInfo = this.transform.parent.GetComponent<NodeMap>();
        if (this.transform.parent != targetObj.transform.parent) return;
        startPos = Vector2Int.RoundToInt(this.transform.position);
        targetPos = Vector2Int.RoundToInt(targetObj.transform.position);
        nodeMap = mapInfo.nodeMap;
        startNode = nodeMap[startPos.x - mapInfo.bottomLeft.x, startPos.y - mapInfo.bottomLeft.y];
        targetNode = nodeMap[targetPos.x - mapInfo.bottomLeft.x, targetPos.y - mapInfo.bottomLeft.y];
        openList = new List<Node> { startNode };
        closedList = new List<Node>();
        pathList = new List<Node>();

        while (openList.Count > 0)
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
                Node tempNode = targetNode;
                while (tempNode != startNode)
                {
                    pathList.Add(tempNode);
                    tempNode = tempNode.parent;
                }
                //pathNodes.Add(startNode);
                pathList.Reverse();
                break;
            }

            CheckCondition(curNode.x, curNode.y + 1);       //Up
            CheckCondition(curNode.x + 1, curNode.y + 1);   //UpRight
            CheckCondition(curNode.x + 1, curNode.y);       //Right
            CheckCondition(curNode.x + 1, curNode.y - 1);   //RightDown
            CheckCondition(curNode.x, curNode.y - 1);       //Down
            CheckCondition(curNode.x - 1, curNode.y - 1);   //DownLeft
            CheckCondition(curNode.x - 1, curNode.y);       //Left
            CheckCondition(curNode.x - 1, curNode.y + 1);   //LeftUp
        }
    }

    public void CheckCondition(int childX, int childY)
    {
        bool inMap = (mapInfo.bottomLeft.x <= childX && childX <= mapInfo.topRight.x) && (mapInfo.bottomLeft.y <= childY && childY <= mapInfo.topRight.y);
        if (inMap)
        {
            Node childNode = nodeMap[childX - mapInfo.bottomLeft.x, childY - mapInfo.bottomLeft.y];
            bool isWall = childNode.isWall;
            bool inClosedList = closedList.Contains(childNode);
            bool canMoveDiagonal = !(nodeMap[childX - mapInfo.bottomLeft.x, curNode.y - mapInfo.bottomLeft.y].isWall ||
                nodeMap[curNode.x - mapInfo.bottomLeft.x, childY - mapInfo.bottomLeft.y].isWall);

            if (!isWall && !inClosedList && canMoveDiagonal)
            {
                int arcCost = curNode.G + (childX == curNode.x || childY == curNode.y ? 10 : 14);
                if (arcCost < childNode.G || !openList.Contains(childNode))
                {
                    childNode.G = arcCost;
                    childNode.H = (Mathf.Abs(targetNode.x - childNode.x) + Mathf.Abs(targetNode.y - childNode.y)) * 10;
                    childNode.parent = curNode;
                    openList.Add(childNode);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (pathList.Count > 0)
        {
            for (int i = 0; i < pathList.Count - 1; i++)
            {
                Vector2 from = new Vector2(pathList[i].x, pathList[i].y);
                Vector2 to = new Vector2(pathList[i + 1].x, pathList[i + 1].y);
                Gizmos.DrawLine(from, to);
            }
        }
    }
}
