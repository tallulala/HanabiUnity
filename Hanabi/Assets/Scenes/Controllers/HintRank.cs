using UnityEngine;
using System.Collections;

public class HintRank : MonoBehaviour
{
    public PlayOptionsController playOpt;

    public void OnMouseUpAsButton()
    {
        playOpt.YourMove(0);
    }
}