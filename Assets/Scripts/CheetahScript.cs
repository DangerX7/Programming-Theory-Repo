using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
public class CheetahScript : PlayerScript
{
    private Rigidbody rb;
    private int moveDir; // -1 = left, 0 = none, 1 = right
    [SerializeField] float speed;
    [SerializeField] float jumpDistance;
    [SerializeField] float power;
    public bool isPlayerOnGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown("space") && isPlayerOnGround)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.S) && isPlayerOnGround)
        {
            if (!inputLock)
            {
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("left"))
        {
            TurnLeft();
            if (!inputLock)
            moveDir = -1;
            {
                Move();
            }
        }
        if (Input.GetKey("right"))
        {
            moveDir= 1;
            if (!inputLock)
            {
                TurnRight();
                Move();
            }
        }
        if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            moveDir = 0;
        }
    }
    //POLYMORPHISM
    public override void Move()
    {
        transform.position += new Vector3(speed * moveDir, 0, 0) * Time.deltaTime;
    }
    //POLYMORPHISM
    public override void Jump()
    {
        rb.AddForce(Vector3.up * jumpDistance, ForceMode.Impulse);
    }
    //POLYMORPHISM
    public override void Attack()
    {
        rb.AddForce(Vector3.right * moveDir * power, ForceMode.Impulse);
        StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        inputLock = true;
        yield return new WaitForSeconds(0.5f);
        inputLock = false;
    }
    public void TurnLeft()
    {
        transform.eulerAngles = new Vector3(0, 90, 0);
    }
    public void TurnRight()
    {
        transform.eulerAngles = new Vector3(0, 270, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
        {
            isPlayerOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision2)
    {
        if (collision2.gameObject.tag.Equals("Platform"))
        {
            isPlayerOnGround = false;
        }
    }
}
