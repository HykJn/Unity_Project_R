using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public GameObject itemPrefab;
    [Range(0f, 1f)]
    public float dropChance;  // 0 to 1 (0% to 100%)
}

public class Chest : MonoBehaviour
{
    public Sprite openedChest;
    public List<Item> items;  // 인스펙터에서 설정할 아이템 리스트

    private bool isPlayerNearby = false;
    bool isOpen = false;
    private Transform playerTransform;
    SpriteRenderer render;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Collider2D sensor = this.transform.GetChild(0).GetComponent<Collider2D>();
        isPlayerNearby = sensor.IsTouchingLayers(LayerMask.GetMask("Player"));

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            OpenChest();
        }
    }

    void OpenChest()
    {
        Item selectedItem = GetRandomItem();
        if (!isOpen && selectedItem != null && playerTransform != null)
        {

            Vector3 dropPosition = playerTransform.position + (playerTransform.position - transform.position).normalized;
            Instantiate(selectedItem.itemPrefab, dropPosition, Quaternion.identity);

            Debug.Log("Obtained item: " + selectedItem.itemPrefab.name);

            // 상자 교체
            render.sprite = openedChest;
            isOpen = true;
        }
    }

    Item GetRandomItem()
    {
        float totalWeight = 0;
        foreach (var item in items)
        {
            totalWeight += item.dropChance;
        }

        float randomValue = Random.value * totalWeight;
        Debug.Log("Random Value: " + randomValue + " / Total Weight: " + totalWeight); 
        foreach (var item in items)
        {
            if (randomValue < item.dropChance)
            {
                return item;
            }
            randomValue -= item.dropChance;
        }

        return null;
    }
}
