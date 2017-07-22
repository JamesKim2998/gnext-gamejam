using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientItemSpawner : MonoBehaviour
{
    private GameManagerScript _gameManager;
    private readonly Dictionary<int, GameObject> _items = new Dictionary<int, GameObject>();

    void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
    }

    private void OnEnable()
    {
        WSClientBroadcast.SpawnItem += OnSpawnItem;
        WSClientBroadcast.DestroyItem += OnDestroyItem;
    }

    private void OnDisable()
    {
        WSClientBroadcast.SpawnItem -= OnSpawnItem;
        WSClientBroadcast.DestroyItem -= OnDestroyItem;
    }

    private void OnSpawnItem(SerDeSpawnItem serDe)
    {
        var prefab = _gameManager.GetItemPrefab(serDe.ItemType);
        var item = GameObject.Instantiate(prefab, serDe.Position, Quaternion.identity);
        item.GetComponent<ItemScript>().NetworkId = serDe.NetworkId;
        item.GetComponent<CircleCollider2D>().enabled = false;
        _items[serDe.NetworkId] = item;
    }

    private void OnDestroyItem(SerDeDestroyItem serDe)
    {
        if (!_items.ContainsKey(serDe.NetworkId)) return;
        var item = _items[serDe.NetworkId];
        if (item != null) Destroy(item);
        _items.Remove(serDe.NetworkId);
    }
}