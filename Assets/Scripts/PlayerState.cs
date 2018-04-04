using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerState
{

    private bool isSkinng;
    private bool isRollling;
    private bool isOnGround;
    private bool isRideSnowMan;
    private bool isLie;
    
    public bool IsSkiing { get { return isSkinng; } set { isSkinng = value; } }

    public bool IsRollling { get { return isRollling; } set { isRollling = value; } }

    public bool IsOnGround { get { return isOnGround; } set { isOnGround = value; } }

    public bool IsRideSnowMan { get { return isRideSnowMan; } set { isRideSnowMan = value; } }

    public bool IsLie { get { return isLie; } set { isLie = value; } }
}