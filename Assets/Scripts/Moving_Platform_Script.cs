using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform_Script : MonoBehaviour
{
    private float moveDir, speed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
        moveDir = speed;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * moveDir, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Platform"))
        {
            if (moveDir == speed)
            {
                moveDir = -speed;
            }
            else if (moveDir == -speed)
            {
                moveDir = speed;
            }
        }
        if (collision.gameObject.tag.Equals("Animal"))
        {
            Master_Script master_Script = GameObject.Find("MasterObject").GetComponent<Master_Script>();
            master_Script.RestartLevel();
        }
    }
}
