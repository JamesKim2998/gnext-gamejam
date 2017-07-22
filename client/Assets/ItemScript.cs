using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public int NetworkId;
    public System.Action<int> DestroyCallback;

    void Start()
    {
        Destroy(gameObject, 20);
    }

    void OnDestroy()
    {
        if (DestroyCallback != null)
            DestroyCallback(NetworkId);
    }

    void Update()
    {
        if (gameObject.transform.tag == "item6")
            transform.Rotate(new Vector3(0, 0, -10.0f));
        if (gameObject.transform.tag == "item7")
            transform.Rotate(new Vector3(0, 0, 10.0f));
    }
}
