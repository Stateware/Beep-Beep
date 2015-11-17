//File Name:        ErrorView.cs
//Description:      Shows the errors that the compiler generates 
//Dependencies:     None
//Additional Notes: None

using UnityEngine;
using System.Collections;
using System;

public class ErrorView : MonoBehaviour {

    private bool displayGUI = false;
    private string errorText = "";
    private int numError = 1;

    void OnGUI()
    //Desription: Called by the unity API
    //PRE: None
    //POST: Creates the error text view area
    {
        if (this.displayGUI)
            GUI.TextArea(new Rect(10, 550, 500, 100), this.errorText, 200);
    }

    public void setErrorText(string newText)
    //Desription: setter for the area text
    //PRE: none
    //POST: error text will be completely re-written
    {
        this.errorText = newText;
    }

    public void appendErrorText(string addToEndText)
    //Desription: appends text error to the current error list, so it just shows them in order, top
    //             being the newest, consider changing to an arraylist later
    //PRE: -
    //POST: addToEndText is now at the beginning of the errorText, and a new line is added
    {
        this.errorText = String.Format("[{0}] {1}\n{2}", this.numError, addToEndText, this.errorText);
        numError++;

    }

    public void setDisplayGui(bool display)
    //Desription: setter to display the GUI (originaly it is invisible, will be set to true if compiler has errors
    //PRE: -
    //POST: Error View GUI will be displayed on the scene
    {
        this.displayGUI = display;
    }
}
