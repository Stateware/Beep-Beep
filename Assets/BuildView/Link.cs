using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour
{
    private int _numberOfLanes;
    private bool _isTwoWay;

    void Start()
    {
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
