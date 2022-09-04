using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Master_Script masterScript;
    private Rigidbody rigidBody;
    public GameObject rabbit, cheetah, rhino, checkPoint0, checkPoint1, checkPoint2, checkPoint3, checkPoint4, checkPoint5;
    private Vector3 posSave, rhinoRabbitDistance, rabbitCheetahDistance, cheetahRhinoDistance, StartDist;

    public static bool inputLock;
    public int animal; // 1 = rabbit, 2 = cheetah, 3 = rhyno


    private void Start()
    {
        masterScript = GameObject.Find("MasterObject").GetComponent<Master_Script>();
        StartDist = new Vector3(5.6f, 6.62f, 0);
        switch (masterScript.checkpoint)
        {
            case 0:
                transform.position = checkPoint0.transform.position + StartDist;
                break;
            case 1:
                transform.position = checkPoint1.transform.position + StartDist;
                break;
            case 2:
                transform.position = checkPoint2.transform.position + StartDist;
                break;
            case 3:
                transform.position = checkPoint3.transform.position + StartDist;
                break;
            case 4:
                transform.position = checkPoint4.transform.position + StartDist;
                break;
            case 5:
                transform.position = checkPoint5.transform.position + StartDist;
                break;
        }

        //Get the correct distance between each animal for a correct transition
        rhinoRabbitDistance = new Vector3(0.5f, 0.4f, 0);
        rabbitCheetahDistance = new Vector3(-0.05f, -0.38f, 0);
        cheetahRhinoDistance = new Vector3(-0.48f, -0.02f, 0);
        //Get the reference for each child and their respective scripts
        rabbit = transform.GetChild(0).gameObject;
        cheetah = transform.GetChild(1).gameObject;
        rhino = transform.GetChild(2).gameObject;

        AnimalChange();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!inputLock)
            {
                AnimalChange();
            }
        }
    }

    // POLYMORPHISM
    public virtual void Move()
    {
        transform.position += new Vector3(10 , 0, 0) * Time.deltaTime;
    }
    // POLYMORPHISM
    public virtual void Attack()
    {
        rigidBody.AddForce(Vector3.right * 15, ForceMode.Impulse);
    }
    // POLYMORPHISM
    public virtual void Jump()
    {
        rigidBody.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    // ABSTRACTION
    public void AnimalChange()
    {
        switch (animal)
        {
            case 0:
                rabbit.gameObject.SetActive(true);
                cheetah.gameObject.SetActive(false);
                rhino.gameObject.SetActive(false);
                animal = 1;
                break;
            case 1:
                Debug.Log("Switched to Cheetah");
                posSave = rabbit.transform.position;
                rabbit.gameObject.SetActive(false);
                cheetah.gameObject.SetActive(true);
                rhino.gameObject.SetActive(false);
                cheetah.transform.position = posSave + rabbitCheetahDistance;
                animal = 2;
                break;
            case 2:
                Debug.Log("Switched to Rhyno");
                posSave = cheetah.transform.position;
                rabbit.gameObject.SetActive(false);
                cheetah.gameObject.SetActive(false);
                rhino.gameObject.SetActive(true);
                rhino.transform.position = posSave + cheetahRhinoDistance;
                animal = 3;
                break;
            case 3:
                Debug.Log("Switched to Rabbit");
                posSave = rhino.transform.position;
                rabbit.gameObject.SetActive(true);
                cheetah.gameObject.SetActive(false);
                rhino.gameObject.SetActive(false);
                rabbit.transform.position = posSave + rhinoRabbitDistance;
                animal = 1;
                break;
        }
    }
}
