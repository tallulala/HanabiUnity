using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOptionsController : MonoBehaviour
{
    bool IsPlayerTurn = true;



    public void OnClick()
    {
        switch (switch_on)
        {
            default:
        }
    }

    public void HintColor()
    {
        // Highlight Card in certain color
        // decrement hint tokens

        //NewTurn();
    }

    public void HintRank()
    {
        // Highlight rank
        // decrement hint tokens

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

        //NewTurn();
    }

    public void Discard()
    {
        // move to correct place in discard field
        // increment hint tokens

        //NewTurn();
    }

    public void NewTurn(string move)
    {
        // add move to moves log
        // change turn
    }
}
