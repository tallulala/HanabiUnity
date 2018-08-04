using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Connected to the StartGame GameObject
/// </summary>
public class ButtonController : MonoBehaviour
{
    public GazeTrackingController GazeCont;

    public GameObject Card;
    public GameObject Front;
    public GameObject[] Deck;
    public GameObject RestartButton;

    public GameObject NameField;
    public string playerName;

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

    public bool isGameOver;

    /// Assigns variables
    public void Start()
    {
        isGameOver = false;

        DeckPos = new Vector3(-2200, 25, 250);

        OffScreen = new Vector3(6000000f, 0f, 6000000f);

        RestartButton.transform.position = OffScreen;

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
            PlayerHand[i] = new Vector3((1800 + (800 * i)), 2, -3500) + DeckPos;
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
                /// Use for testing:
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
                /// Use for testing:
                // Instantiate(Card, DiscardArea[i][k], Quaternion.identity);
            }

            k = 0;
        }
    }

    /// <summary>
    /// Generates and shuffles the deck. Deals five cards to each player
    /// </summary>
    public void OnClick()
    {
        Debug.Log("Buttoncont click");

        StartCoroutine(GazeCont.MakeMap());

        gameObject.transform.position = OffScreen;
        PlayerCardMenu.transform.position = OffScreen;
        ComputerCardMenu.transform.position = OffScreen;
        Selected.transform.position = OffScreen;
        NameField.transform.position = OffScreen;

        playerName = NameField.GetComponent<InputField>().text;

        Deck = new GameObject[50];

        Deck = MakeDeck();
        Deck = Shuffle(Deck);

        StartCoroutine(DealHands());
    }

    /// <summary>
    /// Pauses in between dealing the players hand and the computer's hand for visual clarity
    /// </summary>
    /// <returns>IEnumerator, WaitForSeconds()</returns>
    public IEnumerator DealHands()
    {
        DealToHand(PlayerHand, true);
        Debug.Log("Switching Hands");
        yield return new WaitForSeconds(3);
        DealToHand(ComputerHand, false);
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

    /// <summary>
    /// Deals cards to each position in a specified hand
    /// </summary>
    /// <param name="hand">The positions in the specified hand</param>
    /// <param name="player">The player whose hand should be dealt to</param>
    public void DealToHand(Vector3[] hand, bool player)
    {
        float i = 0.5f;
        foreach (Vector3 pos in hand)
        {
            Debug.Log("Dealing next card...");
            DealOne(pos, player, i);
            Debug.Log("Coroutine executed");
            i += 0.5f;
        }
    }

    /// <summary>
    /// Deals one card to the specified position
    /// </summary>
    /// <param name="position">The position that the card should be placed</param>
    /// <param name="player">Specifies which hand the card should be dealt to, and whether the card should be face up or face down.</param>
    /// <returns>The card that was dealt</returns>
    public GameObject DealOne(Vector3 position, bool player, float delay = 0.0f)
    {
        GameObject card = Deck[DeckCount - 1];

        if (Deck[0] == null)
        {
            isGameOver = true;
            GameOver();
        }
        else
        {
            StartCoroutine(Animation(card, card.transform.position, position, delay));

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

        Debug.Log("Dealing card to: " + position);
        return card;
    }

    /// <summary>
    /// Adds cards from the previously instatiated deck, to a new array in 'random' order
    /// </summary>
    /// <param name="shuffledDeck">temporary deck to add cards to in 'random' order</param>
    /// <returns>The shuffled array</returns>
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

    /// <summary>
    /// Makes an object move from one position to another by slowly sliding accross the screen
    /// TODO: Add sin curve
    /// </summary>
    /// <param name="GO">The GameObject to be moved</param>
    /// <param name="start">The original oposition of the GameObject</param>
    /// <param name="destination">The ending position of the GameObject</param>
    /// <returns>IEnumerator wait for end of frame</returns>
    public IEnumerator Animation(GameObject GO, Vector3 start, Vector3 destination, float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);

        float theta = 0;
        while (theta < 1)
        {
            // linear interpolation
            theta += Time.deltaTime;
            GO.transform.position = (1 - theta) * start + theta * destination;
            yield return new WaitForEndOfFrame();
        }
        GO.transform.position = destination;
    }

    /// <summary>
    /// Ends game
    /// Happens when three mistakes are made 
    /// OR when there are no more cards in the deck
    /// </summary>
    public void GameOver()
    {
        Debug.Log("GAME OVER");

        GazeCont.GetComponent<GazeTrackingController>().SaveTextureAsPNG();

        PlayerCardMenu.GetComponent<PlayOptionsController>().ScoreText.transform.position = new Vector3(2500, 50, -1500);

        RestartButton.transform.position = new Vector3(3000, 50, -2000);

        foreach (GameObject card in Deck)
        {
            card.SetActive(false);
            Debug.Log(card);
        }
        Debug.Log("cards destroyed: " + Deck.Length);

        foreach (GameObject token in Background.GetComponent<TokenController>().Mistakes)
        {
            token.SetActive(false);
        }
        foreach (GameObject token in Background.GetComponent<TokenController>().Hints)
        {
            token.SetActive(false);
        }
        Debug.Log("tokens destroyed");
    }
}