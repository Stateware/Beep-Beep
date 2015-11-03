using UnityEngine;
using System.Collections;

public class ErrorView : MonoBehaviour {

    private bool displayGUI = false;
    private string errorText = "";

    void OnGUI()
    {
        if (this.displayGUI)
            GUI.TextArea(new Rect(10, 550, 200, 100), this.errorText, 200);
    }

    public void setErrorText(string newText)
    {
        this.errorText = newText;
    }

    public void appendErrorText(string addToEndText)
    {
        this.errorText = this.errorText + "\n" + addToEndText;
    }

    public void setDisplayGui(bool display)
    {
        this.displayGUI = display;
    }
}
