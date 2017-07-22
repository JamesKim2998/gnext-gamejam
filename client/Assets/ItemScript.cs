using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("SetFalse", 20.0f);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.transform.tag == "item6")
            this.transform.Rotate(new Vector3(0, 0, 15.0f));
        if (this.gameObject.transform.tag == "item7")
            this.transform.Rotate(new Vector3(0, 0, -15.0f));

    }

    void SetFalse()
    {
        this.gameObject.SetActive(false);
    }
}
