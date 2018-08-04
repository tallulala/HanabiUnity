using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    public ButtonController ButtonCont;
    public bool EnterPressed = false;

    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        var SubE = new InputField.SubmitEvent();
        SubE.AddListener(SubmitName);
        input.onEndEdit = SubE;
       // SubmitName(input);
    }

    private void SubmitName(string name)
    {
        ButtonCont.playerName = name;
    }
}