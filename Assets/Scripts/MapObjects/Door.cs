using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    bool flag = false;
    public DoorPos pos;
    public Sprite open;
    public Sprite close;
    SpriteRenderer render;
    Collider2D col;
    Camera cam;
    GameObject minimapCam;
    GameObject player;

    private void Awake()
    {
        render = this.GetComponent<SpriteRenderer>();
        col = this.GetComponent<Collider2D>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        minimapCam = GameObject.Find("MinimapCam");
    }

    private void Update()
    {
        if (flag && Input.GetKeyDown(KeyCode.E))
        {
            UseDoor(player);
        }
    }

    public void DoorOpen()
    {
        isOpen = true;
        render.sprite = open;
        col.isTrigger = true;
    }

    public void DoorClose()
    {
        isOpen = false;
        render.sprite = close;
        col.isTrigger = false;
    }

    void UseDoor(GameObject player)
    {
        Player pLogic = player.GetComponent<Player>();
        Room rInfo = pLogic.curRoom.GetComponent<Room>();
        int idx = rInfo.conDoors.IndexOf(pos);
        Transform nextRoom = rInfo.conRooms[idx].transform;
        if (isOpen)
        {
            switch (pos)
            {
                case DoorPos.Up:
                    player.transform.position += Vector3.up * 6;
                    break;
                case DoorPos.Down:
                    player.transform.position += Vector3.down * 6;
                    break;
                case DoorPos.Left:
                    player.transform.position += Vector3.left * 6;
                    break;
                case DoorPos.Right:
                    player.transform.position += Vector3.right * 6;
                    break;
            }
            nextRoom.gameObject.SetActive(true);
            player.transform.parent = nextRoom;
            pLogic.minimapMask.transform.position = pLogic.curRoom.position;
            cam.transform.position = nextRoom.position + new Vector3(0.5f, 1.5f, -1);
            minimapCam.transform.position = new Vector3(nextRoom.position.x, nextRoom.position.y, minimapCam.transform.position.z);
            if (GameManager.instance.playState != PlayState.InPlay) SceneChanger.ToPlay();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        player = collision.gameObject;
        flag = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flag = false;
    }
}
