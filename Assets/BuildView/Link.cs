using UnityEngine;
using System.Collections;

//File name: Link.cs
//Description: Contains all the setters and getters for gameobjects of type link
//Dependencies: GameObject - LinkPrefab (Clone)
//Additional Notes:

public class Link : MonoBehaviour
{
    private int _numberOfLanes;
    private bool _isTwoWay;

    //initializes link and preserves the gameboject between scenes
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        this.NumberOfLanes = 1;
        this.IsTwoWay = true;
    }

    public int NumberOfLanes
    {
        get { return _numberOfLanes; }
        set { _numberOfLanes = value; }
    }

    public bool IsTwoWay
    {
        get { return _isTwoWay; }
        set { _isTwoWay = value; }
    }
}
