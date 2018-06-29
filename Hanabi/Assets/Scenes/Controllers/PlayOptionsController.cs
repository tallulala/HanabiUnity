using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayOptionsController : MonoBehaviour
{
    public bool IsPlayerTurn;

    public ScrollRect Moves;

    public TextMesh Rank;
    public TextMesh Color;
    public TextMesh Play;
    public TextMesh Disc;

    public Text Turn;

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

        CardController CurrentCard = GetComponent<CardController>();
        CurrentCard.HintBox.transform.position = new Vector3(-0.5f, 1, 0.5f);
        CurrentCard.HintedRank.text = CurrentCard.RankLabel.text;

        //NewTurn();
    }

    public void HintColor()
    {
        // decrement hint tokens

        CardController CurrentCard = GetComponent<CardController>();
        CurrentCard.HintBox.transform.position = new Vector3(-0.5f, 1, 0.5f);
        CurrentCard.HintBox.GetComponent<MeshRenderer>().material = CurrentCard.CardColor;

        //NewTurn();
    }

    public void PlayCard()
    {
        // Check if valid move
        // if yes, move to correct spot in play field
        // if card has rank 5 increment hint tokens
        // if no move to correct spot in discard pile
        // increment hint tokens
        // decrement mistakes counter

        CardController CurrentCard = GetComponent<CardController>();

        /* if ()
        {
            CurrentCard.location = CardController.Location.BOARD;

        } else {
            CurrentCard.location = CardController.Location.TRASH;
        } */ 


        Debug.Log("Card Played!");

        //NewTurn();
    }

    public void Discard()
    {
        // move to correct place in discard field
        // increment hint tokens
        CardController CurrentCard = GetComponent<CardController>();
        CurrentCard.location = CardController.Location.TRASH;

        Debug.Log("Card discarded!");

        //NewTurn();
    }

    public void NewTurn(string move)
    {
        // add move to moves log
        // Moves.content.GetComponent<Text> = player + move + card;

        // IsPlayerTurn = !IsPlayerTurn;

    }
}
