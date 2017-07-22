using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    public float power;
    public GameObject HpBar;
    Canvas UICanvas;
    GameObject PlayerHp;
    Rigidbody2D body;
    public static float speed;
    public static float SpinSpeed;

    float PowerTime;
    float SpeedTime;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        UICanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        power = 4.0f;
        speed = 6.0f;
        SpinSpeed = -10.0f;
        PlayerHp = Instantiate(HpBar, new Vector3(540, 350, 0), Quaternion.identity, UICanvas.transform);
        PlayerHp.transform.SetParent(UICanvas.transform, false);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0, 0, SpinSpeed));
    }

    IEnumerator PowerTimer()
    {
        Debug.Log("powerco");
        power = 9.0f;
        SpinSpeed = -30.0f;
        yield return new WaitForSeconds(5.0f);
        power = 4.0f;
        SpinSpeed = -10.0f;
    }

    IEnumerator SpeedTimer()
    {
        Debug.Log("speedco");
        speed = 10.0f;
        yield return new WaitForSeconds(5.0f);
        speed = 6.0f;
    }

}
