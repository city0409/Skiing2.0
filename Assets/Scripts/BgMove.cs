using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform PosTarget;
    [SerializeField] private Transform BgPos1;
    [SerializeField] private Transform BgPos2;
    private Vector3 dis;
    private int index = 0;

    private void Awake()
    {
        dis = BgPos2.position - BgPos1.position;
    }

    void Update()
    {
        if (Player.position.x >= PosTarget.position.x)
        {
            index++;
            if (index % 2 == 0)
            {
                BgPos1.position = BgPos2.position + dis;
            }
            else
            {
                BgPos2.position = BgPos1.position + dis;
            }
            PosTarget.position += dis;
        }

    }
    //private void OnDrawGizmosSelected()
    //{
    //    if (PosTarget != null)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine(PosTarget.position, PosTarget.position + new Vector3(0, 30, 0));
    //    }
    //}
}
