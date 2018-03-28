using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePool : Singleton<TreePool>
{
    private GameObject Tree1Prefab;
    private GameObject Tree2Prefab;

    private int treeAmount = 30;
    private List<GameObject> treePool = new List<GameObject>();

	protected override  void Awake() 
	{
        base.Awake();
        Tree1Prefab = Resources.Load<GameObject>("Prefabs/Tree1");
        Tree2Prefab = Resources.Load<GameObject>("Prefabs/Tree2");

    }
    private void Start()
    {
        for (int i = 0; i < treeAmount; i++)
        {
            GameObject obj1 = Instantiate(Tree1Prefab, transform);
            obj1.SetActive(false);
            treePool.Add(obj1);
        }
    }
    public GameObject InitTree () 
	{
        for (int i = 0; i < treePool.Count; i++)
        {
            if (treePool[i].activeSelf == false)
            {
                treePool[i].SetActive(true);
                return treePool[i];
            }
        }

        GameObject obj1 = Instantiate(Tree1Prefab, transform);
        treePool.Add(obj1);
        return obj1;
    }
}
