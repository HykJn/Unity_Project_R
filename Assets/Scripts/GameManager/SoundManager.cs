using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header("- Audio Setting")]
    [Range(0, 1)]
    public float mainVolume = 1;
    [Range(0, 1)]
    public float bgmVolume = 1;
    [Range(0, 1)]
    public float sfxVolume = 1;

    float MainVolume
    {
        set
        {
            BGMChannel.volume = bgmVolume * value;
            SFXChannel.volume = sfxVolume * value;
        }
    }

    public AudioSource BGMChannel;
    public AudioSource SFXChannel;


    [Header("- BGM")]
    public AudioClip titleBGM;
    public AudioClip inGameBGM;
    public AudioClip lobbyBGM;
    [Header("- System")]
    public AudioClip clickSFX;
    [Header("- Player")]
    public AudioClip playerHitSFX;
    public AudioClip getItemSFX;
    [Header("- Mage")]
    public AudioClip mageAttackSFX;
    public AudioClip mageAttackNormalHitSFX;
    public AudioClip mageAttackCriticalHitSFX;
    public AudioClip mageTeleportSFX;
    public AudioClip mageBlessSFX;
    [Header("- Enemy")]
    public AudioClip slimeSpawnSFX;
    public AudioClip slimeAttackSFX;
    public AudioClip slimeDeathSFX;
    public AudioClip zombieAttackSFX;
    public AudioClip zombieDeathSFX;
    public AudioClip skeletonSpawnSFX;
    public AudioClip skeletonAttackSFX;
    public AudioClip skeletonDeathSFX;
    public AudioClip mimicAwakeSFX;
    public AudioClip mimicDeathSFX;
    public AudioClip skeletonMageAttackSFX;
    public AudioClip skeletonMageSummonSFX;
    public AudioClip skeletonMageDeathSFX;
    public AudioClip randomSpawnerSFX;
    [Header("- Environmnet")]
    public AudioClip woodBreakSFX;
    public AudioClip jarBreakSFX;
    public AudioClip slimeSpawnerBreakSFX;
    public AudioClip explosionSFX;
    public AudioClip thornTrapSFX;
    public AudioClip bearTrapSFX;
    public AudioClip arrowTrapSFX;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(string name)
    {
        switch (name)
        {
            case "titleBGM":
                BGMChannel.PlayOneShot(titleBGM);
                break;
            case "inGameBGM":
                BGMChannel.PlayOneShot(inGameBGM);
                break;
            case "lobbyBGM":
                BGMChannel.PlayOneShot(lobbyBGM);
                break;
            case "click":
                SFXChannel.PlayOneShot(clickSFX);
                break;
            case "playerHit":
                SFXChannel.PlayOneShot(playerHitSFX);
                break;
            case "getItem":
                SFXChannel.PlayOneShot(getItemSFX);
                break;
            case "mageAttack":
                SFXChannel.PlayOneShot(mageAttackSFX);
                break;
            case "mageAttackNormalHit":
                SFXChannel.PlayOneShot(mageAttackNormalHitSFX);
                break;
            case "mageAttackCriticalHit":
                SFXChannel.PlayOneShot(mageAttackCriticalHitSFX);
                break;
            case "mageTeleport":
                SFXChannel.PlayOneShot(mageTeleportSFX);
                break;
            case "mageBless":
                SFXChannel.PlayOneShot(mageBlessSFX);
                break;
            case "slimeSpawn":
                SFXChannel.PlayOneShot(slimeSpawnSFX);
                break;
            case "slimeAttack":
                SFXChannel.PlayOneShot(slimeAttackSFX);
                break;
            case "slimeDeath":
                SFXChannel.PlayOneShot(slimeDeathSFX);
                break;
            case "zombieAttack":
                SFXChannel.PlayOneShot(zombieAttackSFX);
                break;
            case "zombieDeath":
                SFXChannel.PlayOneShot(zombieDeathSFX);
                break;
            case "skeletonSpawn":
                SFXChannel.PlayOneShot(skeletonSpawnSFX);
                break;
            case "skeletonAttack":
                SFXChannel.PlayOneShot(skeletonAttackSFX);
                break;
            case "skeletonDeath":
                SFXChannel.PlayOneShot(skeletonDeathSFX);
                break;
            case "mimicAwake":
                SFXChannel.PlayOneShot(mimicAwakeSFX);
                break;
            case "mimicDeath":
                SFXChannel.PlayOneShot(mimicDeathSFX);
                break;
            case "skeletonMageAttack":
                SFXChannel.PlayOneShot(skeletonMageAttackSFX);
                break;
            case "skeletonMageSummon":
                SFXChannel.PlayOneShot(skeletonMageSummonSFX);
                break;
            case "skeletonMageDeath":
                SFXChannel.PlayOneShot(skeletonMageDeathSFX);
                break;
            case "randomSpawner":
                SFXChannel.PlayOneShot(randomSpawnerSFX);
                break;
            case "woodBreak":
                SFXChannel.PlayOneShot(woodBreakSFX);
                break;
            case "jarBreak":
                SFXChannel.PlayOneShot(jarBreakSFX);
                break;
            case "slimeSpawnerBreak":
                SFXChannel.PlayOneShot(slimeSpawnerBreakSFX);
                break;
            case "explosion":
                SFXChannel.PlayOneShot(explosionSFX);
                break;
            case "thornTrap":
                SFXChannel.PlayOneShot(thornTrapSFX);
                break;
            case "bearTrap":
                SFXChannel.PlayOneShot(bearTrapSFX);
                break;
            case "arrowTrap":
                SFXChannel.PlayOneShot(arrowTrapSFX);
                break;
        }
    }

    private void Update()
    {
        MainVolume = mainVolume;
    }

    public SoundManager()
    {
        instance = this;
    }
}
