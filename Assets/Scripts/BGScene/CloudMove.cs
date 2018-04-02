using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMove : MonoBehaviour 
{
    [SerializeField]
    private Transform cloud01;
    [SerializeField]
    private Transform cloud02;
    [SerializeField]
    private Transform cloud03;

    private float cameraWidth;
    private float initCloudPos;
    private float initCloud01;


    private void Awake() 
	{
        initCloud01 = cloud01.transform.position.x;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        initCloudPos = transform.position.x - cameraWidth;
        MoveCloud(cloud01, initCloudPos - cameraWidth , 30f);
    }

    private void Update () 
	{
        if (cloud01.position.x < initCloudPos)
        {
            print("okl");
            cloud01.position = new Vector3(initCloud01, cloud01.position.y, cloud01.position.z);
            //cloud01.position = new Vector3(cloud01.position.x + 4* cameraWidth, cloud01.position.y, cloud01.position.z);
            //cloud01.position += new Vector3(cloud01.position.x + 4* cameraWidth, 0f, 0f);
            MoveCloud(cloud01, initCloudPos - cameraWidth , 30f);
        }
    }

    private void MoveCloud(Transform cloud,float endValue,float duration)
    {
        cloud.DOLocalMoveX(endValue, duration);

    }
}
