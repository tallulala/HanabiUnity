using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public GameObject Card;
    public GameObject Front;
    public TextMesh Label;
    public GameObject[] Deck;

    public int DeckCount;

    public int[] rankCount = { 3, 2, 2, 2, 1 };

    public Vector3 DeckPos;
    public Vector3 OffScreen;

    public Vector3[] PlayerHand;
    public Vector3[] ComputerHand;
    public Vector3[] PlayArea;
    public Vector3[] DiscardPile;

    public void Start()
    {
        DeckPos = new Vector3(-1368, 15, 550);

        Deck = new GameObject[50];
        DeckCount = 0;

        OffScreen = new Vector3(6000000f, 0f, 6000000f);

        ComputerHand = new Vector3[5];
        PlayerHand = new Vector3[5];
        for (int i = 0; i < 5; i++)
        {
            ComputerHand[i] = new Vector3((800 + (330 * i)), 2, -50);
            PlayerHand[i] = new Vector3((800 + (330 * i)), 2, -1750);
        }
    }

    public void OnClick()
    {
        gameObject.transform.position = OffScreen;

        GenerateDeck();

        Deck = Shuffle(Deck);

        Deal(PlayerHand);
        Deal(ComputerHand);
    }

    public void GenerateDeck()
    {
        for (int i = 0; i < 50; i++)
        {
            Deck[i] = Instantiate(Card, DeckPos, Quaternion.identity);
            Deck[i].name = ("Card" + i);
            Deck[i].SetActive(true);
            Deck[i].transform.Rotate(Vector3.right, 180);
            Deck[i].GetComponent<CardController>().inHand = false;
            DeckCount++;
        }

        int idx = 0;
        int rank = 0;

        CardController thisCard;

        for (int j = 0; j < 5; j++)
        {
            foreach (int count in rankCount)
            {
                for (int k = 0; k < count; k++)
                {
                    thisCard = Deck[idx].GetComponent<CardController>();
                    thisCard.setRank(rank);
                    thisCard.setColor(j);
                    idx++;
                }
                rank++;
            }
        }
    }

    public void Deal (Vector3[] hand)
    {
        Vector3 position;

        foreach (Vector3 pos in hand)
        {
            position = pos;

            Deck[DeckCount - 1].transform.Translate(position, Space.World);

            Deck[DeckCount - 1].transform.Rotate(Vector3.right, 180);

            Deck[DeckCount - 1].GetComponent<CardController>().inHand = true;

            DeckCount--;
        }
    }

    public GameObject[] Shuffle(GameObject[] shuffledDeck)
    {
        System.Random random = new System.Random();
        shuffledDeck = new GameObject[DeckCount];
        HashSet<int> checkDistinct = new HashSet<int>();
        int current;

        for (int i = 0; i < DeckCount; i++)
        {
            current = random.Next(DeckCount);
            while (checkDistinct.Contains(current))
            {
                current = random.Next(DeckCount);
            }

            shuffledDeck[i] = Deck[current];
            checkDistinct.Add(current);
        }
        return shuffledDeck;
    }
}