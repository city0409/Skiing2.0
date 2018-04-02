using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxFeatherPool : Singleton<FxFeatherPool>
{
    private GameObject fxFeatherPrefab;

    private int fxFeatherAmount = 10;
    private List<GameObject> fxFeatherPool = new List<GameObject>();


    private void Awake() 
	{
        base.Awake();
        fxFeatherPrefab = Resources.Load<GameObject>("Prefabs/FxPrefabs/Feather");
    }

    private void Start()
    {
        for (int i = 0; i < fxFeatherAmount; i++)
        {
            GameObject obj1 = Instantiate(fxFeatherPrefab, transform);
            obj1.SetActive(false);
            fxFeatherPool.Add(obj1); 
        }
    }

    public GameObject InitFxFeather()
    {
        for (int i = 0; i < fxFeatherPool.Count; i++)
        {
            if (fxFeatherPool[i].activeSelf == false)
            {
                fxFeatherPool[i].SetActive(true);
                return fxFeatherPool[i];
            }
        }

        GameObject obj1 = Instantiate(fxFeatherPrefab, transform);
        fxFeatherPool.Add(obj1);
        return obj1;
    }
}
