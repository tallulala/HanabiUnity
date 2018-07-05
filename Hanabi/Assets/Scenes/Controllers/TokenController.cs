using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenController : MonoBehaviour {

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


    // Use this for initialization
    void Start () {
        /// Hint and Mistake tokens
        Hints = new GameObject[8];
        HintTokenPos = new Vector3[8];
        Mistakes = new GameObject[3];
        MistakeTokenPos = new Vector3[3];

        /// Mistake tokens
        for (int j = 0; j < 3; j++)
        {
            MistakeTokenPos[j] = new Vector3((1745 + j * 250), 30, 1050);
            Mistakes[j] = Instantiate(Token, MistakeTokenPos[j], Quaternion.identity);
            Mistakes[j].GetComponent<MeshRenderer>().material = MistakeMat;
        }

        /// Hint tokens
        for (int i = 0; i < 8; i++)
        {
            HintTokenPos[i] = new Vector3((-600 + i * 250), 30, 1050);
            Hints[i] = Instantiate(Token, HintTokenPos[i], Quaternion.identity);
            Hints[i].GetComponent<MeshRenderer>().material = HintMat;
        }
    }

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
