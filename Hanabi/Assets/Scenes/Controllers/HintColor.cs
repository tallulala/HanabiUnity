using UnityEngine;
using System.Collections;

public class HintColor : MonoBehaviour
{

    public PlayOptionsController playOpt;

    public void OnMouseUpAsButton()
    {
        playOpt.YourMove(1);
    }

}