using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class CardController : MonoBehaviour
{
    public GameObject Card;
    public GameObject Front;
    public GameObject HintBox;
    public GameObject Selected;

    public ButtonController ButtonCont;

    public Vector3 OffScreen = new Vector3(6000000f, 0f, 6000000f);

    public GameObject PlayerCardMenu;
    public GameObject ComputerCardMenu;

    public TextMesh RankLabel;
    public TextMesh HintedRank;

    public Material CardColor;
    public Material HintedColor;

    public Material white;
    public Material blue;
    public Material yellow;
    public Material red;
    public Material green;
    public Material grey;

    public int idx;

    public Color color;
    public Rank rank;

    public Location location;

    public enum Location 
    {
        PLAYER, COMPUTER, BOARD, DECK, TRASH
    };

    public enum Rank : int
    {
        ONE, TWO, THREE, FOUR, FIVE
    };

    public enum Color
    {
        WHITE, BLUE, YELLOW, RED, GREEN
    };

    public void Start()
    {
        HintBox.SetActive(false);
        HintedColor = grey;
        HintedRank.text = "";
    }

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

    public string getColorName()
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

    public void OnMouseUpAsButton()
    {
        Vector3 CardPos = gameObject.transform.position;
        Vector3 MenuPos;
        Debug.Log("Click");

        if (location == Location.PLAYER)
        {
            Selected.transform.position = new Vector3(CardPos.x, (CardPos.y - 1), CardPos.z);
            Debug.Log("Selected");

            MenuPos = new Vector3(CardPos.x, (CardPos.y + 1), (CardPos.z + 400));
            PlayerCardMenu.transform.position = MenuPos;
            PlayerCardMenu.GetComponent<PlayOptionsController>().CardCont = this;
            ComputerCardMenu.transform.position = OffScreen;
        }
        else if (location == Location.COMPUTER)
        {
            Selected.transform.position = new Vector3(CardPos.x, (CardPos.y - 1), CardPos.z);
            Debug.Log("Selected");

            MenuPos = new Vector3(CardPos.x, (CardPos.y + 1), (CardPos.z - 400));
            ComputerCardMenu.transform.position = MenuPos;
            ComputerCardMenu.GetComponent<PlayOptionsController>().CardCont = this;
            PlayerCardMenu.transform.position = OffScreen;
        }
    }
}