using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour 
{
    [SerializeField] private GameObject visual1;
    [SerializeField] private GameObject visual2;
    [SerializeField] private GameObject visual3;
    public GameObject Visual1 { get { return visual1; } set { visual1 = value; } }
    public GameObject Visual2 { get { return visual1; } set { visual1 = value; } }
    public GameObject Visual3 { get { return visual1; } set { visual1 = value; } }

    //[SerializeField]
    private float speed;
    //public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float gravity = 0.0f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private float rolling_min_time = 1.0f;
    [SerializeField] private float rolling_thresh = 30f;
    private float cur_rolling_time = 0f;
    private float rolling_speed;

    private Rigidbody2D rig;
    private PlayerController controller;
    private Vector2 cur_velocity;
    public Vector2 Cur_velocity { get { return cur_velocity; } set { cur_velocity = value; } }

    private bool reset = true;
    public bool Reset { get { return reset; } set { reset = value; } }

    private Quaternion initRotation;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        initRotation = transform.rotation;
        rolling_speed = 360 / rolling_min_time;
    }

    public void Movement(bool m_left_btn_clicked, Vector3 ground_normal)
    {
        if (controller.MyState.IsLie)
        {
            return;
        }
        if (controller.MyState.IsOnGround && m_left_btn_clicked)
        {
            //rig.AddForce(new Vector2(jumpForce * ground_normal.x , jumpForce * ground_normal.y), ForceMode2D.Impulse);
            rig.AddForce(new Vector2(jumpForce * ground_normal.x + jumpForce * ground_normal.y, 0.6f* jumpForce * ground_normal.y), ForceMode2D.Impulse);
        }
        else if (m_left_btn_clicked && !controller.MyState.IsOnGround && !controller.MyState.IsRollling)
        {
            Roll();
        }
        else if(controller.MyState.IsRollling)
        {
            Rolling();
        }
        else if (controller.MyState.IsOnGround && !m_left_btn_clicked)
        {
            rig.AddForce(new Vector2(moveForce, -gravity), ForceMode2D.Force);
            Vector3 v = rig.velocity;
            Vector3 correct_dir = v - Vector3.Dot(ground_normal, v) * ground_normal;
            correct_dir.Normalize();
            rig.velocity = new Vector2(correct_dir.x, correct_dir.y) * v.magnitude;
        }

        speed = Mathf.Clamp(Vector3.SqrMagnitude(rig.velocity), 0f, maxSpeed);

        rig.velocity = rig.velocity.normalized * speed;
    }

    private void Roll()
    {
        Debug.Log("Roll");
        controller.MyState.IsRollling = true;
        cur_rolling_time = 0f;
        //rig.AddTorque(60);
        //controller.MyState.IsRoll = false;
    }

    private void Rolling()
    {
        Debug.Log("rolling");
        float rolling_angle = Time.deltaTime * rolling_speed;
        float cur_angle = rig.rotation + rolling_angle;
        cur_rolling_time += Time.deltaTime;
        if (cur_rolling_time > rolling_min_time)
        {
            RaycastHit2D ray_hit = Physics2D.Raycast(transform.position, -transform.up, 100f, layerMaskGround);
            if (!ray_hit)
            {
                rig.MoveRotation(cur_angle);
            }
            else
            {
                Vector3 normal = ray_hit.normal;
                float up_normal_angle = Vector3.SignedAngle(transform.up, normal, Vector3.forward);
                if (Mathf.Abs(up_normal_angle) > rolling_thresh)
                {
                    rig.MoveRotation(cur_angle);
                }
                else
                {
                    Debug.Log("rolling stop");
                    controller.MyState.IsRollling = false;
                    cur_rolling_time = 0f;
                }
            }
        }
        else
        {
            rig.MoveRotation(cur_angle);
        }
    }

    public void Lie()
    {
        if (reset)
        {
            reset = false;
            cur_velocity = rig.velocity;
            visual1.SetActive(false);
            visual2.SetActive(false);
            visual3.SetActive(true);

        }
        
        cur_velocity.Scale(new Vector2(0.1f, 0.9f));
        speed = cur_velocity.magnitude;
        if (cur_velocity.magnitude < 0.1f)
        {
            cur_velocity = Vector2.zero;
            Debug.Log("Lie" + rig.velocity);
            rig.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            //Debug.Log("Lie" + cur_velocity);
        }
        rig.velocity = cur_velocity;
        transform.rotation = initRotation;
    }
}
