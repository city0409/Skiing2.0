using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SomeClass : MonoBehaviour
{
    private ObjectPool<List<Vector3>> m_poolOfListOfVector3 =
        //32为假设的最大数量
        new ObjectPool<List<Vector3>>(32,
        (list) => {
            list.Clear();
        },
        (list) => {
            //初始化容量为1024
            list.Capacity = 1024;
        });

    void Update()
    {
        List<Vector3> listVector3 = m_poolOfListOfVector3.New();

        // do stuff

        m_poolOfListOfVector3.Store(listVector3);
    }
}