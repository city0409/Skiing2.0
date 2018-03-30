using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private int cur_click_sum = 0;
    private float timerFull = 2;
    private float timer = 0;
    //public bool my_left_btn_clicked;

    private Stack<float> times = new Stack<float>(); 
    
    protected override void Awake()
    {
        base.Awake();
        timer = timerFull;
    }

    private void Update()
    {
        //timer -= Time.deltaTime;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    cur_click_sum += 1;
        //    if (cur_click_sum >= 3)
        //    {
        //        print("wwwwwwwwwwwww");
        //        //GameManager.Instance.PlayerResurgenceEvent();
        //        cur_click_sum = 0;
        //    }
        //}
        //if (timer <= 0)
        //{
        //    timer = timerFull;
        //    cur_click_sum = 0;
        //}

        //if (Input .GetKeyDown(KeyCode.E))
        //{
        //    GameManager.Instance.PlayerDeadEvent();
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    times.Push(Time.realtimeSinceStartup);

        //    if (times.Count == 3)
        //    {
        //        //times.Pop();

        //        if ((Time.realtimeSinceStartup - times.Peek()) < 2f)
        //        {
        //            times.Clear();
        //            print("hhh");
        //        }
        //        if (times.Count > 0)
        //            times.Pop();
        //    }
        //}

    }

    public void DoSome()
    {
        print("DOSome");
    }
}
