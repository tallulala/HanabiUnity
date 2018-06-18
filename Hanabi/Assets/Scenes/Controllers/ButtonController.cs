using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public GameObject Card;
    public GameObject[] cards;

    //public Transform faceUp;

    public Vector3 deckPos;
    public Vector3 offScreen;

    public Vector3[] playerHand;
    public Vector3[] computerHand;

    public void Start()
    {
        cards = new GameObject[50];
        deckPos = new Vector3(-20f, 0f, 6f);
        //faceUp.rotation = Quaternion.Euler(180, 0, 180);
        offScreen = new Vector3(6000000f, 0f, 6000000f);

    }

    public void OnClick()
	{
        gameObject.transform.position = offScreen;

        for (int i = 0; i < 50; i++)
        {
            cards[i] = Instantiate(Card, deckPos, Quaternion.identity);
        }
        
        // Deal();
	}
		
    public void Deal ()
    {
        //if any empty spaces new card
    }
}
