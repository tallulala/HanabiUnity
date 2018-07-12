﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Connected to the background of the game
/// TODO: In GameOverMethod() -> implement "last round of turns" when deck is empty?
/// TODO: InvalidMove() function?
/// </summary>
public class TokenController : MonoBehaviour
{
    public ButtonController ButtonCont;

    public GameObject Token;
    public GameObject[] Hints;
    public GameObject[] Mistakes;
    public Vector3[] HintTokenPos;
    public Vector3[] MistakeTokenPos;
    public Material HintMat;
    public Material MistakeMat;
    public Material Grey;
    public int HintIdx = 7;
    public int MistakeIdx = 2;

    /// <summary>
    /// Initialize most of the variables
    /// Instantiate hint tokens and mistake tokens dynamically
    /// </summary>
    private void Start()
    {
        // Instantiates HintTokens
        HintTokenPos = new Vector3[8];
        Hints = new GameObject[8];
        for (int i = 0; i < 8; i++)
        {
            HintTokenPos[i] = new Vector3((-300 + (i * 600)), 10, 1700);
            Hints[i] = Instantiate(Token, HintTokenPos[i], Quaternion.identity);
            Hints[i].GetComponent<MeshRenderer>().material = HintMat;
        }

        // Instantiates MistakeTokens
        MistakeTokenPos = new Vector3[3];
        Mistakes = new GameObject[3];
        for (int j = 0; j < 3; j++)
        {
            MistakeTokenPos[j] = new Vector3((5000 + (j * 600)), 10, 1700);
            Mistakes[j] = Instantiate(Token, MistakeTokenPos[j], Quaternion.identity);
            Mistakes[j].GetComponent<MeshRenderer>().material = MistakeMat;
        }
    }

    /// <summary>
    /// Decrements hint tokens by one
    /// Done when hint is given
    /// </summary>
    public void RemoveHint()
    {
        Debug.Log("HintCounter is " + HintIdx);
        Debug.Log("Hints length: " + Hints.Length);
        
        if (HintIdx == 0)
        {
            // INVALID MOVE
            // TODO: show pop up and ask player to try again

            Debug.Log("INVALID MOVE");
            return;
        }
        Hints[HintIdx].GetComponent<MeshRenderer>().material = Grey;
        HintIdx--;

        Debug.Log("Hint Given. " + "HintCounter is " + HintIdx);
    }

    /// <summary>
    /// Increments hint tokens by one
    /// Possible when there are less than 8 hint tokens available
    /// Done when card is discarded or a five is played correctly
    /// </summary>
    public void AddHint()
    {
        if (HintIdx < 7)
        {
            HintIdx++;
            Hints[HintIdx].GetComponent<MeshRenderer>().material = HintMat;

            Debug.Log("Hint Regained. " + "HintCounter is " + HintIdx);
        }
    }

    /// <summary>
    /// Decrements mistake tokens
    /// Done when card is incorrectly played
    /// Game is over on third mistake
    /// </summary>
    public void RemoveMistake()
    {
        Mistakes[MistakeIdx].GetComponent<MeshRenderer>().material = Grey;
        MistakeIdx--;
        if (MistakeIdx < 0)
        {
            GameOver();
        }

        Debug.Log("Mistake made. " + "MistakeCounter is " + MistakeIdx);
    }

    /// <summary>
    /// Ends game
    /// Happens when three mistakes are made 
    /// OR when there are no more cards in the deck
    /// </summary>
    public void GameOver()
    {
        //END GAME
        // TODO: show pop up of score and end game
        // Restart/start new game option?

        foreach(GameObject card in ButtonCont.Deck)
        {
            card.SetActive(false);
        }

        foreach (GameObject token in Mistakes)
        {
            token.SetActive(false);
        }

        foreach (GameObject token in Hints)
        {
            token.SetActive(false);
        }

        ButtonCont.PlayerCardMenu.GetComponent<PlayOptionsController>().ScoreText.transform.position = new Vector3(3124, -1250, 0);


        Debug.Log("GAME OVER");
        return;
    }
}
