using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayState
{
    Title, Lobby, InPlay, GameOver, Clear
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayState playState = PlayState.Title;
    //Gameplay Info
    //1. Time
    public string curTime { get { return string.Format("{0:D2}:{1:D2}", minute, second); } }
    static int minute = 0;
    static int second = 0;
    float tick;
    //2. Score
    [HideInInspector] public static int score;
    //3. DeathByWhat
    [HideInInspector] public static string deathBy;
    //4. Eliminated Enemy
    [HideInInspector] public static int e_Slime;
    [HideInInspector] public static int e_Zombie;
    [HideInInspector] public static int e_Skeleton;
    [HideInInspector] public static int e_Mimic;
    //5. Items
    [HideInInspector] public static List<Sprite> itemIcons = new();
    [HideInInspector] public static List<string> items = new();
    [HideInInspector] public static List<int> counts = new();


    GameObject player;
    Player pLogic;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (playState == PlayState.InPlay)
        {
            ShowTimeUI();
            RoomClear();
        }
        if(playState == PlayState.Lobby) RoomClear();
    }

    public void ShowTimeUI()
    {
        tick += Time.deltaTime;
        if(tick >= 1)
        {
            second++;
            tick = 0;
        }
        if(second >= 60)
        {
            minute++;
            second = 0;
        }
        UIManager.instance.time = curTime;
    }

    public static void Init()
    {
        minute = 0;
        second = 0;
        instance.tick = 0;
        score = 0;
        deathBy = "";
        e_Slime = 0;
        e_Zombie = 0;
        e_Skeleton = 0;
        e_Mimic = 0;
        itemIcons.Clear();
        items.Clear();
        counts.Clear();
    }

    public void RoomClear()
    {
        if (playState != PlayState.InPlay && playState != PlayState.Lobby) return;
        if (player == null) player = GameObject.FindWithTag("Player");
        if (player != null && pLogic == null) pLogic = player.GetComponent<Player>();
        if (pLogic == null) return;
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        Room roomInfo = pLogic.curRoom.GetComponent<Room>();
        if (enemies.Count > 0) roomInfo.isClear = false;
        else roomInfo.isClear = true;
        roomInfo.SwitchDoors();
    }

    public static void GetItem(GameObject item)
    {
        string name = item.name.Replace("(Clone)", "");
        if(items.Contains(name))
        {
            int idx = items.IndexOf(name);
            counts[idx]++;
        }
        else
        {
            items.Add(name);
            Sprite sprite = item.GetComponent<SpriteRenderer>().sprite;
            itemIcons.Add(sprite);
            counts.Add(1);
        }
    }

    public GameManager()
    {
        instance = this;
    }
}
