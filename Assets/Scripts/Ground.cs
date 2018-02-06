using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour 
{
    [SerializeField]
    private EdgeCollider2D edgeCollider2D;

    private Vector2[] poss = new Vector2[500];
    [ContextMenu("Do")]
	private void Start() 
	{
		float x = 0;
        float y = 0;
        float z = 0;
        float w = 0;

        for (int i = 0; i < poss .Length ; i++)
        {
            z += 0.1f;
            x += 0.3f;
            y -= 0.1f;
            w += 0.1f;

            float sample = Mathf.PerlinNoise(z,w);

            poss[i] = new Vector3(x, (sample*4 + y));

        }
        edgeCollider2D.points = poss;

    }

    private void OnDrawGizmos () 
	{
        //Gizmos.DrawLine()
	}
}
