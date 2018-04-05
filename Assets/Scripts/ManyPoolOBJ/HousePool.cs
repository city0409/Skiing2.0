using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePool : Singleton<HousePool>
{
    private GameObject house1Prefab;
    private GameObject house2Prefab;

    private int houseAmount = 20;
    private List<GameObject> housePool = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        house1Prefab = Resources.Load<GameObject>("Prefabs/House001");
        house2Prefab = Resources.Load<GameObject>("Prefabs/House002");

    }
    private void Start()
    {
        for (int i = 0; i < houseAmount; i++)
        {
            GameObject obj1 = Instantiate(house1Prefab, transform);
            obj1.SetActive(false);
            housePool.Add(obj1);
        }
    }
    public GameObject InitHouse()
    {
        for (int i = 0; i < housePool.Count; i++)
        {
            if (housePool[i].activeSelf == false)
            {
                housePool[i].SetActive(true);
                return housePool[i];
            }
        }

        GameObject obj1 = Instantiate(house1Prefab, transform);
        housePool.Add(obj1);
        return obj1;
    }
}
