using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    //public bool my_left_btn_clicked;

    //protected override void Awake()
    //{
    //    base.Awake();
    //}

    private void Update()
    {

    }

    public void Resurgence()
    {
        GameManager.Instance.PlayerResurgenceEvent();
    }
}
