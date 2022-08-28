using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : PlayerScript
{
    private GameObject Player;
    private Rigidbody rb;
    private float power = 2;
    private float jumpDistance = 8;
    private void Start()
    {
        Player = transform.parent.gameObject;
        transform.position = Player.transform.position;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        speed = 10;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpDistance, ForceMode.Impulse);
    }
    public void AttackLeft()
    {
        rb.AddForce(Vector3.left * power, ForceMode.Impulse);
        StartCoroutine(Recover());
    }
    public void AttackRight()
    {
        rb.AddForce(Vector3.right * power, ForceMode.Impulse);
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
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void TurnRight()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
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
