using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardController : MonoBehaviour
{
    public GameObject Card;
    public GameObject front;

    public TextMesh label;

    public Material white;
    public Material blue;
    public Material yellow;
    public Material red;
    public Material green;

    public bool inHand;

    Color color;
    Rank rank;

    enum Rank : int
    {
        one,
        two,
        three,
        four,
        five
    };

    enum Color
    {
        white, blue, yellow, red, green
    };

    public void setColor(int toColor)
    {
        color = (Color) toColor;

        switch (color)
        {
            case Color.white:
                front.GetComponent<MeshRenderer>().material = white;
                break;
            case Color.blue:
                front.GetComponent<MeshRenderer>().material = blue;
                break;
            case Color.yellow:
                front.GetComponent<MeshRenderer>().material = yellow;
                break;
            case Color.red:
                front.GetComponent<MeshRenderer>().material = red;
                break;
            case Color.green:
                front.GetComponent<MeshRenderer>().material = green;
                break;
            default:
                break;
        }
    }

    public void setRank(int toRank)
    {
        rank = (Rank) toRank;

        switch (rank)
        {
            case Rank.one:
                label.text = "1";
                break;
            case Rank.two:
                label.text = "2";
                break;
            case Rank.three:
                label.text = "3";
                break;
            case Rank.four:
                label.text = "4";
                break;
            case Rank.five:
                label.text = "5";
                break;
            default:
                break;
        }
    }
}