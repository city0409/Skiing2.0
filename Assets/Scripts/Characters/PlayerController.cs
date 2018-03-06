using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Singleton<PlayerController>
{
    private const string pauseName = "pause";
    private const string continueName = "continue";
    private const string restartName = "restart";
    private PlayerMotor playerMotor;
    private PlayerState myState;
    public PlayerState MyState { get { return myState; } set { myState = value; } }

    [SerializeField] private GameObject visual1;
    [SerializeField] private GameObject visual2;
    [SerializeField] private GameObject visual3;
    private BoxCollider2D playColl;
    public BoxCollider2D PlayColl { get { return playColl; } set { playColl = value; } }

    private bool m_left_btn_clicked;
    private Vector3 ground_normal;//当前地面法线

    [SerializeField] private SnowManController snowMan;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerMaskStone;
    [SerializeField] private LayerMask layerMaskSnowman;
    [SerializeField] private LayerMask layerMaskSnow;


    protected override void Awake()
    {
        base.Awake();
        snowMan = GetComponent<SnowManController>();
        playColl = GetComponent<BoxCollider2D>();
        myState = new PlayerState();

        playerMotor = GetComponent<PlayerMotor>();
    }

    private void Start()
    {
        myState.IsSkiing = true;
    }

    private void Update()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        if (obj && (obj.name == pauseName || obj.name == continueName || obj.name == restartName))
        {
            return;
        }
        if (!m_left_btn_clicked)
        {
            m_left_btn_clicked = Input.GetMouseButtonDown(0);
        }

        if (myState.IsRideSnowMan)
        {
            GoYou();
        }
    }

    private void FixedUpdate()
    {
        DetacteRaycast();
        playerMotor.Movement(m_left_btn_clicked, ground_normal);
        m_left_btn_clicked = false;

        if (myState.IsLie)
        {
            playerMotor.Lie();
        }
    }

    private void GoYou()
    {
        //playColl.offset = new Vector2(0f, 0.34f);
        // playColl.size = new Vector2(1.02f, 1.68f);
        //myState.IsSkiing = true;
    }

    private void DetacteRaycast()
    {
        if (!playerMotor.Reset)
        {
            Debug.Log("DetacteRaycast");
            return;
        }

        Collider2D[] colliders = new Collider2D[1];
        ContactFilter2D filter_snow = new ContactFilter2D();
        filter_snow.layerMask = layerMaskSnow;
        filter_snow.useLayerMask = true;

        int hit_snow_num = playColl.OverlapCollider(filter_snow, colliders);
        if (hit_snow_num > 0)
        {
            //被雪崩撞了
            myState.IsLie = true;
            return;
        }

        ContactFilter2D filter_stone = new ContactFilter2D();
        filter_stone.layerMask = layerMaskStone;
        filter_stone.useLayerMask = true;

        int hit_stone_num = playColl.OverlapCollider(filter_stone, colliders);
        if (hit_stone_num > 0)
        {
            //LayerMask mask = colliders[0].GetComponent<Transform>().gameObject.layer;
            //Debug.Log("die " + LayerMask.LayerToName(mask.value));
            Debug.Log("stone!!!!!!");
            myState.IsLie = true;
            return;
        }
        //RaycastHit2D ray_hit0 = Physics2D.Raycast(transform.position, transform.right, 0.6f, layerMaskOther);
        //if (ray_hit0)
        //{
        //    myState.IsLie = true;
        //    return;
        //}

        ContactFilter2D filter_snowman = new ContactFilter2D();
        filter_snowman.layerMask = layerMaskSnowman;
        filter_snowman.useLayerMask = true;

        int hit_snowman_num = playColl.OverlapCollider(filter_snowman, colliders);
        if (hit_snowman_num > 0)
        {
            //雪人处理代码
            Debug.Log("snow man");
            visual1.SetActive(false);
            visual2.SetActive(true);
            myState.IsRideSnowMan = true;
        }


        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = layerMask;
        filter.useLayerMask = true;
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hit_num = playColl.Cast(Vector2.down, filter, hits, 0.1f);

        bool is_hit = false;
        bool is_lie = false;

        if (hit_num > 0)
        {
            RaycastHit2D ray_hit = hits[0];
            Vector3 normal = ray_hit.normal;
            Vector3 up = transform.up;
            ground_normal = normal;

            float up_dot_normal = Vector3.Dot(normal, up);
            float thresh = Mathf.Sqrt(2) / 2.0f;

            is_hit = true;
            myState.IsOnGround = true;
            myState.IsRollling = false;
            if (up_dot_normal < thresh)
            {
                is_lie = true;
            }
        }
        else
        {
            myState.IsOnGround = false;
        }

        if (is_hit)
        {
            if (is_lie)
            {
                Debug.Log("jiance");
                //myState.IsJump = false;
                myState.IsLie = true;
            }
            else
            {
                myState.IsSkiing = true;
                //myState.IsJump = false;
            }
        }


        //Vector2 col_offset = playColl.offset;
        //Vector2 col_size = playColl.size;
        //float expand = 1.2f;
        //Vector3 left_bottom = new Vector3(-col_size.x * 0.5f * expand + col_offset.x, -col_size.y * 0.5f * expand + col_offset.y, 0.0f);
        //Vector3 right_bottom = new Vector3(col_size.x * 0.5f * expand + col_offset.x, -col_size.y * 0.5f * expand + col_offset.y, 0.0f);
        //Vector3 left_top = new Vector3(-col_size.x * 0.5f * expand + col_offset.x, col_size.y * 0.5f * expand + col_offset.y, 0.0f);
        //Vector3 right_top = new Vector3(col_size.x * 0.5f * expand + col_offset.x, col_size.y * 0.5f * expand + col_offset.y, 0.0f);

        //Vector3[] points = new Vector3[4];
        //points[0] = transform.TransformPoint(left_bottom);
        //points[1] = transform.TransformPoint(right_bottom);
        //points[2] = transform.TransformPoint(right_top);
        //points[3] = transform.TransformPoint(left_top);

        //Vector3[] lines = new Vector3[4];
        //lines[0] = points[1] - points[0];
        //lines[1] = points[2] - points[1];
        //lines[2] = points[3] - points[2];
        //lines[3] = points[0] - points[3];

        //bool is_hit = false;
        //bool is_lie = false;

        //for (int i = 0; i < 1; i++)
        //{
        //    Vector3 dir = lines[i];
        //    dir.Normalize();
        //    Vector3 start = points[i];
        //    Vector3 end = points[(i + 1) % 4];
        //    RaycastHit2D ray_hit = Physics2D.Raycast(start, dir, lines[i].magnitude, layerMask);
        //    Vector3 normal = ray_hit.normal;
        //    Vector3 up = transform.up;

        //    float up_dot_normal = Vector3.Dot(normal, up);
        //    float thresh = Mathf.Sqrt(3) / 2.0f;

        //    if (ray_hit)
        //    {
        //        is_hit = true;
        //        if (up_dot_normal < thresh)
        //        {
        //            is_lie = true;
        //            Debug.Log("start" + start);
        //            Debug.Log("end" + end);
        //            Debug.Log("normal" + i);
        //            Debug.Log("up" + up);
        //            break;
        //        }
        //    }
        //}

    }

}
