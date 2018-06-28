using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour
{

    public PlayOptionsController playOpt;

    public void OnMouseUpAsButton()
    {
        playOpt.YourMove(2);
    }

}