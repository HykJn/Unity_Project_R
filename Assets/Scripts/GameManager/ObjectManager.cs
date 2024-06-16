using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;
    //Prefabs
    [Header("- Player")]
    public GameObject mageNormalAttackPrefab;
    public GameObject mageCritAttackPrefab;
    [Header("- Enemy")]
    public GameObject slimePrefab;
    public GameObject zombiePrefab;
    public GameObject zomSlashPrefab;
    public GameObject skeletonPrefab;
    public GameObject skelBonePrefab;
    public GameObject mimicPrefab;
    public GameObject skelMageAttackPrefab;
    public GameObject skelMagePotionPrefab;
    //public GameObject meteorPrefab;
    public GameObject boneShotPrefab;
    [Header("- Items")]
    public GameObject emptyHPPrefab;
    public GameObject halfHPPrefab;
    public GameObject fullHPPrefab;
    public GameObject glovePrefab;
    public GameObject powerPotionPrefab;
    public GameObject shieldPrefab;
    public GameObject shoesPrefab;
    public GameObject skullPrefab;
    public GameObject teleportBookPrefab;
    public GameObject blessBookPrefab;
    public GameObject watchPrefab;

    //Objects
    GameObject[] mageNormalAttack;
    GameObject[] mageCritAttack;

    GameObject[] slime;
    GameObject[] zombie;
    GameObject[] zomSlash;
    GameObject[] skeleton;
    GameObject[] skelBone;
    GameObject[] mimic;
    GameObject[] skelMageAttack;
    GameObject[] skelMagePotion;
    //GameObject[] meteor;
    GameObject[] boneShot;

    GameObject[] emptyHP;
    GameObject[] halfHP;
    GameObject[] fullHP;
    GameObject[] glove;
    GameObject[] powerPotion;
    GameObject[] shield;
    GameObject[] shoes;
    GameObject[] skull;
    GameObject[] teleportBook;
    GameObject[] blessBook;
    GameObject[] watch;

    GameObject[] pool;

    private void Awake()
    {
        mageNormalAttack = new GameObject[50];
        mageCritAttack = new GameObject[50];

        slime = new GameObject[50];
        zombie = new GameObject[50];
        zomSlash = new GameObject[50];
        skeleton = new GameObject[50];
        skelBone = new GameObject[100];
        mimic = new GameObject[50];
        skelMageAttack = new GameObject[50];
        skelMagePotion = new GameObject[10];
        //meteor = new GameObject[10];
        boneShot = new GameObject[50];

        emptyHP = new GameObject[30];
        halfHP = new GameObject[30];
        fullHP = new GameObject[30];
        glove = new GameObject[30];
        powerPotion = new GameObject[30];
        shield = new GameObject[30];
        shoes = new GameObject[30];
        skull = new GameObject[30];
        teleportBook = new GameObject[30];
        blessBook = new GameObject[30];
        watch = new GameObject[30];
        Init();
    }

    private void Start()
    {
        //Init();
    }

    void Init()
    {
        for (int i = 0; i < mageNormalAttack.Length; i++)
        {
            mageNormalAttack[i] = Instantiate(mageNormalAttackPrefab);
            mageNormalAttack[i].SetActive(false);
        }
        for (int i = 0; i < mageCritAttack.Length; i++)
        {
            mageCritAttack[i] = Instantiate(mageCritAttackPrefab);
            mageCritAttack[i].SetActive(false);
        }

        for (int i = 0; i < slime.Length; i++)
        {
            slime[i] = Instantiate(slimePrefab);
            slime[i].SetActive(false);
        }
        for (int i = 0; i < zombie.Length; i++)
        {
            zombie[i] = Instantiate(zombiePrefab);
            zombie[i].SetActive(false);        }
        for (int i = 0; i < zomSlash.Length; i++)
        {
            zomSlash[i] = Instantiate(zomSlashPrefab);
            zomSlash[i].SetActive(false);
        }
        for (int i = 0; i < skeleton.Length; i++)
        {
            skeleton[i] = Instantiate(skeletonPrefab);
            skeleton[i].SetActive(false);
        }
        for (int i = 0; i < skelBone.Length; i++)
        {
            skelBone[i] = Instantiate(skelBonePrefab);
            skelBone[i].SetActive(false);
        }
        for (int i = 0; i < mimic.Length; i++)
        {
            mimic[i] = Instantiate(mimicPrefab);
            mimic[i].SetActive(false);
        }
        for (int i = 0; i < skelMageAttack.Length; i++)
        {
            skelMageAttack[i] = Instantiate(skelMageAttackPrefab);
            skelMageAttack[i].SetActive(false);
        }
        for (int i = 0; i < skelMagePotion.Length; i++)
        {
            skelMagePotion[i] = Instantiate(skelMagePotionPrefab);
            skelMagePotion[i].SetActive(false);
        }
        //for (int i = 0; i < meteor.Length; i++)
        //{
        //    meteor[i] = Instantiate(meteorPrefab);
        //    meteor[i].SetActive(false);
        //}
        for (int i = 0; i < boneShot.Length; i++)
        {
            boneShot[i] = Instantiate(boneShotPrefab);
            boneShot[i].SetActive(false);
        }

        for (int i = 0; i < emptyHP.Length; i++)
        {
            emptyHP[i] = Instantiate(emptyHPPrefab);
            emptyHP[i].SetActive(false);
        }
        for (int i = 0; i < halfHP.Length; i++)
        {
            halfHP[i] = Instantiate(halfHPPrefab);
            halfHP[i].SetActive(false);
        }
        for (int i = 0; i < fullHP.Length; i++)
        {
            fullHP[i] = Instantiate(fullHPPrefab);
            fullHP[i].SetActive(false);
        }
        for (int i = 0; i < glove.Length; i++)
        {
            glove[i] = Instantiate(glovePrefab);
            glove[i].SetActive(false);
        }
        for (int i = 0; i < powerPotion.Length; i++)
        {
            powerPotion[i] = Instantiate(powerPotionPrefab);
            powerPotion[i].SetActive(false);
        }
        for (int i = 0; i < shield.Length; i++)
        {
            shield[i] = Instantiate(shieldPrefab);
            shield[i].SetActive(false);
        }
        for (int i = 0; i < shoes.Length; i++)
        {
            shoes[i] = Instantiate(shoesPrefab);
            shoes[i].SetActive(false);
        }
        for (int i = 0; i < skull.Length; i++)
        {
            skull[i] = Instantiate(skullPrefab);
            skull[i].SetActive(false);
        }
        for (int i = 0; i < teleportBook.Length; i++)
        {
            teleportBook[i] = Instantiate(teleportBookPrefab);
            teleportBook[i].SetActive(false);
        }
        for (int i = 0; i < blessBook.Length; i++)
        {
            blessBook[i] = Instantiate(blessBookPrefab);
            blessBook[i].SetActive(false);        
        }
        for (int i = 0; i < watch.Length; i++)
        {
            watch[i] = Instantiate(watchPrefab);
            watch[i].SetActive(false);
        }
    }

    public GameObject Activate(string type)
    {
        switch(type)
        {
            case "mageNormalAttack":
                pool = mageNormalAttack;
                break;
            case "mageCritAttack":
                pool = mageCritAttack;
                break;
            case "slime":
                pool = slime;
                break;     
            case "zombie":
                pool = zombie;
                break;
            case "zomSlash":
                pool = zomSlash;
                break;
            case "skeleton":
                pool = skeleton;
                break;
            case "skelBone":
                pool = skelBone;
                break;
            case "mimic":
                pool = mimic;
                break;
            case "skelMageAttack":
                pool = skelMageAttack;
                break;
            case "skelMagePotion":
                pool = skelMagePotion;
                break;
            //case "meteor":
            //    pool = meteor;
            //    break;
            case "boneShot":
                pool = boneShot;
                break;
            case "emptyHP":
                pool = emptyHP;
                break;
            case "halfHP":
                pool = halfHP;
                break;
            case "fullHP":
                pool = fullHP;
                break;
            case "glove":
                pool = glove;
                break;
            case "powerPotion":
                pool = powerPotion;
                break;
            case "shield":
                pool = shield;
                break;
            case "shoes":
                pool = shoes;
                break;
            case "skull":
                pool = skull;
                break;
            case "teleportBook":
                pool = teleportBook;
                break;
            case "blessBook":
                pool = blessBook;
                break;
            case "watch":
                pool = watch;
                break;
        }

        for(int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }

    public ObjectManager()
    {
        instance = this;
    }
}
