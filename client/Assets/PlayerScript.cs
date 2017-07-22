using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float power;
    public GameObject HpBar;
    Canvas UICanvas;
    public float PlayerHPValue;
    GameObject PlayerHp;
    Rigidbody2D body;
    public static float speed;
    public static float SpinSpeed;
    Animator PlayerAnimator;

    float PowerTime;
    float SpeedTime;

    // Use this for initialization
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        PlayerAnimator.SetBool("PowerUp", false);
        body = GetComponent<Rigidbody2D>();
        UICanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        power = 5.0f;
        speed = 9.0f;
        SpinSpeed = -10.0f;
        PlayerHp = Instantiate(HpBar, new Vector3(540, 350, 0), Quaternion.identity, UICanvas.transform);
        PlayerHp.transform.SetParent(UICanvas.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, SpinSpeed));
        PlayerHp.GetComponent<Slider>().value = PlayerHPValue;

        var team = GetComponent<PlayerQueue>().Value % 2;
        var offset = Vector3.zero;
        var offsetAbs = 100;
        if (team == 0) offset.y = -offsetAbs;
        else offset.y = offsetAbs;
        PlayerHp.transform.position = this.transform.position + offset;
    }

    public void PowerTimer()
    {
        StartCoroutine(CoPowerTimer());
    }

    private IEnumerator CoPowerTimer()
    {
        PlayerAnimator.SetBool("PowerUp", true);
        Debug.Log("powerco");
        power = 12.0f;
        SpinSpeed = -30.0f;
        yield return new WaitForSeconds(5.0f);
        PlayerAnimator.SetBool("PowerUp", false);
        power = 4.0f;
        SpinSpeed = -10.0f;
    }

    public void SpeedTimer()
    {
        StartCoroutine(CoSpeedTimer());
    }

    private IEnumerator CoSpeedTimer()
    {
        Debug.Log("speedco");
        speed = 14.0f;
        yield return new WaitForSeconds(5.0f);
        speed = 9.0f;
    }
}
