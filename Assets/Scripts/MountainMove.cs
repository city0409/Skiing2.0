using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainMove : MonoBehaviour
{
    private Material mat;
    private Renderer rend;
    private PlayerMotor Player;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        mat = rend.material;
        Player = LevelDirector.Instance.Player.GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        mat.mainTextureOffset += new Vector2(Time.deltaTime * Player.Speed / 40, 0f);
    }
}
