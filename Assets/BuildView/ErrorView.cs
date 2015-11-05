using UnityEngine;
using System.Collections;
using System;

public class ErrorView : MonoBehaviour {

    private bool displayGUI = false;
    private string errorText = "";
    private int numError = 1;

    void OnGUI()
    {
        if (this.displayGUI)
            GUI.TextArea(new Rect(10, 550, 500, 100), this.errorText, 200);
    }

    public void setErrorText(string newText)
    {
        this.errorText = newText;
    }

    public void appendErrorText(string addToEndText)
    {
        this.errorText = String.Format("[{0}] {1}\n{2}", this.numError, addToEndText, this.errorText);
        numError++;

    }

    public void setDisplayGui(bool display)
    {
        this.displayGUI = display;
    }
}
