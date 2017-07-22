using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float power;
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
        PlayerAnimator.SetBool("Groggy", false);
        power = 5.0f;
        speed = 12.0f;
        SpinSpeed = -10.0f;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, SpinSpeed * Time.deltaTime * 60));
    }

    public void PowerTimer()
    {
        StartCoroutine(CoPowerTimer());
    }

    private IEnumerator CoPowerTimer()
    {
        PlayerAnimator.SetBool("PowerUp", true);
        // Debug.Log("powerco");
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
        // Debug.Log("speedco");
        speed = 18.0f;
        yield return new WaitForSeconds(5.0f);
        speed = 12.0f;
    }

    public void GroggyTimer()
    {
        StartCoroutine(CoGroggyTimer());
    }

    private IEnumerator CoGroggyTimer()
    {
        GameObject.Find("Canvas").GetComponent<TouchScreen>().enabled = false;
        PlayerAnimator.SetBool("Groggy", true);
        SpinSpeed = 0.0f;
        yield return new WaitForSeconds(1.7f);
        GameObject.Find("Canvas").GetComponent<TouchScreen>().enabled = true;
        PlayerAnimator.SetBool("Groggy", false);
        SpinSpeed = -10.0f;
    }
}
