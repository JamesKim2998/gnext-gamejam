using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerItemSpawner : MonoBehaviour
{
    private static int _itemNetworkId;
    GameManagerScript _gameManager;
    float timer;
    float blueTimeLeft;

    void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
        blueTimeLeft = Random.Range(3f, 8f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 7.0f)
        {
            timer = 0.0f;
            SpawnRandomItem();
        }

        blueTimeLeft -= Time.deltaTime;
        if (blueTimeLeft <= 0)
        {
            blueTimeLeft = Random.Range(5f, 12f);
            SpawnRandomBlue();
        }
    }

    private static Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f));
    }

    private GameObject InstantiateItem(int itemType)
    {
        var prefab = _gameManager.GetItemPrefab(itemType);
        var position = RandomPosition();
        var ret = GameObject.Instantiate(prefab, position, Quaternion.identity);
        var item = ret.GetComponent<ItemScript>();
        item.NetworkId = _itemNetworkId;
        WSServer.SpawnItem(new SerDeSpawnItem
        {
            ItemType = itemType,
            Position = position,
            NetworkId = _itemNetworkId,
        });

        item.DestroyCallback += networkId =>
            WSServer.DestroyItem(networkId);

        ++_itemNetworkId;
        return ret;
    }

    private void SpawnRandomBlue()
    {
        InstantiateItem(2);
    }

    private void SpawnRandomItem()
    {
        var ItemNum = Random.Range(0, 7);

        switch (ItemNum)
        {
            case 0:
                InstantiateItem(3);
                break;
            case 1:
                InstantiateItem(4);
                break;
            case 2:
                InstantiateItem(5);
                break;
            case 3:
                InstantiateItem(6);
                InstantiateItem(7);
                break;
            case 4:
                InstantiateItem(8);
                break;
            case 5:
                InstantiateItem(9);
                break;
            case 6:
                InstantiateItem(10);
                break;
            default:
                Debug.LogError("case not handled");
                break;
        }
    }
}
