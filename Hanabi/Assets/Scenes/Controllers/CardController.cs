using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Connected to the Card prefab
/// </summary>
public class CardController : MonoBehaviour
{
    public ButtonController ButtonCont;

    public GameObject EyeTrackerDot;

    public GameObject Front;
    public GameObject HintBox;
    public GameObject Selected;

    public Material CardColor;
    public Material HintedColor;
    public TextMesh RankLabel;
    public TextMesh HintedRank;
    public GameObject MoveCardOptions;
    public GameObject HintOptions;

    public Material white;
    public Material blue;
    public Material yellow;
    public Material red;
    public Material green;
    public Material grey;

    public Vector3 OffScreen = new Vector3(6000000f, 0f, 6000000f);

    public int idx;

    public Color color;
    public Rank rank;
    public Location location;

    /// <summary>
    /// Location of a card on the board
    /// </summary>
    public enum Location
    {
        PLAYER, COMPUTER, BOARD, DECK, TRASH
    };

    /// <summary>
    /// Rank of any given card
    /// </summary>
    public enum Rank : int
    {
        ONE, TWO, THREE, FOUR, FIVE
    };

    /// <summary>
    /// Color of any given card
    /// </summary>
    public enum Color
    {
        WHITE, BLUE, YELLOW, RED, GREEN
    };

    /// <summary>
    /// Initializes variables
    /// </summary>
    public void Start()
    {
        HintBox.SetActive(false);
        HintedColor = grey;
        HintedRank.text = "";
    }

    /// <summary>
    /// Sets the color of a card to the specified color
    /// </summary>
    /// <param name="toColor">The color that the card should have, based on the enum Color</param>
    public void SetColor(int toColor)
    {
        color = (Color)toColor;

        switch (color)
        {
            case Color.WHITE:
                Front.GetComponent<MeshRenderer>().material = white;
                CardColor = white;
                break;
            case Color.BLUE:
                Front.GetComponent<MeshRenderer>().material = blue;
                CardColor = blue;
                break;
            case Color.YELLOW:
                Front.GetComponent<MeshRenderer>().material = yellow;
                CardColor = yellow;
                break;
            case Color.RED:
                Front.GetComponent<MeshRenderer>().material = red;
                CardColor = red;
                break;
            case Color.GREEN:
                Front.GetComponent<MeshRenderer>().material = green;
                CardColor = green;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Sets the rank of a card with the specified rank
    /// </summary>
    /// <param name="toRank">The rank that the card should have, based on the enum Rank</param>
    public void SetRank(int toRank)
    {
        rank = (Rank)toRank;

        switch (rank)
        {
            case Rank.ONE:
                RankLabel.text = "1";
                break;
            case Rank.TWO:
                RankLabel.text = "2";
                break;
            case Rank.THREE:
                RankLabel.text = "3";
                break;
            case Rank.FOUR:
                RankLabel.text = "4";
                break;
            case Rank.FIVE:
                RankLabel.text = "5";
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Gets the name of the color of a card, based on the enum Color
    /// </summary>
    /// <returns>The string version of the color name</returns>
    public string GetColorName()
    {
        switch (color)
        {
            case Color.WHITE:
                return "white";
            case Color.BLUE:
                return "blue";
            case Color.YELLOW:
                return "yellow";
            case Color.RED:
                return "red";
            case Color.GREEN:
                return "green";
            default:
                return "";
        }
    }

    /// <summary>
    /// When card a card in either the player's or the AI's hand is clicked, 
    /// it is highlighted in red and the corresponding PlayOptions Menu pops up either above or below it
    /// </summary>
    public void OnMouseUpAsButton()
    {
        Vector3 CardPos = gameObject.transform.position;
        Vector3 MenuPos;

        if (location == Location.PLAYER)
        {
            Selected.transform.position = new Vector3(CardPos.x, (CardPos.y - 1), CardPos.z);
            Debug.Log("Selected");

            MenuPos = new Vector3(CardPos.x, (CardPos.y + 1), (CardPos.z + 900));
            MoveCardOptions.transform.position = MenuPos;
            MoveCardOptions.GetComponent<PlayOptionsController>().CardCont = this;
            HintOptions.transform.position = OffScreen;
        }
        else if (location == Location.COMPUTER)
        {
            Selected.transform.position = new Vector3(CardPos.x, (CardPos.y - 1), CardPos.z);
            Debug.Log("Selected");

            MenuPos = new Vector3(CardPos.x, (CardPos.y + 1), (CardPos.z - 900));
            HintOptions.transform.position = MenuPos;
            HintOptions.GetComponent<PlayOptionsController>().CardCont = this;
            MoveCardOptions.transform.position = OffScreen;
        }
    }
}