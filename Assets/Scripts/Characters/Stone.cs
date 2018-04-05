using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour 
{
    private GameObject player;

    private void Awake()
    {
        player = LevelDirector.Instance.PlayerOBJ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelDirector.Instance.InitFxFeather(transform.position);
            gameObject.SetActive(false);
            player.GetComponent<PlayerController>().MyState.IsLie = true;
            player.GetComponent<PlayerController>().MyState.IsOnGround = false;
        }
    }
}
