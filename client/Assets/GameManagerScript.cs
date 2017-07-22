using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    [Header("Player")]
    public GameObject Player;
    public Dictionary<int, GameObject> Players = new Dictionary<int, GameObject>();


    [Header("Player")]
    public GameObject Enemy;


    [Header("Ball")]
    public GameObject Ball;

    [Header("Item")]
    public GameObject Item2;
    public GameObject Item3;
    public GameObject Item4;
    public GameObject Item5;
    public GameObject Item6;
    public GameObject Item7;
    public GameObject Item8;
    public GameObject Item9;
    public GameObject Item10;

    float GameTime;
    public static bool firstturn;


    // Use this for initialization
    void Start () {
        firstturn = false;
        GameTime = 90.0f;
    }

    void Update()
    {
        GameTime -= Time.deltaTime;

        if (GameTime < 0.0f)
        {
            GameTime = 90.0f;
        }
    }

    public GameObject GetItemPrefab(int itemType)
    {
        switch (itemType)
        {
            case 2: return Item2;
            case 3: return Item3;
            case 4: return Item4;
            case 5: return Item5;
            case 6: return Item6;
            case 7: return Item7;
            case 8: return Item8;
            case 9: return Item9;
            case 10: return Item10;
            default:
                Debug.LogError("Item not found: " + itemType);
                return Item2;
        }
    }
}
