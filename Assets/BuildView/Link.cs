// File name:        Link.cs
// Description:      Contains all the setters and getters for gameobjects of type link
// Dependencies:     GameObject - LinkPrefab (Clone)
// Additional Notes: N/A

using UnityEngine;

public class Link : MonoBehaviour
{
    private int _numberOfLanes;

    private bool _isTwoWay;

    // Description: Initializes link and preserves the gameboject between scenes
    // PRE:         
    // POST:
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        this.NumberOfLanes = 1;
        this.IsTwoWay = true;
    }

    // Description: 
    // PRE:         
    // POST:
    public int NumberOfLanes
    {
        get { return _numberOfLanes; }
        set { _numberOfLanes = value; }
    }

    // Description: 
    // PRE:         
    // POST:
    public bool IsTwoWay
    {
        get { return _isTwoWay; }
        set { _isTwoWay = value; }
    }
}
