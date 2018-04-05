using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePool : Singleton<SlidePool>
{
    private GameObject slidePrefab;

    private int slideAmount = 5;
    private List<GameObject> slidePool = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        slidePrefab = Resources.Load<GameObject>("Prefabs/SlideFx");

    }
    private void Start()
    {
        for (int i = 0; i < slideAmount; i++)
        {
            GameObject obj1 = Instantiate(slidePrefab, transform);
            obj1.SetActive(false);
            slidePool.Add(obj1);
        }
    }
    public GameObject InitSlide()
    {
        for (int i = 0; i < slidePool.Count; i++)
        {
            if (slidePool[i].activeSelf == false)
            {
                slidePool[i].SetActive(true);
                return slidePool[i];
            }
        }

        GameObject obj1 = Instantiate(slidePrefab, transform);
        slidePool.Add(obj1);
        return obj1;
    }
}
