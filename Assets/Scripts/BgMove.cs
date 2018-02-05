using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour 
{
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private Transform Pos1;
    [SerializeField]
    private Transform Pos2;
    private Transform PosTarget;
    //private Transform PosCurrent;
    
    private Vector3 dis;
    [SerializeField]
    private Transform BgPos1;
    [SerializeField]
    private Transform BgPos2;

    //private Renderer rend;
    //private Material mat;

    private int index;
    private void Awake()
    {
        
        dis = Pos2.position - Pos1.position;
        //PosCurrent = Pos1;
        PosTarget = Pos2;
    }

    void Update()
    {
        //mat.SetTextureOffset("_MainTex", new Vector3(0, Time.time / 8)); 
        if (Player.position .x >= PosTarget.position.x)
        {
            index++;
            if (index % 2 == 0) {
                
                BgPos1.position = BgPos2.position + dis;
            }
            else {
                BgPos2.position = BgPos1.position + dis;
            }
            print("change");
            
            PosTarget.position = PosTarget.position + dis;
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (Pos2 != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(Pos2.position, Pos2.position + new Vector3(0, 30, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            BgPos2.position = BgPos1.position + dis;
            Debug.Log("okkkk");
        }
    }

}
