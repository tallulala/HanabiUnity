using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public GameObject Card;

    public GameObject[] Deck;

    public int DeckCount;

    public Vector3 DeckPos;
    public Vector3 OffScreen;

    public Vector3[] PlayerHand;
    public Vector3[] ComputerHand;
    public Vector3[] PlayArea;
    public Vector3[] DiscardPile;

    public void Start()
    {
        Card = CardController.FindObjectOfType<GameObject>();

        DeckPos = new Vector3(-1638, 1, 345);

        Deck = new GameObject[50];
        DeckCount = 0;

        OffScreen = new Vector3(6000000f, 0f, 6000000f);

        ComputerHand = new Vector3[5];
        PlayerHand = new Vector3[5];
        for (int i = 0; i < 5; i++)
        {
            ComputerHand[i] = new Vector3((1000 + (330 * i)), 2, 200);
            PlayerHand[i] = new Vector3((1000 + (330 * i)), 2, -1500);
        }
    }

    public void OnClick()
    {
        gameObject.transform.position = OffScreen;

        GenerateDeck();

        Shuffle(Deck);

        Deal(PlayerHand);
        Debug.Log("player dealt");

        Deal(ComputerHand);
        Debug.Log("Comp dealt");
    }

    public void GenerateDeck()
    {
        for (int i = 0; i < 50; i++)
        {
            Deck[i] = Instantiate(Card, DeckPos, Quaternion.identity);
            Deck[i].name = ("Card" + i);
            Deck[i].transform.Rotate(Vector3.right, 180);
            DeckCount++;
        }        
    }

    public void Deal (Vector3[] hand)
    {
        Vector3 position;
        float radius = 1;

        foreach (Vector3 pos in hand)
        {
            position = pos;

            if (Physics.CheckSphere(position, radius))
            {
                Deck[DeckCount - 1].transform.Translate(position, Space.World);
                Deck[DeckCount - 1].transform.Rotate(Vector3.right, 180);
                DeckCount--;
            }
        }
    }

    public GameObject[] Shuffle(GameObject[] deck)
    {
        // TODO: write method

        return deck;
    }
}

/*
    private Vector3 GetMaxBounds(GameObject playField)
    {
        throw new NotImplementedException();
    }

 */
