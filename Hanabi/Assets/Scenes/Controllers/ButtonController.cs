using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public GameObject Card;
    public GameObject Front;
    public GameObject[] Deck;

    public GameObject PlayerCardMenu;
    public GameObject ComputerCardMenu;

    public int DeckCount;

    public int[] RankCount = { 3, 2, 2, 2, 1 };

    public Vector3 DeckPos;
    public Vector3 OffScreen;

    public Vector3[] PlayerHand;
    public Vector3[] ComputerHand;
    public Vector3[] PlayArea;
    public Vector3[] DiscardPile;

    /// Assigns variables
    public void Start()
    {
        DeckPos = new Vector3(-1365, 25, 570);

        Deck = new GameObject[50];

        OffScreen = new Vector3(6000000f, 0f, 6000000f);

        ComputerHand = new Vector3[5];
        PlayerHand = new Vector3[5];
        for (int i = 0; i < 5; i++)
        {
            ComputerHand[i] = new Vector3((800 + (350 * i)), 2, -75);
            PlayerHand[i] = new Vector3((800 + (350 * i)), 2, -1775);
        }
    }

    ///Clicking the start button
    /// Generates and shuffles deck
    /// Deals five cards to each player
    public void OnClick()
    {
        gameObject.transform.position = OffScreen;

        PlayerCardMenu.transform.position = OffScreen;
        ComputerCardMenu.transform.position = OffScreen;

        Deck = MakeDeck();

        Deck = Shuffle(Deck);

        Deal(PlayerHand, true);
        Deal(ComputerHand, false);
    }

    /// Generates 50 cards objects, adds color and rank to each card
    /// 3 ones of each color
    /// 2 twos, 2 threes, and 2 fours of each color
    /// 1 five of each color
    public GameObject[] MakeDeck()
    {
        int i = 0;
        int rank;

        CardController thisCard;

        for (int j = 0; j < 5; j++)
        {
            rank = 0;
            foreach (int count in RankCount)
            {
                for (int k = 0; k < count; k++)
                {
                    Deck[i] = Instantiate(Card, DeckPos, Quaternion.identity);
                    Deck[i].GetComponent<CardController>().ButtonCont = this;
                    Deck[i].GetComponent<CardController>().location = CardController.Location.DECK;
                    Deck[i].name = ("Card" + i);
                    Deck[i].SetActive(true);
                    Deck[i].transform.Rotate(Vector3.right, 180);

                    Deck[i].GetComponent<CardController>().PlayerCardMenu = PlayerCardMenu;
                    Deck[i].GetComponent<CardController>().ComputerCardMenu = ComputerCardMenu;
                    Deck[i].GetComponent<CardController>().location = CardController.Location.DECK;

                    DeckCount++;

                    thisCard = Deck[i].GetComponent<CardController>();
                    thisCard.SetRank(rank);
                    thisCard.SetColor(j);
                    Debug.Log("making " + rank.ToString() + " of color " + j.ToString());
                    i++;
                }
                rank++;
            }
        }

        return Deck;
    }

    /// Deals cards to each position in a specified hand
    /// @param Hand array of positions in a certain players hand
    public void Deal(Vector3[] hand, bool player)
    {
        Vector3 position;

        foreach (Vector3 pos in hand)
        {
            position = pos;

            Deck[DeckCount - 1].transform.Translate(position, Space.World);

            Deck[DeckCount - 1].transform.Rotate(Vector3.right, 180);

            if (player)
                Deck[DeckCount - 1].GetComponent<CardController>().location = CardController.Location.PLAYER;
            else
                Deck[DeckCount - 1].GetComponent<CardController>().location = CardController.Location.COMPUTER;
            DeckCount--;
        }
    }

    /// Adds cards to new array in 'random' order
    /// @param shuffledDeck temporary deck to add cards to in 'random' order
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