using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public CardController CardCont;
    public PlayOptionsController PlayCont;

    public void MakeMove(int Move)
    {
        System.Random random = new System.Random();
        Move = random.Next(1, 4);

        switch (Move)
        {
            case 1:
                HintRank();
                break;
            case 2:
                HintColor();
                break;
            case 3:
                PlayCard();
                break;
            case 4:
                Discard();
                break;
            default:
                break;
        }
    }
    public void HintRank()
    {
        GameObject thisCard = PickCard(CardController.Location.PLAYER);

        PlayCont.Background.GetComponent<TokenController>().RemoveHint();

        foreach (GameObject card in CardCont.ButtonCont.Deck)
        {
            if (card.GetComponent<CardController>().location == thisCard.GetComponent<CardController>().location)
            {
                if (card.GetComponent<CardController>().rank == thisCard.GetComponent<CardController>().rank)
                {
                    card.GetComponent<CardController>().HintBox.SetActive(true);
                    card.GetComponent<CardController>().HintedRank.text = CardCont.RankLabel.text;
                }
            }
        }

        PlayCont.MovesLog.text = ("The Computer hinted your " + CardCont.RankLabel.text + " cards.\n\n") + PlayCont.MovesLog.text;

    }

    public void HintColor()
    {
        GameObject thisCard = PickCard(CardController.Location.PLAYER);

        PlayCont.Background.GetComponent<TokenController>().RemoveHint();

        foreach (GameObject card in CardCont.ButtonCont.Deck)
        {
            if (card.GetComponent<CardController>().location == thisCard.GetComponent<CardController>().location)
            {
                if (card.GetComponent<CardController>().CardColor == thisCard.GetComponent<CardController>().CardColor)
                {
                    card.GetComponent<CardController>().HintBox.SetActive(true);
                    card.GetComponent<CardController>().HintBox.GetComponent<MeshRenderer>().material = thisCard.GetComponent<CardController>().CardColor;
                }
            }
        }

        PlayCont.MovesLog.text = ("The Computer hinted your " + CardCont.GetColorName() + " cards.\n\n") + PlayCont.MovesLog.text;
    }

    public void PlayCard()
    {
        GameObject thisCard = PickCard(CardController.Location.COMPUTER);

        bool ValidMove = true;
        Vector3 startPos = thisCard.GetComponent<CardController>().transform.position;
        Vector3 endPos;

        if (thisCard.GetComponent<CardController>().rank == CardController.Rank.FIVE)
        {
            PlayCont.Background.GetComponent<TokenController>().AddHint();
        }

        if ((int)thisCard.GetComponent<CardController>().rank != CardCont.ButtonCont.PlayIdx[(int)CardCont.color])
        {
            ValidMove = false;
        }

        if (!ValidMove)
        {
            thisCard.GetComponent<CardController>().location = CardController.Location.TRASH;

            endPos = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];

            StartCoroutine(CardCont.ButtonCont.Animation(thisCard, startPos, endPos));

            PlayCont.Background.GetComponent<TokenController>().RemoveMistake();
        }
        else
        {
            CardCont.location = CardController.Location.BOARD;

            endPos = CardCont.ButtonCont.PlayArea[(int)CardCont.color][(int)CardCont.rank];

            StartCoroutine(CardCont.ButtonCont.Animation(thisCard, startPos, endPos));

            PlayCont.Score++;
            PlayCont.ScoreText.text = "Score: " + PlayCont.Score + "/25";
        }

        if (ValidMove)
        {
            PlayCont.MovesLog.text = ("The Computer played its " + CardCont.GetColorName() +
                CardCont.RankLabel.text + " card.\n\n") + PlayCont.MovesLog.text;
        }
        else
        {
            PlayCont.MovesLog.text = ("The Computer attempted to play its " + CardCont.GetColorName() +
                CardCont.RankLabel.text + " card, but it was discarded.\n\n") + PlayCont.MovesLog.text;
        }
    }

    public void Discard()
    {
        GameObject thisCard = PickCard(CardController.Location.COMPUTER);

        CardCont.ButtonCont.DealOne(thisCard.transform.position, true);

        thisCard.GetComponent<CardController>().location = CardController.Location.TRASH;
        thisCard.transform.position = CardCont.ButtonCont.DiscardArea[(int)CardCont.color][CardCont.ButtonCont.DiscardIdx[(int)CardCont.color]];
        thisCard.GetComponent<CardController>().ButtonCont.DiscardIdx[(int)CardCont.color]++;

        PlayCont.Background.GetComponent<TokenController>().AddHint();

        PlayCont.MovesLog.text = ("The Computer discarded its " + CardCont.GetColorName() +
                CardCont.RankLabel.text + " card.\n\n") + PlayCont.MovesLog.text;
    }

    public GameObject PickCard(CardController.Location loc)
    {
        GameObject[] cards = new GameObject[5];
        int i = 0;
        foreach (GameObject card in CardCont.ButtonCont.Deck)
        {
            if (card.GetComponent<CardController>().location == loc)
            {
                cards[i] = card;
                i++;
            }
        }

        System.Random random = new System.Random();
        return cards[random.Next(0, 4)];
    }
}