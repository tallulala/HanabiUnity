using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayOptionsController : MonoBehaviour
{
    public bool IsPlayerTurn;

    public CardController CardCont;
    public ButtonController ButtonCont;

    public GameObject HintBox;

    public int PlayAreaIndex1 = 0;
    public int PlayAreaIndex2 = 0;
    public int DiscardAreaIdx1 = 0;
    public int DiscardAreaIdx2 = 0;

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

    public void Start()
    {

    }

    public void YourMove(int opt)
    {
        if (IsPlayerTurn)
        {
            player = "The Computer ";
        } else
        {
            player = "You ";
        }

        MoveOption = (Options) opt;

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
        // decrement hint tokens

        CardCont.HintBox.SetActive(true);
        CardCont.HintedRank.text = CardCont.RankLabel.text;

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
        // decrement hint tokens

        CardCont.HintBox.SetActive(true);
        CardCont.HintBox.GetComponent<MeshRenderer>().material = CardCont.CardColor;

        if (IsPlayerTurn)
        {
            MovesLog.text = ("You hinted your oponent's " + CardCont.getColorName() + " cards.\n\n") + MovesLog.text;
        } else
        {
            MovesLog.text = ("Your oponent hinted your " + CardCont.getColorName() + " cards.\n\n") + MovesLog.text;
        }

        NewTurn();
    }

    public void PlayCard()
    {

        Debug.Log("Card Played!");

        bool ValidMove = true;
        // Check if valid move
        // if yes, move to correct spot in play field
        // if card has rank 5 increment hint tokens
        // if no move to correct spot in discard pile
        // increment hint tokens ?
        // decrement mistakes counter

        // disable selection
        CardCont.ButtonCont.Selected.transform.position = CardCont.OffScreen;

        // disable menu
        gameObject.transform.position = CardCont.OffScreen;

        // deal new card
        CardCont.ButtonCont.DealOne(CardCont.transform.position, true);

        if((int) CardCont.rank != CardCont.ButtonCont.CurrentPlayIdx[(int)CardCont.color])
        {
            ValidMove = false;
        }

        if (!ValidMove)
        {
            CardCont.location = CardController.Location.TRASH;

            CardCont.transform.position = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.CurrentDiscardIdx[(int)CardCont.color]];

            CardCont.ButtonCont.CurrentDiscardIdx[(int)CardCont.color]++;
        } else
        {
            CardCont.location = CardController.Location.BOARD;
            CardCont.transform.position = CardCont.ButtonCont.PlayArea[(int) CardCont.color][(int) CardCont.rank];
            CardCont.ButtonCont.CurrentPlayIdx[(int)CardCont.color]++;
        }


        if (IsPlayerTurn) {
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
        } else
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
        Debug.Log("Card discarded!");

        // disable selection
        CardCont.ButtonCont.Selected.transform.position = CardCont.OffScreen;

        // disable menu
        gameObject.transform.position = CardCont.OffScreen;

        // deal new card
        CardCont.ButtonCont.DealOne(CardCont.transform.position, true);

        // return hint

        CardCont.location = CardController.Location.TRASH;

        CardCont.transform.position = CardCont.ButtonCont.DiscardArea[(int) CardCont.color][CardCont.ButtonCont.CurrentDiscardIdx[(int) CardCont.color]];

        CardCont.ButtonCont.CurrentDiscardIdx[(int) CardCont.color]++;
      
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
