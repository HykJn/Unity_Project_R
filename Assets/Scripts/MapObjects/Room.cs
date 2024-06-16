using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorPos
{
    Up = 1, Down = -1, Left = 2, Right = -2
}

public class Room : MonoBehaviour
{
    public List<DoorPos> activatableDoors;
    public List<DoorPos> conDoors;
    public List<GameObject> conRooms;
    public GameObject door_U;
    public GameObject door_D;
    public GameObject door_L;
    public GameObject door_R;
    public GameObject edge;

    public bool isClear = false;

    public void CreateDoors()
    {
        foreach(DoorPos door in conDoors)
        {
            switch (door)
            {
                case DoorPos.Up:
                    door_U.SetActive(true);
                    break;
                case DoorPos.Down:
                    door_D.SetActive(true);
                    break;
                case DoorPos.Left:
                    door_L.SetActive(true);
                    break;
                case DoorPos.Right:
                    door_R.SetActive(true);
                    break;
            }
        }
    }

    public void SwitchDoors()
    {
        if(isClear)
        {
            foreach (DoorPos door in conDoors)
            {
                Door dLogic;
                switch (door)
                {
                    case DoorPos.Up:
                        dLogic = door_U.GetComponent<Door>();
                        dLogic.DoorOpen();
                        break;
                    case DoorPos.Down:
                        dLogic = door_D.GetComponent<Door>();
                        dLogic.DoorOpen();
                        break;
                    case DoorPos.Left:
                        dLogic = door_L.GetComponent<Door>();
                        dLogic.DoorOpen();
                        break;
                    case DoorPos.Right:
                        dLogic = door_R.GetComponent<Door>();
                        dLogic.DoorOpen();
                        break;
                }
            }
        }
        else
        {
            foreach (DoorPos door in conDoors)
            {
                Door dLogic;
                switch (door)
                {
                    case DoorPos.Up:
                        dLogic = door_U.GetComponent<Door>();
                        dLogic.DoorClose();
                        break;
                    case DoorPos.Down:
                        dLogic = door_D.GetComponent<Door>();
                        dLogic.DoorClose();
                        break;
                    case DoorPos.Left:
                        dLogic = door_L.GetComponent<Door>();
                        dLogic.DoorClose();
                        break;
                    case DoorPos.Right:
                        dLogic = door_R.GetComponent<Door>();
                        dLogic.DoorClose();
                        break;
                }
            }
        }
    }

    public void DrawEdge()
    {
        if (conDoors.Count > 0)
        {
            foreach (DoorPos door in conDoors)
            {
                GameObject Edge = Instantiate(edge, this.transform.position, Quaternion.identity);
                switch (door)
                {
                    case DoorPos.Up:
                        Edge.transform.position += Vector3.up * 6.5f;
                        Edge.transform.localScale = new Vector2(0.5f, 2f);
                        break;
                    case DoorPos.Down:
                        Edge.transform.position += Vector3.down * 6.5f;
                        Edge.transform.localScale = new Vector2(0.5f, 2f);
                        break;
                    case DoorPos.Left:
                        Edge.transform.position += Vector3.left * 9.75f;
                        Edge.transform.localScale = new Vector2(7.5f, 0.5f);
                        //Instantiate(edge, (Vector2)this.transform.position + Vector2.left * 9.75f, Quaternion.identity);
                        break;
                    case DoorPos.Right:
                        Edge.transform.position += Vector3.right * 9.75f;
                        Edge.transform.localScale = new Vector2(7.5f, 0.5f);
                        //Instantiate(edge, (Vector2)this.transform.position + Vector2.right * 9.75f, Quaternion.identity);
                        break;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (conDoors.Count > 0)
        {
            foreach (DoorPos door in conDoors)
            {
                switch (door)
                {
                    case DoorPos.Up:
                        Debug.DrawRay(this.transform.position, Vector2.up * 7f, Color.red);
                        break;
                    case DoorPos.Down:
                        Debug.DrawRay(this.transform.position, Vector2.down * 7f, Color.red);
                        break;
                    case DoorPos.Left:
                        //Instantiate(edge, (Vector2)this.transform.position + Vector2.left * 9.75f, Quaternion.identity);
                        Debug.DrawRay(this.transform.position, Vector2.left * 13f, Color.red);
                        break;
                    case DoorPos.Right:
                        //Instantiate(edge, (Vector2)this.transform.position + Vector2.right * 9.75f, Quaternion.identity);
                        Debug.DrawRay(this.transform.position, Vector2.right * 13f, Color.red);
                        break;
                }
            }
        }
    }
}
