using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public GameObject Card;
    public GameObject PlayField;
    public GameObject Background;
    public GameObject[] Deck;

    public int DeckIndex;

    public Vector3 DeckPos;
    public Vector3 PlayFieldSize;
    public Vector3 BackgroundSize;
    public Vector3 OffScreen;

    public Vector3[] PlayerHand;
    public Vector3[] ComputerHand;
    public Vector3[] PlayArea;
    public Vector3[] DiscardPile;


    public void Start()
    {
        Background.transform.Translate(new Vector3(0f, 0f, 0f), Space.World);
        PlayField.transform.Translate(new Vector3(0, 0.01f, 0), Space.World);
       
        Deck = new GameObject[50];
        DeckIndex = 0;
        DeckPos = new Vector3(-87, 1, 50);

        OffScreen = new Vector3(6000000f, 0f, 6000000f);

        ComputerHand = new Vector3[5];
        PlayerHand = new Vector3[5];
        for (int i = 0; i < 5; i++)
        {
            ComputerHand[i] = new Vector3((12 * i), 1, 40);
            PlayerHand[i] = new Vector3((12 * i), 1, -40);
        }
    }

    public void OnClick()
    {
        gameObject.transform.position = OffScreen;

        for (int i = 0; i < 50; i++)
        {
            Deck[i] = Instantiate(Card, DeckPos, Quaternion.identity);
            Deck[i].name = ("Card" + i);
            DeckIndex++;
        }

        Deal();
    }
    
    public void Deal ()
    {
        Vector3 position;
        float radius = 1;
 
        foreach (Vector3 pos in ComputerHand) 
        {
            position = pos;

            if (Physics.CheckSphere(position, radius))
            {
                Deck[DeckIndex - 1].transform.Translate(position, Space.World);
                Deck[DeckIndex - 1].transform.Rotate(Vector3.up, 180);
                DeckIndex--;
            }
        }

        foreach (Vector3 pos in PlayerHand)
        {
            position = pos;

            if (Physics.CheckSphere(position, radius))
            {
                Deck[DeckIndex - 1].transform.Translate(position, Space.World);
                Deck[DeckIndex - 1].transform.Rotate(Vector3.up, 180);
                DeckIndex--;
            }
        }

        Debug.Log("Dealt");
    }
}

/*
    private Vector3 GetMaxBounds(GameObject playField)
    {
        throw new NotImplementedException();
    }

 */
