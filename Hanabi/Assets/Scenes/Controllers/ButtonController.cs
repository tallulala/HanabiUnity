using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public GameObject[] cards;
    public Vector3[] myhand;

	public void OnClick()
	{
		gameObject.SetActive (false);
        
		//GameObject.FindGameObjectWithTag ("Start Button").GetComponent<Button>().enabled = false;
		Debug.Log ("button");
	}
		
}
