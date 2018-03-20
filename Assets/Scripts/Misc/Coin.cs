using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    private CircleCollider2D collCoin;
    public AudioSource audioCoin;
    private Renderer rend;
    private void Awake()
    {
        collCoin = GetComponent<CircleCollider2D>();
        rend = GetComponent<Renderer>();
        audioCoin = GetComponent<AudioSource>();

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject .CompareTag  ("Player"))
        {
            collCoin.enabled = false;
            audioCoin.Play();
            rend.enabled = false;
            LevelDirector.Instance.ScoreAward += 10;

            Destroy(this.gameObject, audioCoin.clip.length);

        }
    }
}
