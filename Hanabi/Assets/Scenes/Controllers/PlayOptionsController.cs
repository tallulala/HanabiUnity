using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Connected to the AICardOptions and the PlayerCardOptions GameObjects
/// TODO: Make adding moves to the scroll view better -> wording, faster?
/// </summary>
public class PlayOptionsController : MonoBehaviour
{
    public CardController CardCont;
    public TokenController TokenCont;

    public bool IsPlayerTurn;

    public GameObject HintBox;
    public GameObject Background;

    public TextMesh Rank;
    public TextMesh Color;
    public TextMesh Play;
    public TextMesh Disc;

    public int Score = 0;
    public Text ScoreText;
    public ScrollRect Moves;
    public Text MovesLog;
    public Text Turn;

    public Options MoveOption;

    /// <summary>
    /// Available move options to make on player turn
    /// </summary>
    public enum Options
    {
        RANK, COLOR, PLAY, DISC
    };

    /// <summary>
    /// Player chooses a move to make on their turn
    /// </summary>
    /// <param name="opt">Corresponds to the option the player chose from one of the pop-up menus after selecting a card</param>
    public void YourMove(int opt)
    {
        MoveOption = (Options)opt;

        switch (MoveOption)
        {
            case Options.RANK:
                HintRank();
                break;
            case Options.COLOR:
                HintColor();
                break;
            case Options.PLAY:
                PlayCard();
                break;
            case Options.DISC:
                Discard();
                break;
            default:
                break;
        }
        CardCont.ButtonCont.Selected.transform.position = CardCont.OffScreen;
        gameObject.transform.position = CardCont.OffScreen;

        if (CardCont.ButtonCont.isGameOver == true)
        {
            return;
        } else
        {
            StartCoroutine(EndOfPlayerTurn());
        }
    }

    ///<summary>
    /// Tells the Ai to make it's move, then gives control back to player.
    ///</summary>
    /// <returns>IEnumerator WaitForSeconds()</returns>
    public IEnumerator EndOfPlayerTurn()
    {
        Debug.Log("Waiting");

        CardCont.ButtonCont.PlayerCardMenu.GetComponent<PlayOptionsController>().Turn.text = "Computer's Turn";

        yield return new WaitForSeconds(1);

        Background.GetComponent<AiController>().MakeMove();

        yield return new WaitForSeconds(1);

        CardCont.ButtonCont.PlayerCardMenu.GetComponent<PlayOptionsController>().Turn.text = "Your Turn";

        Debug.Log("Waited");
    }

    /// <summary>
    /// Gives the AI a hint of all the cards of a certain rank in it's hand
    /// TODO: Add this knowledge to AI's mental state of it's own hand
    /// </summary>
    public void HintRank()
    {
        
        Debug.Log("Player HintRank()");
        Background.GetComponent<TokenController>().RemoveHint();

        foreach (GameObject card in CardCont.ButtonCont.Deck)
        {
            if (card.GetComponent<CardController>().location == CardCont.location)
            {
                if (card.GetComponent<CardController>().RankLabel.text == CardCont.RankLabel.text)
                {
                    card.GetComponent<CardController>().HintBox.SetActive(true);
                    card.GetComponent<CardController>().HintedRank.text = CardCont.RankLabel.text;
                }
            }
        }

        MovesLog.text = ("You hinted your oponent's " + CardCont.RankLabel.text + " cards.\n\n") + MovesLog.text;
        
    }

    /// <summary>
    /// Gives the AI a hint of all the cards of a certain color in it's hand
    /// TODO: Add this knowledge to AI's mental state of it's own hand
    /// </summary>
    public void HintColor()
    {
        Debug.Log("Player HintColor()");
        foreach (GameObject card in CardCont.ButtonCont.Deck)
        {
            if (card.GetComponent<CardController>().location == CardCont.location)
            {
                if (card.GetComponent<CardController>().CardColor == CardCont.CardColor)
                {
                    card.GetComponent<CardController>().HintBox.SetActive(true);
                    card.GetComponent<CardController>().HintBox.GetComponent<MeshRenderer>().material = CardCont.CardColor;
                }
            }
        }

        Background.GetComponent<TokenController>().RemoveHint();

        MovesLog.text = ("You hinted your oponent's " + CardCont.GetColorName() + " cards.\n\n") + MovesLog.text;
    }

    /// <summary>
    /// Moves the selected card to the proper place in either the play field or the discard pile
    /// </summary>
    public void PlayCard()
    {
        Debug.Log("Player PlayCard()");
        CardCont.HintBox.SetActive(false);

        bool ValidMove = true;
        Vector3 startPos = CardCont.transform.position;
        Vector3 endPos;

        CardCont.transform.Rotate(Vector3.right, 180);
        CardCont.RankLabel.gameObject.SetActive(true);

        CardCont.ButtonCont.Selected.transform.position = CardCont.OffScreen;
        gameObject.transform.position = CardCont.OffScreen;
        CardCont.ButtonCont.DealOne(CardCont.transform.position, true);

        if ((int)CardCont.rank != CardCont.ButtonCont.PlayIdx[(int)CardCont.color])
        {
            ValidMove = false;
        }

        if (!ValidMove)
        {
            CardCont.location = CardController.Location.TRASH;

            endPos = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];

            StartCoroutine(CardCont.ButtonCont.Animation(CardCont.gameObject, startPos, endPos));
            CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]++;

            Background.GetComponent<TokenController>().RemoveMistake();
        }
        else
        {
            CardCont.location = CardController.Location.BOARD;

            endPos = CardCont.ButtonCont.PlayArea[(int)CardCont.color][(int)CardCont.rank];

            StartCoroutine(CardCont.ButtonCont.Animation(CardCont.gameObject, startPos, endPos));
            CardCont.ButtonCont.PlayIdx[(int)CardCont.color]++;

            if (CardCont.rank == CardController.Rank.FIVE)
            {
                Background.GetComponent<TokenController>().AddHint();
            }

            Score++;
            ScoreText.text = "Score: " + Score + "/25";
        }

        if (ValidMove)
        {
            MovesLog.text = ("You played your " + CardCont.GetColorName() +
                CardCont.RankLabel.text + " card.\n\n") + MovesLog.text;
        }
        else
        {
            MovesLog.text = ("You attempted to play your " + CardCont.GetColorName() +
                CardCont.RankLabel.text + " card, but it was discarded.\n\n") + MovesLog.text;
        }
    }

    /// <summary>
    /// Moves the selected card to the proper place in the discard pile
    /// TODO: Order discard pile by rank
    /// </summary>
    public void Discard()
    {
        Debug.Log("Player Discard()");
        CardCont.HintBox.SetActive(false);

        Vector3 startPos = CardCont.transform.position;
        Vector3 endPos = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];

        CardCont.transform.Rotate(Vector3.right, 180);
        CardCont.RankLabel.gameObject.SetActive(true);

        CardCont.ButtonCont.Selected.transform.position = CardCont.OffScreen;
        gameObject.transform.position = CardCont.OffScreen;
        CardCont.ButtonCont.DealOne(CardCont.transform.position, true);

        CardCont.location = CardController.Location.TRASH;

        StartCoroutine(CardCont.ButtonCont.Animation(CardCont.gameObject, startPos, endPos));
        // CardCont.transform.position = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];
        CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]++;

        Background.GetComponent<TokenController>().AddHint();

        MovesLog.text = ("You discarded your " + CardCont.GetColorName() +
            CardCont.RankLabel.text + " card.\n\n") + MovesLog.text;
    }
}

