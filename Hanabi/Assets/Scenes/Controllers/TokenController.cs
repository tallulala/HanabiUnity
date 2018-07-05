using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenController : MonoBehaviour
{
    public GameObject Token;

    public Material HintMat;
    public Material MistakeMat;
    public Material Grey;

    public GameObject[] Hints;
    public GameObject[] Mistakes;
    public Vector3[] HintTokenPos;
    public Vector3[] MistakeTokenPos;

    public int HintIdx = 7;
    public int MistakeIdx = 2;

    public void RemoveHint()
    {
        if (HintIdx == 0)
        {
            /// INVALID MOVE
            Debug.Log("INVALID MOVE");
        }
        Hints[HintIdx].GetComponent<MeshRenderer>().material = Grey;
        HintIdx--;

        Debug.Log("Color Hinted. " + "HintCounter is " + HintIdx);
    }

    public void AddHint()
    {
        if (HintIdx < 7)
        {
            HintIdx++;
            Hints[HintIdx].GetComponent<MeshRenderer>().material = HintMat;

            Debug.Log("Hint Regained. " + "HintCounter is " + HintIdx);
        }
    }

    public void RemoveMistake()
    {
        Mistakes[MistakeIdx].GetComponent<MeshRenderer>().material = Grey;
        MistakeIdx--;
        if (MistakeIdx < 0)
        {
            //END GAME
            Debug.Log("GAME OVER");
        }

        Debug.Log("Mistake made. " + "MistakeCounter is " + MistakeIdx);
    }

}
