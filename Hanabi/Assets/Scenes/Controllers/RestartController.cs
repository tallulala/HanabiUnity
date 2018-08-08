using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartController : MonoBehaviour {

    public ButtonController ButtonCont;

    public void OnClick()
    {
        Debug.Log("Restartcont click");

        ButtonCont.Start();
        ButtonCont.OnClick();

        foreach (GameObject card in ButtonCont.Deck)
        {
            card.SetActive(true);
            card.GetComponent<CardController>().HintBox.SetActive(false);
            card.transform.position = ButtonCont.DeckPos;
        }

        //ButtonCont.Deck = ButtonCont.Shuffle(ButtonCont.Deck);
        ButtonCont.Background.GetComponent<TokenController>().ResetTokens();
        ButtonCont.PlayerCardMenu.GetComponent<PlayOptionsController>().ScoreText.transform.position = new Vector3(-3000, 2, 1500);
        ButtonCont.PlayerCardMenu.GetComponent<PlayOptionsController>().MovesLog.text = "";
    
    }
}
