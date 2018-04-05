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
    private float initHousePos;
    private float initStonePos;
    private float initSlidePos;
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
        if (player == null)  return; 
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            InitTrees();
            //InitHouses();
            InitStones();
        }
        
    }
    public void InitTrees()
    {
        initTreePos = player.transform.position.x + cameraWidth*2f + UnityEngine.Random.Range(6f, 16f);
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

    public void InitSlide()
    {
        initSlidePos = player.transform.position.x - cameraWidth;
        pos = new Vector2(initSlidePos, player.transform.position.y+20f);
        RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up, 1000f, layerMaskGround);
        if (hit.collider != null)
        {
            Vector3 v = hit.point;
            GameObject obj1 = SlidePool.Instance.InitSlide();
            obj1.transform.position = v;
            obj1.transform.localRotation = Quaternion.FromToRotation(obj1.transform.up, hit.normal);
            obj1.transform.localRotation = Quaternion.identity;
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
        initStonePos = player.transform.position.x + cameraWidth * 1.5f + UnityEngine.Random.Range(16f, 26f);
        pos = new Vector2(initStonePos, player.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up, 1000f, layerMaskGround);
        if (hit.collider != null)
        {
            Vector3 v = hit.point;
            GameObject obj1 = StonePool.Instance.InitStone();
            obj1.transform.position = v;
            obj1.transform.localRotation = Quaternion.FromToRotation(obj1.transform.up, hit.normal);
        }
    }
    public void InitHouses()
    {
        initHousePos = player.transform.position.x + cameraWidth*4 + UnityEngine.Random.Range(16f, 26f);
        pos = new Vector2(initHousePos, player.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up, 1000f, layerMaskGround);
        if (hit.collider != null)
        {
            Vector3 v = hit.point;
            GameObject obj1 = HousePool.Instance.InitHouse();
            obj1.transform.position = v;
            obj1.transform.localRotation = Quaternion.FromToRotation(obj1.transform.up, hit.normal);
        }
    }
    private void OnDisable()
    {
        //EventService.Instance.GetEvent<BedBoyBornEvent>().UnSubscribe(onBedBoyBorn);
        EventService.Instance.GetEvent<OnPlayerSpawnEvent>().UnSubscribe(onPlayerSpawn);
    }
}
