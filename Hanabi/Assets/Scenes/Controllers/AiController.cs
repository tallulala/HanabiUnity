using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Connected to the background of the game
/// All pseudo-random moves. AI does not make any decisions yet.
/// </summary>
public class AiController : MonoBehaviour
{
    public ButtonController ButtonCont;
    public PlayOptionsController PlayCont;

    /// <summary>
    /// AI psuedo-randomly chooses a move to make on it's turn
    /// Called in PlayOptionsController after a player makes their move
    /// TODO: Add options enum?
    /// </summary>
    public void MakeMove()
    {
        System.Random random = new System.Random();
        int Move = random.Next(1, 4);

        switch (Move)
        {
            case 1:
                HintRank();
                break;
            case 2:
                HintColor();
                break;
            case 3:
                PlayCard();
                break;
            case 4:
                Discard();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Gives the player a hint of all the cards of a certain rank in their hand
    /// </summary>
    public void HintRank()
    {
        Debug.Log("AI HintRank()");
        CardController thisCard = PickCard(CardController.Location.PLAYER);
        PlayCont.Background.GetComponent<TokenController>().RemoveHint();
        foreach (GameObject card in thisCard.ButtonCont.Deck)
        {
            if (card.GetComponent<CardController>().location == thisCard.GetComponent<CardController>().location)
            {
                if (card.GetComponent<CardController>().rank == thisCard.GetComponent<CardController>().rank)
                {
                    if (!card.GetComponent<CardController>().HintBox.activeInHierarchy)
                    {
                        card.GetComponent<CardController>().HintBox.SetActive(true);
                        card.GetComponent<CardController>().HintBox.transform.Rotate(Vector3.right, 180);
                        card.GetComponent<CardController>().HintBox.transform.localPosition = new Vector3(-0.5f, -5, -0.7f);
                    }
                    card.GetComponent<CardController>().HintedRank.text = thisCard.RankLabel.text;
                }
            }
        }

        // Add move to scroll view
        PlayCont.MovesLog.text = ("The Computer hinted your " + thisCard.RankLabel.text + " cards.\n\n") + PlayCont.MovesLog.text;
    }

    /// <summary>
    /// Gives the player a hint of all the cards of a certain color in their hand
    /// </summary>
    public void HintColor()
    {
        Debug.Log("AI HintColor()");
        CardController thisCard = PickCard(CardController.Location.PLAYER);
        PlayCont.Background.GetComponent<TokenController>().RemoveHint();
        foreach (GameObject card in thisCard.ButtonCont.Deck)
        {
            if (card.GetComponent<CardController>().location == thisCard.GetComponent<CardController>().location)
            {
                if (card.GetComponent<CardController>().CardColor == thisCard.GetComponent<CardController>().CardColor)
                {
                    if (!card.GetComponent<CardController>().HintBox.activeInHierarchy)
                    {
                        card.GetComponent<CardController>().HintBox.SetActive(true);
                        card.GetComponent<CardController>().HintBox.transform.Rotate(Vector3.right, 180);
                        card.GetComponent<CardController>().HintBox.transform.localPosition = new Vector3(-0.5f, -5, -0.7f);
                    }
                    card.GetComponent<CardController>().HintBox.GetComponent<MeshRenderer>().material = thisCard.GetComponent<CardController>().CardColor;
                }
            }
        }

        // Add move to scroll view
        PlayCont.MovesLog.text = ("The Computer hinted your " + thisCard.GetColorName() + " cards.\n\n") + PlayCont.MovesLog.text;
    }

    /// <summary>
    /// Plays a 'randomly' selected card
    /// </summary>
    public void PlayCard()
    {
        Debug.Log("AI PlayCard()");
        CardController thisCard = PickCard(CardController.Location.COMPUTER);

        Vector3 startPos = thisCard.GetComponent<CardController>().transform.position;
        Vector3 endPos;
        bool ValidMove = true;

        thisCard.GetComponent<CardController>().HintBox.SetActive(false);

        if ((int)thisCard.GetComponent<CardController>().rank != thisCard.ButtonCont.PlayIdx[(int)thisCard.color])
        {
            ValidMove = false;
        }

        thisCard.ButtonCont.DealOne(thisCard.transform.position, false);

        if (!ValidMove)
        {
            thisCard.GetComponent<CardController>().location = CardController.Location.TRASH;
            PlayCont.Background.GetComponent<TokenController>().RemoveMistake();

            endPos = thisCard.ButtonCont.DiscardArea[(int)thisCard.color][thisCard.ButtonCont.DiscardIdx[(int)thisCard.color]];
            StartCoroutine(thisCard.ButtonCont.Animation(thisCard.GetComponent<CardController>().gameObject, startPos, endPos));
            thisCard.GetComponent<CardController>().ButtonCont.DiscardIdx[(int)thisCard.color]++;
        }
        else
        {
            if (thisCard.GetComponent<CardController>().rank == CardController.Rank.FIVE)
            {
                PlayCont.Background.GetComponent<TokenController>().AddHint();
            }

            thisCard.GetComponent<CardController>().location = CardController.Location.BOARD;

            endPos = thisCard.ButtonCont.PlayArea[(int)thisCard.color][(int)thisCard.rank];
            StartCoroutine(thisCard.ButtonCont.Animation(thisCard.GetComponent<CardController>().gameObject, startPos, endPos));

            PlayCont.GetComponent<PlayOptionsController>().Score++;
            PlayCont.GetComponent<PlayOptionsController>().ScoreText.text = "Score: " + PlayCont.GetComponent<PlayOptionsController>().Score + "/25";
        }

        // Add move to scroll view
        if (ValidMove)
        {
            PlayCont.MovesLog.text = ("The Computer played its " + thisCard.GetColorName() +
                thisCard.RankLabel.text + " card.\n\n") + PlayCont.MovesLog.text;
        }
        else
        {
            PlayCont.MovesLog.text = ("The Computer attempted to play its " + thisCard.GetColorName() +
                thisCard.RankLabel.text + " card, but it was discarded.\n\n") + PlayCont.MovesLog.text;
        }
    }

    /// <summary>
    /// Discards a 'randomly' selected card
    /// </summary>
    public void Discard()
    {
        Debug.Log("AI Discard()");
        CardController thisCard = PickCard(CardController.Location.COMPUTER);
        Vector3 startPos = thisCard.transform.position;
        Vector3 endPos;

        thisCard.GetComponent<CardController>().HintBox.SetActive(false);
        thisCard.GetComponent<CardController>().location = CardController.Location.TRASH;
        PlayCont.Background.GetComponent<TokenController>().AddHint();

        endPos = thisCard.ButtonCont.DiscardArea[(int)thisCard.color][thisCard.ButtonCont.DiscardIdx[(int)thisCard.color]];

        thisCard.ButtonCont.DealOne(thisCard.transform.position, false);

        StartCoroutine(thisCard.GetComponent<CardController>().ButtonCont.Animation(thisCard.gameObject, startPos, endPos));
        thisCard.GetComponent<CardController>().ButtonCont.DiscardIdx[(int)thisCard.color]++;

        // Add move to scroll view
        PlayCont.MovesLog.text = ("The Computer discarded its " + thisCard.GetColorName() +
                thisCard.RankLabel.text + " card.\n\n") + PlayCont.MovesLog.text;
    }

    /// <summary>
    /// 'Randomly' selects a card from the given hand
    /// TODO: Have AI pick a card based on it's knowledge of the game
    /// </summary>
    /// <param name="loc">The hand from which to select a card. Either 'Player' or 'Computer'.</param>
    public CardController PickCard(CardController.Location loc)
    {
        GameObject[] cards = new GameObject[5];
        int i = 0;
        foreach (GameObject card in ButtonCont.Deck)
        {
            if (card.GetComponent<CardController>().location == loc)
            {
                cards[i] = card;
                i++;
            }
        }
        Debug.Assert(i == 5);

        System.Random random = new System.Random();
        return cards[random.Next(0, 5)].GetComponent<CardController>();
    }
}