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

    public Vector3[][] PlayArea;
    public Vector3[][] DiscardArea;

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
        PlayArea = new Vector3[5][];
        DiscardArea = new Vector3[5][];

        foreach(Vector3[] i in PlayArea)
        {
            foreach(Vector3 j in PlayArea[10])
            {
            }
        }
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
        // Highlight rank
        // decrement hint tokens

        Debug.Log("Rank hinted!");

        //NewTurn();
    }

    public void HintColor()
    {
        // Highlight Card in certain color
        // decrement hint tokens

        Debug.Log("Color hinted!");

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

        Debug.Log("Card Played!");

        //NewTurn();
    }

    public void Discard()
    {
        // move to correct place in discard field
        // increment hint tokens

        Debug.Log("Card discarded!");

        //NewTurn();
    }

    public void NewTurn(string move)
    {
        // add move to moves log
        //Moves.content.GetComponent<Text> = player + move + card;

        IsPlayerTurn = !IsPlayerTurn;

    }
}
