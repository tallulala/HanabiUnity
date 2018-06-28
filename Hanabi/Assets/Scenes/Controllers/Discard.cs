using UnityEngine;
using System.Collections;

public class Discard : MonoBehaviour
{

    public PlayOptionsController playOpt;

    public void OnMouseUpAsButton()
    {
        playOpt.YourMove(3);
    }
}