using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int spaceSize;
    int curSize = 0;

    public GameObject[] roomTypes;
    public GameObject startRoom;
    public GameObject bossRoom;
    public GameObject roomMinimapMask;
    public GameObject bossRoomMinimapMask;
    public List<GameObject> openList;
    public List<GameObject> closeList;
    public List<GameObject> GeneratedRooms;

    private void Start()
    {
        //Init();
        MapGenerate();
        foreach(GameObject room in GeneratedRooms)
        {
            room.SetActive(false);
        }
        startRoom.SetActive(true);
        CreateDoors();
    }

    public void Init()
    {
        openList.Clear();
        closeList.Clear();
        curSize = 0;
        Room startRoomInfo = startRoom.GetComponent<Room>();
        startRoomInfo.activatableDoors.Clear();
        startRoomInfo.activatableDoors.Add(DoorPos.Up);
        startRoomInfo.conDoors.Clear();
        startRoomInfo.conRooms.Clear();
        GameObject[] activatedRooms = GameObject.FindGameObjectsWithTag("Room");
        foreach(GameObject room in activatedRooms)
        {
            Destroy(room);
        }
    }
    public void MapGenerate()
    {
        openList = new List<GameObject> { startRoom };
        closeList = new List<GameObject>();
        GeneratedRooms = new List<GameObject>() { startRoom };
        List<Vector2> roomPos = new List<Vector2> { startRoom.transform.position };

        bool sizeFlag = true;
        while (sizeFlag)
        {
            if (openList.Count == 0)
            {
                openList.Add(closeList[closeList.Count - 1]);
                closeList.RemoveAt(closeList.Count - 1);
            }
            GameObject curRoom = openList[0];
            openList.RemoveAt(0);
            closeList.Add(curRoom);

            Room curRoomInfo = curRoom.GetComponent<Room>();
            if (curRoomInfo.activatableDoors.Count == 0) continue;

            int numOfActivateDoors = Random.Range(1, curRoomInfo.activatableDoors.Count + 1);
            int count = 0;
            List<DoorPos> randDoors = new List<DoorPos>();
            while (count < numOfActivateDoors)
            {
                int randIdx = Random.Range(0, curRoomInfo.activatableDoors.Count);
                DoorPos randDoor = curRoomInfo.activatableDoors[randIdx];
                if (curRoomInfo.conDoors.Contains(randDoor) || randDoors.Contains(randDoor)) continue;
                randDoors.Add(randDoor);
                count++;
            }

            foreach (DoorPos door in randDoors)
            {
                if (curSize >= spaceSize)
                {
                    sizeFlag = false;
                    break;
                }
                int randIdx = Random.Range(0, roomTypes.Length);
                Room tempForCheck = roomTypes[randIdx].GetComponent<Room>();
                DoorPos oppositeDoor = (DoorPos)(-(int)door);
                if (tempForCheck.activatableDoors.Contains(oppositeDoor))
                {
                    Vector2 genPos = new Vector2(0, 0);
                    switch (door)
                    {
                        case DoorPos.Up:
                            genPos = (Vector2)curRoom.transform.position + Vector2.up * 14f;
                            break;
                        case DoorPos.Down:
                            genPos = (Vector2)curRoom.transform.position + Vector2.down * 14f;
                            break;
                        case DoorPos.Left:
                            genPos = (Vector2)curRoom.transform.position + Vector2.left * 26f;
                            break;
                        case DoorPos.Right:
                            genPos = (Vector2)curRoom.transform.position + Vector2.right * 26f;
                            break;
                    }
                    if (!roomPos.Contains(genPos))
                    {
                        roomPos.Add(genPos);
                        GameObject neighbor = Instantiate(roomTypes[randIdx]);
                        Instantiate(roomMinimapMask, genPos, Quaternion.identity);
                        GeneratedRooms.Add(neighbor);
                        neighbor.transform.position = genPos;
                        curSize++;
                        Room neighborInfo = neighbor.GetComponent<Room>();

                        curRoomInfo.conDoors.Add(door);
                        curRoomInfo.activatableDoors.Remove(door);
                        curRoomInfo.conRooms.Add(neighbor);

                        neighborInfo.conDoors.Add(oppositeDoor);
                        neighborInfo.activatableDoors.Remove(oppositeDoor);
                        neighborInfo.conRooms.Add(curRoom);

                        openList.Add(neighbor);
                    }
                }
            }
        }

        for (int i = GeneratedRooms.Count - 1; i > 0; i--)
        {
            Room curRoomInfo = GeneratedRooms[i].GetComponent<Room>();
            Vector2 genPos = curRoomInfo.gameObject.transform.position;
            int curCount = GeneratedRooms.Count;
            foreach (DoorPos dp in curRoomInfo.activatableDoors)
            {
                switch (dp)
                {
                    case DoorPos.Left:
                        genPos += Vector2.left * 26f;
                        break;
                    case DoorPos.Right:
                        genPos += Vector2.right * 26f;
                        break;
                    case DoorPos.Up:
                        genPos += Vector2.up * 14f;
                        break;
                    case DoorPos.Down:
                        genPos += Vector2.down * 14f;
                        break;
                }
                if (roomPos.Contains(genPos)) continue;
                else
                {
                    GameObject bossRoomObj = Instantiate(bossRoom, genPos, Quaternion.identity);
                    Instantiate(bossRoomMinimapMask, genPos, Quaternion.identity);
                    curRoomInfo.activatableDoors.Remove(dp);
                    curRoomInfo.conDoors.Add(dp);
                    curRoomInfo.conRooms.Add(bossRoomObj);

                    Room bossRoomInfo = bossRoomObj.GetComponent<Room>();
                    bossRoomInfo.activatableDoors.Remove((DoorPos)(-(int)dp));
                    bossRoomInfo.conDoors.Add((DoorPos)(-(int)dp));
                    bossRoomInfo.conRooms.Add(curRoomInfo.gameObject);

                    GeneratedRooms.Add(bossRoomObj);
                    break;
                }
            }
            if (curCount != GeneratedRooms.Count) break;
        }
    }

    public void CreateDoors()
    {
        foreach(GameObject room in GeneratedRooms)
        {
            room.GetComponent<Room>().CreateDoors();
            room.GetComponent<Room>().DrawEdge();
        }
    }
}
