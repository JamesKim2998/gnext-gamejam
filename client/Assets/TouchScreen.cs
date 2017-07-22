using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour {
    Vector3 StartPoint;
    Vector3 EndPoint;
    public static Vector3 inNormal;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetTouch(0).phase ==TouchPhase.Began)
        //if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("hi");
            Vector3 temppoint = Input.GetTouch(0).position;
            StartPoint = Camera.main.ScreenToWorldPoint(temppoint);
            Debug.Log(StartPoint);
        }
        else if(Input.GetTouch(0).phase == TouchPhase.Moved)
        //else if (Input.GetMouseButton(0))
        {
            Vector3 temppoint2 = Input.GetTouch(0).position;
            EndPoint = Camera.main.ScreenToWorldPoint(temppoint2);
            inNormal = Vector3.Normalize(EndPoint - StartPoint);
            Debug.Log(inNormal);
        }
        else;
	}
}
