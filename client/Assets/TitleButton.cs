using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour {
    public GameObject TitleImage;
    public GameObject Title;

	// Use this for initialization
	void Start () {
        StartCoroutine(Blink());
    }


    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Blink()
    {
        Image spr = GetComponent<Image>();
        while (true)
        {
            Color color = spr.color;
            if (color.a == 0.0f)
                color.a = 1.0f;
            else
                color.a = 0.0f;
            spr.color = color;

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TitletoLobby()
    {
        SceneManager.LoadScene("main");
    }
}
