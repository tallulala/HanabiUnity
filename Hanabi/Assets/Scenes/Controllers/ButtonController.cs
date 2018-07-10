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
    public GameObject Selected;
    public GameObject Background;
    
    public int DeckCount;

    public int[] RankCount = { 3, 2, 2, 2, 1 };

    public Vector3 DeckPos;
    public Vector3 OffScreen;

    public Vector3[] PlayerHand;
    public Vector3[] ComputerHand;

    public Vector3[][] DiscardArea;
    public Vector3[][] PlayArea;

    public int[] DiscardIdx;
    public int[] PlayIdx;

    /// Assigns variables
    public void Start()
    {
        DeckPos = new Vector3(-2200, 25, 250);

        Deck = new GameObject[50];

        OffScreen = new Vector3(6000000f, 0f, 6000000f);

        /// Hands, play area, discard area
        ComputerHand = new Vector3[5];
        PlayerHand = new Vector3[5];

        DiscardArea = new Vector3[5][];
        PlayArea = new Vector3[5][];

        DiscardIdx = new int[5];
        PlayIdx = new int[5];

        /// Instantiate Computer and Player hand positions
        for (int i = 0; i < 5; i++)
        {
            ComputerHand[i] = new Vector3((1800 + (800 * i)), 2, 200) + DeckPos;
            PlayerHand[i] = new Vector3((1800 + (800 * i)), 2, -3350) + DeckPos;
        }

        /// Instantiate PlayArea positions
        for (int i = 0; i < 5; i++)
        {
            PlayArea[i] = new Vector3[5];

            PlayIdx[i] = 0;

            int j;

            for (j = 0; j < 5; j++)
            {
                PlayArea[i][j] = new Vector3((-400 + (800 * i)), (25 + (j * 2)), (-650 - (350 * j)));
                //Use for testing:
                //Instantiate(Card, PlayArea[i][j], Quaternion.identity);
            }

            j = 0;
        }

        /// Instantiate DiscardArea positions
        for (int i = 0; i < 5; i++)
        {
            DiscardArea[i] = new Vector3[10];
            DiscardIdx[i] = 0;
            int k;

            for (k = 0; k < 10; k++)
            {
                DiscardArea[i][k] = new Vector3(4000 + (350 * k), (25 + (k * 2)), (500 - (900 * i)));
                //Use for testing:
                // Instantiate(Card, DiscardArea[i][k], Quaternion.identity);
            }

            k = 0;
        }
    }

    /// Clicking the start button
    /// Generates and shuffles deck
    /// Deals five cards to each player
    public void OnClick()
    {
        gameObject.transform.position = OffScreen;
        PlayerCardMenu.transform.position = OffScreen;
        ComputerCardMenu.transform.position = OffScreen;
        Selected.transform.position = OffScreen;

        Deck = MakeDeck();

        Deck = Shuffle(Deck);

        Deal(PlayerHand, true);
        StartCoroutine(Wait(1));
        Debug.Log("Switching Hands");
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

                    thisCard = Deck[i].GetComponent<CardController>();

                    thisCard.ButtonCont = this;

                    thisCard.name = ("Card" + i);
                    thisCard.transform.Rotate(Vector3.right, 180);
                    thisCard.RankLabel.gameObject.SetActive(false);

                    thisCard.MoveCardOptions = PlayerCardMenu;
                    thisCard.HintOptions = ComputerCardMenu;
                    thisCard.Selected = Selected;

                    thisCard.location = CardController.Location.DECK;

                    thisCard.SetRank(rank);
                    thisCard.SetColor(j);

                    DeckCount++;

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
        foreach (Vector3 pos in hand)
        {
            Wait(0.5f);
            DealOne(pos, player);
        }
    }

    public GameObject DealOne(Vector3 position, bool player)
    {
        GameObject card = Deck[DeckCount - 1];

        if (Deck[0] == null)
        {
            Background.GetComponent<TokenController>().GameOver();
        }
        else
        {
            StartCoroutine(Animation(card, card.transform.position, position));

            if (player)
            {
                card.GetComponent<CardController>().location = CardController.Location.PLAYER;
            }
            else
            {
                card.GetComponent<CardController>().location = CardController.Location.COMPUTER;
                card.transform.Rotate(Vector3.right, 180);
                card.GetComponent<CardController>().RankLabel.gameObject.SetActive(true);
            }
            DeckCount--;
        }
        return card;
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

    // linear interpolation
    public IEnumerator Animation(GameObject GO, Vector3 start, Vector3 destination)
    {
        float theta = 0;
        while (theta < 1)
        {
            theta += Time.deltaTime;
            GO.transform.position = (1 - theta) * start + theta * destination;
            yield return new WaitForEndOfFrame();
        }
        GO.transform.position = destination;
    }

    public IEnumerator Wait(float sec)
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(sec);
        Debug.Log("Waited");
    }
}