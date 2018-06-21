using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public GameObject Card;

    Color color;
    Rank rank;

    enum Rank : int
    {
        one = 1,
        two = 2,
        three = 3,
        four = 4,
        five = 5
    };

    enum Color
    {
        white, blue, yellow, red, green
    };

    private void Start()
    {
        Card = gameObject;
        Card.SetActive(true);
    }

    public void setColor(int toColor)
    {
        color = (Color) toColor;
    }

    public void setRank(int toRank)
    {
        rank = (Rank) toRank;
    }
}