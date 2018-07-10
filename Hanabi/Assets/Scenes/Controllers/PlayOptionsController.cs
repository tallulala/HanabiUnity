using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayOptionsController : MonoBehaviour
{
    public bool IsPlayerTurn;

    public int Score = 0;
    public Text ScoreText;

    public GameObject HintBox;
    public GameObject Background;

    public CardController CardCont;

    public TextMesh Rank;
    public TextMesh Color;
    public TextMesh Play;
    public TextMesh Disc;

    public ScrollRect Moves;

    public Text Turn;
    public Text MovesLog;

    public string player;
    public string move;
    public string card;

    public Options MoveOption;

    public enum Options
    {
        RANK, COLOR, PLAY, DISC
    };

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

        Turn.text = "Computer's Turn";

        System.Random rand = new System.Random();
        Background.GetComponent<AiController>().MakeMove(rand.Next(1, 4));

        Turn.text = "Your Turn";
    }

    public void HintRank()
    {
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

        /// Log move in scroll view

        MovesLog.text = ("You hinted your oponent's " + CardCont.RankLabel.text + " cards.\n\n") + MovesLog.text;
    }

    public void HintColor()
    {
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

        /// Log move in scroll view        
        MovesLog.text = ("You hinted your oponent's " + CardCont.GetColorName() + " cards.\n\n") + MovesLog.text;
    }

    public void PlayCard()
    {
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

            //CardCont.transform.position = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];
            //CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]++;

            Background.GetComponent<TokenController>().RemoveMistake();
        }
        else
        {
            CardCont.location = CardController.Location.BOARD;

            endPos = CardCont.ButtonCont.PlayArea[(int)CardCont.color][(int)CardCont.rank];

            StartCoroutine(CardCont.ButtonCont.Animation(CardCont.gameObject, startPos, endPos));

            //CardCont.transform.position = CardCont.ButtonCont.PlayArea[(int)CardCont.color][(int)CardCont.rank];
            //CardCont.ButtonCont.PlayIdx[(int)CardCont.color]++;

            if (CardCont.rank == CardController.Rank.FIVE)
            {
                Background.GetComponent<TokenController>().AddHint();
            }

            Score++;
            ScoreText.text = "Score: " + Score + "/25";
        }
        // tell ai to make move

        /// Log move in scroll view
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

    public void Discard()
    {
        CardCont.transform.Rotate(Vector3.right, 180);
        CardCont.RankLabel.gameObject.SetActive(true);

        CardCont.ButtonCont.Selected.transform.position = CardCont.OffScreen;
        gameObject.transform.position = CardCont.OffScreen;
        CardCont.ButtonCont.DealOne(CardCont.transform.position, true);

        CardCont.location = CardController.Location.TRASH;
        CardCont.transform.position = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];
        CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]++;

        Background.GetComponent<TokenController>().AddHint();

        /// Log move in scroll view

        MovesLog.text = ("You discarded your " + CardCont.GetColorName() +
            CardCont.RankLabel.text + " card.\n\n") + MovesLog.text;
    }
}

