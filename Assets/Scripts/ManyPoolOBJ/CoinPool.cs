using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : Singleton<CoinPool>
{
    private GameObject coinPrefab;

    private int coinAmount = 30;
    private List<GameObject> coinPool = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        coinPrefab = Resources.Load<GameObject>("Prefabs/Coins");
    }
    private void Start()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            GameObject obj1 = Instantiate(coinPrefab, transform);
            obj1.SetActive(false);
            coinPool.Add(obj1);
        }
    }
    public GameObject InitCoin()
    {
        for (int i = 0; i < coinPool.Count; i++)
        {
            if (coinPool[i].activeSelf == false)
            {
                coinPool[i].SetActive(true);
                return coinPool[i];
            }
        }

        GameObject obj1 = Instantiate(coinPrefab, transform);
        coinPool.Add(obj1);
        return obj1;
    }
}
