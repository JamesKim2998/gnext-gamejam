using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScript : MonoBehaviour {
    public GameObject BackImage;
    public GameObject ManImg;
    public GameObject GirlImg;
    public GameObject VSImg;
    public GameObject VSPImg;
    public GameObject GameManager;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void StartGame()
   {
        BackImage.SetActive(false);
        ManImg.SetActive(false);
        GirlImg.SetActive(false);
        VSImg.SetActive(false);
        VSPImg.SetActive(false);
       this.gameObject.SetActive(false);
       GameManager.SetActive(true);
   }
}
