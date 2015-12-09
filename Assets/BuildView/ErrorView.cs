// Copyright (c) 2015 Stateware Team -- Licensed GPL v3
// File Name:        ErrorView.cs
// Description:      Shows the errors that the compiler generates 
// Dependencies:     N/A
// Additional Notes: N/A

using UnityEngine;
using System;

public class ErrorView : MonoBehaviour
{
    private bool _displayGUI = false;
    private string _errorText = "";
    private int _numError = 1;

    // Description: Called by the unity API
    // PRE:         N/A
    // POST:        Creates the error text view area
    void OnGUI()
    {
        if (this._displayGUI)
        {
            GUI.TextArea(new Rect(10, 550, 500, 100), this._errorText, 200);
        }
    }

    // Description: Setter for the area text
    // PRE:         N/A
    // POST:        Error text will be completely re-written
    public void SetErrorText(string newText)
    {
        this._errorText = newText;
    }

    // Description: Appends text error to the current error list, so it just shows them in order, top
    //              being the newest, consider changing to an arraylist later
    // PRE:         N/A
    // POST:        AddToEndText is now at the beginning of the errorText, and a new line is added
    public void AppendErrorText(string addToEndText)
    {
        this._errorText = String.Format("[{0}] {1}\n{2}", this._numError, addToEndText, this._errorText);
        _numError++;

    }

    // Desription:  Setter to display the GUI (originally it is invisible, will be set to true if compiler has errors)
    // PRE:         N/A
    // POST:        Error View GUI will be displayed on the scene
    public void SetDisplayGui(bool display)
    {
        this._displayGUI = display;
    }
}
