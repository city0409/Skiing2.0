using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMove : MonoBehaviour 
{
    [SerializeField]
    private Transform cloudRight;
    [SerializeField]
    private Transform cloudLeft;

    private float cameraWidth;
    private float poleR;
    private float initCloudR;
    private float initCloudL;

    private Tweener t_cloudRight;
    private Tweener t_cloudLeft;

    private void Awake() 
	{
        initCloudR = cloudRight.transform.position.x + 17f;
        initCloudL = cloudLeft.transform.position.x - 14f;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        poleR = transform.position.x - cameraWidth*5 ;
        t_cloudRight = MoveCloud(cloudRight, poleR, 240f);
        t_cloudLeft = MoveCloud(cloudLeft, poleR, 160f);
    }

    private void Update () 
	{
        if (cloudRight.position.x < poleR + cameraWidth)
        {
            t_cloudRight.Kill();
            cloudRight.position = new Vector3(initCloudR , cloudRight.position.y, cloudRight.position.z);
            t_cloudRight = MoveCloud(cloudRight, poleR , 240f);
        }
        if (cloudLeft.position.x < poleR + cameraWidth )
        {
            t_cloudLeft.Kill();
            cloudLeft.position = new Vector3(initCloudR, cloudLeft.position.y, cloudLeft.position.z);
            t_cloudLeft = MoveCloud(cloudLeft, poleR, 240f);
        }
    }

    private Tweener MoveCloud(Transform cloud,float endValue,float duration)
    {
        return cloud.DOLocalMoveX(endValue, duration);
    }
}
