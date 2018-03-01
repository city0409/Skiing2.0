using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform bgText;

    private Vector3 tempPos;
    [SerializeField]
    private LayerMask mask;

    private void Awake () 
	{
    }
	
	private void LateUpdate () 
	{
        bgText.transform.position = GetComponent<Transform>().position + new Vector3(0f,0f,20f);
        tempPos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, 30f, mask);
        if (hit==true )
        {
            tempPos = Vector2.Lerp(tempPos, hit.point + new Vector2(13, -5), Time.deltaTime * 5);
            tempPos.z = -10;
            transform.position = tempPos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player !=null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(player.position, player.position + new Vector3(0, -30, 0));
        }
    } 
}
