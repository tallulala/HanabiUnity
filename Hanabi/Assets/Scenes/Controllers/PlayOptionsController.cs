using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayOptionsController : MonoBehaviour
{
    public bool IsPlayerTurn;

    public GameObject HintBox;

    public CardController CardCont;
    public TokenController TokenCont;

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
        if (IsPlayerTurn)
        {
            player = "The Computer ";
        }
        else
        {
            player = "You ";
        }

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
    }

    public void HintRank()
    {
        CardCont.HintBox.SetActive(true);
        CardCont.HintedRank.text = CardCont.RankLabel.text;
        TokenCont.RemoveHint();

        /// Log move in scroll view
        if (IsPlayerTurn)
        {
            MovesLog.text = ("You hinted your oponent's " + CardCont.RankLabel.text + " cards.\n\n") + MovesLog.text;
        }
        else
        {
            MovesLog.text = ("Your oponent hinted your " + CardCont.RankLabel.text + " cards.\n\n") + MovesLog.text;
        }

        NewTurn();
    }

    public void HintColor()
    {
        CardCont.HintBox.SetActive(true);
        CardCont.HintBox.GetComponent<MeshRenderer>().material = CardCont.CardColor;
        TokenCont.RemoveHint();

        /// Log move in scroll view
        if (IsPlayerTurn)
        {
            MovesLog.text = ("You hinted your oponent's " + CardCont.getColorName() + " cards.\n\n") + MovesLog.text;
        }
        else
        {
            MovesLog.text = ("Your oponent hinted your " + CardCont.getColorName() + " cards.\n\n") + MovesLog.text;
        }


        NewTurn();
    }

    public void PlayCard()
    {
        bool ValidMove = true;

        if (CardCont.location == CardController.Location.PLAYER)
        {
            CardCont.transform.Rotate(Vector3.right, 180);
            CardCont.RankLabel.gameObject.SetActive(true);
        }

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
            CardCont.transform.position = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];
            CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]++;
            TokenCont.RemoveMistake();
        }
        else
        {
            CardCont.location = CardController.Location.BOARD;
            CardCont.transform.position = CardCont.ButtonCont.PlayArea[(int)CardCont.color][(int)CardCont.rank];
            CardCont.ButtonCont.PlayIdx[(int)CardCont.color]++;

            if (CardCont.rank == CardController.Rank.FIVE)
            {
                TokenCont.AddHint();
            }
        }

        /// Log move in scroll view
        if (IsPlayerTurn)
        {
            if (ValidMove)
            {
                MovesLog.text = ("You played your " + CardCont.getColorName() +
                    CardCont.RankLabel.text + " card.\n\n") + MovesLog.text;
            }
            else
            {
                MovesLog.text = ("You attempted to play your " + CardCont.getColorName() +
                    CardCont.RankLabel.text + " card, but it was discarded.\n\n") + MovesLog.text;
            }
        }
        else
        {
            if (ValidMove)
            {
                MovesLog.text = ("Your oponent played their " + CardCont.getColorName() +
                    CardCont.RankLabel.text + " card.\n\n") + MovesLog.text;
            }
            else
            {
                MovesLog.text = ("Your oponent attempted to play their " + CardCont.getColorName() +
                    CardCont.RankLabel.text + " card, but it was discarded.\n\n") + MovesLog.text;
            }
        }

        NewTurn();
    }

    public void Discard()
    {
        if (CardCont.location == CardController.Location.PLAYER)
        {
            CardCont.transform.Rotate(Vector3.right, 180);
            CardCont.RankLabel.gameObject.SetActive(true);
        }

        CardCont.ButtonCont.Selected.transform.position = CardCont.OffScreen;
        gameObject.transform.position = CardCont.OffScreen;
        CardCont.ButtonCont.DealOne(CardCont.transform.position, true);

        CardCont.location = CardController.Location.TRASH;
        CardCont.transform.position = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];
        CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]++;

        TokenCont.AddHint();

        /// Log move in scroll view
        if (IsPlayerTurn)
        {
            MovesLog.text = ("You discarded your " + CardCont.getColorName() +
                CardCont.RankLabel.text + " card.\n\n") + MovesLog.text;
        }
        else
        {
            MovesLog.text = ("Your oponent discarded their " + CardCont.getColorName() +
                CardCont.RankLabel.text + " card.\n\n") + MovesLog.text;
        }

        NewTurn();
    }

    public void NewTurn()
    {
        IsPlayerTurn = !IsPlayerTurn;
        if (IsPlayerTurn)
        {
            Turn.text = "Your Turn";
        }
        else
        {
            Turn.text = "Computer's Turn";
        }
    }
}
