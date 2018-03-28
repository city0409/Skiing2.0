using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AroundInitOBJ : MonoBehaviour 
{
    private GameObject player;
    private Action onBedBoyBorn;
    private Action onPlayerSpawn;
    private float initTreePos;
    private float timer;
    [SerializeField]
    private LayerMask layerMaskGround;
    Vector2 pos;

    private float cameraWidth;

    private void Awake() 
	{
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }
    private void OnEnable()
    {
        onBedBoyBorn = OnBedBoyBorn;
        onPlayerSpawn = OnPlayerSpawn;
        //EventService.Instance.GetEvent<BedBoyBornEvent>().Subscribe(onBedBoyBorn);
        EventService.Instance.GetEvent<OnPlayerSpawnEvent>().Subscribe(onPlayerSpawn);
    }
    private void OnPlayerSpawn()
    {
        player = LevelDirector.Instance.PlayerOBJ;
        print("OnPlayerSpawn");
    }
    private void OnBedBoyBorn()
    {
        player = LevelDirector.Instance.BedBoyOBJ;
        print("OnBedBoyBorn");
    }
    private void Update () 
	{
        if (player == null) { print("#########"); return; }
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            InitTrees();
        }
        
    }
    public void InitTrees()
    {
        initTreePos = player.transform.position.x + cameraWidth + UnityEngine.Random.Range(6f, 16f);
        pos = new Vector2(initTreePos, player.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up, 1000f, layerMaskGround);
        if (hit.collider != null)
        {
            Vector3 v =  hit.point;
            GameObject obj1 = TreePool.Instance.InitTree();
            obj1.transform.position = v;
            obj1.transform.localRotation = Quaternion.FromToRotation(obj1.transform.up, hit.normal);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pos, pos - new Vector2(0f, 1000f));
        }
    }
    public void InitStones()
    {

    }
    public void InitHouses()
    {

    }
    private void OnDisable()
    {
        //EventService.Instance.GetEvent<BedBoyBornEvent>().UnSubscribe(onBedBoyBorn);
        EventService.Instance.GetEvent<OnPlayerSpawnEvent>().UnSubscribe(onPlayerSpawn);
    }
}
