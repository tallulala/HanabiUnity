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

    private void Start()
    {
        HintTokenPos = new Vector3[8];
        MistakeTokenPos = new Vector3[3];

        Hints = new GameObject[8];
        Mistakes = new GameObject[3];

        for (int i = 0; i < 8; i++)
        {
            HintTokenPos[i] = new Vector3((-300 + (i * 600)), 10, 1700);
            Hints[i] = Instantiate(Token, HintTokenPos[i], Quaternion.identity);
            Hints[i].GetComponent<MeshRenderer>().material = HintMat;
        }

        for (int j = 0; j < 3; j++)
        {
            MistakeTokenPos[j] = new Vector3((5000 + (j * 600)), 10, 1700);
            Mistakes[j] = Instantiate(Token, MistakeTokenPos[j], Quaternion.identity);
            Mistakes[j].GetComponent<MeshRenderer>().material = MistakeMat;
        }
    }

    public void RemoveHint()
    {
        Debug.Log("HintCounter is " + HintIdx);
        Debug.Log("Hints length: " + Hints.Length);
        
        if (HintIdx == 0)
        {
            /// INVALID MOVE
            Debug.Log("INVALID MOVE");
            return;
        }
        Hints[HintIdx].GetComponent<MeshRenderer>().material = Grey;
        HintIdx--;

        Debug.Log("Hint Given. " + "HintCounter is " + HintIdx);
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
            GameOver();
        }

        Debug.Log("Mistake made. " + "MistakeCounter is " + MistakeIdx);
    }

    public void GameOver()
    {
        //END GAME
        Debug.Log("GAME OVER");
        return;
    }
}
