using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Singleton<PlayerController>
{
    private Animator anim;
    private TrailRenderer Scarf;
    private const string pauseName = "pause";
    private const string continueName = "continue";
    private const string restartName = "restart";
    private PlayerMotor playerMotor;
    private PlayerState myState;
    public PlayerState MyState { get { return myState; } set { myState = value; } }

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
        anim = GetComponentInChildren<Animator>();
        Scarf = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        myState.IsSkiing = true;
        Scarf.time = 0.09f;
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

        //if (myState.IsRideSnowMan)
        //{
        //    GoYou();
        //}
        
        if(myState.IsLie)
        {
            //anim.SetTrigger("lie");
            anim.SetBool("ski", false);
            anim.SetBool("lie", true);
            anim.SetBool("roll", false);
            anim.SetBool("jump", false);
        }
        else if (myState.IsRollling)
        {
            //anim.SetTrigger("roll");
            anim.SetBool("ski", false);
            anim.SetBool("lie", false);
            anim.SetBool("roll", true);
            anim.SetBool("jump", false);
        }
        else if (!myState.IsOnGround)
        {
            //anim.SetTrigger("jump");
            anim.SetBool("ski", false);
            anim.SetBool("lie", false);
            anim.SetBool("roll", false);
            anim.SetBool("jump", true);
        }
        else if (myState.IsSkiing)
        {
            //anim.SetTrigger("ski");
            anim.SetBool("ski", true);
            anim.SetBool("lie", false);
            anim.SetBool("roll", false);
            anim.SetBool("jump", false);
        }

    }

    private void FixedUpdate()
    {
        DetacteRaycast();
        playerMotor.Movement(m_left_btn_clicked, ground_normal);
        m_left_btn_clicked = false;

        if (myState.IsLie)
        {
            playerMotor.IsStatic = true;
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
            myState.IsLie = true;
            return;
        }

        ContactFilter2D filter_snowman = new ContactFilter2D();
        filter_snowman.layerMask = layerMaskSnowman;
        filter_snowman.useLayerMask = true;

        int hit_snowman_num = playColl.OverlapCollider(filter_snowman, colliders);
        if (hit_snowman_num > 0)
        {
            //雪人处理代码
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
                myState.IsLie = true;
            }
            else
            {
                myState.IsSkiing = true;
            }
        }
    }

}
