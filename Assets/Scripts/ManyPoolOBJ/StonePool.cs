using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePool : Singleton<StonePool>
{
    private GameObject stone1Prefab;
    private GameObject stone2Prefab;

    private int stoneAmount = 30;
    private List<GameObject> stonePool = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        stone1Prefab = Resources.Load<GameObject>("Prefabs/Stone1");
        stone2Prefab = Resources.Load<GameObject>("Prefabs/Stone2");

    }
    private void Start()
    {
        for (int i = 0; i < stoneAmount; i++)
        {
            GameObject obj1 = Instantiate(stone1Prefab, transform);
            obj1.SetActive(false);
            stonePool.Add(obj1);
        }
    }
    public GameObject InitStone()
    {
        for (int i = 0; i < stonePool.Count; i++)
        {
            if (stonePool[i].activeSelf == false)
            {
                stonePool[i].SetActive(true);
                return stonePool[i];
            }
        }

        GameObject obj1 = Instantiate(stone1Prefab, transform);
        stonePool.Add(obj1);
        return obj1;
    }
}
