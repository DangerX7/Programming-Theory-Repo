using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private RabbitScript rabbitScript;
    private CheetahScript cheetahScript;
    private RhinoScript rhinoScript;

    private GameObject rabbit, cheetah, rhyno;
    private Vector3 posSave, rhinoRabbitDistance, rabbitCheetahDistance, cheetahRhinoDistance;
    private int animal = 99; // 1 = rabbit, 2 = cheetah, 3 = rhyno
    private int moveDir; // -1 = left, 0 = none, 1 = right
    private bool isCharacterFacingRight;

    // ENCAPSULATION
    public static bool inputLock { get; set; }
    // ENCAPSULATION
    public static float speed{ get; set; }
    // ENCAPSULATION
    public static bool isPlayerOnGround { get; set; }

    private void Start()
    {
        //Get the correct distance between each animal for a correct transition
        rhinoRabbitDistance = new Vector3(0.5f, 0.4f, 0);
        rabbitCheetahDistance = new Vector3(-0.05f, -0.38f, 0);
        cheetahRhinoDistance = new Vector3(-0.48f, -0.02f, 0);
        //Get the reference for each child and their respective scripts
        rabbit = transform.GetChild(0).gameObject;
        cheetah = transform.GetChild(1).gameObject;
        rhyno = transform.GetChild(2).gameObject;
        rabbitScript = rabbit.GetComponent<RabbitScript>();
        cheetahScript = cheetah.GetComponent<CheetahScript>();
        rhinoScript = rhyno.GetComponent<RhinoScript>();

        animal = 0;
        AnimalChange();
    }
    private void Update()
    {
        #region Get Input Actions
        if (Input.GetKey("left"))
        {
            moveDir = -1;
            if (!inputLock)
            {
                Move();
            }
        }
        if (Input.GetKey("right"))
        {
            moveDir = 1;
            if (!inputLock)
            {
                Move();
            }
        }
        if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            moveDir = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && moveDir == 0 && isPlayerOnGround)
        {
            if (!inputLock)
            {
                AnimalChange();
            }
        }
        if (Input.GetKeyDown("space") && isPlayerOnGround)
        {
            JumpInitiate();
        }
        if (Input.GetKeyDown(KeyCode.Q) && isPlayerOnGround)
        {
            if (!inputLock)
            {
                Attack();
            }
        }
        #endregion
    }

    // ABSTRACTION
    private void Move()
    {
        if (moveDir == -1)
        {
            isCharacterFacingRight = false;
        }
        else if (moveDir == 1)
        {
            isCharacterFacingRight = true;
        }
        TurnLeftRight();
        transform.position += new Vector3(speed * moveDir, 0, 0) * Time.deltaTime;
    }

    // ABSTRACTION
    private void Attack()
    {
            //Get the correct methods from each animal script
            switch (animal)
            {
                case 1:
                    if (!isCharacterFacingRight)
                    {
                        rabbitScript.AttackLeft();
                    }
                    else
                    {
                        rabbitScript.AttackRight();
                    }
                    break;
                case 2:
                    if (!isCharacterFacingRight)
                    {
                        cheetahScript.AttackLeft();
                    }
                    else
                    {
                        cheetahScript.AttackRight();
                    }
                    break;
                case 3:
                    if (!isCharacterFacingRight)
                    {
                        rhinoScript.AttackLeft();
                    }
                    else
                    {
                        rhinoScript.AttackRight();
                    }
                    break;
            }
    }

    // ABSTRACTION
    private void JumpInitiate()
    {
        switch (animal)
        {
            case 1:
                rabbitScript.Jump();
                break;
            case 2:
                cheetahScript.Jump();
                break;
            case 3:
                rhinoScript.Jump();
                break;
        }
    }

    // ABSTRACTION
    private void AnimalChange()
    {
        switch (animal)
        {
            case 0:
                rabbit.gameObject.SetActive(true);
                cheetah.gameObject.SetActive(false);
                rhyno.gameObject.SetActive(false);
                animal = 1;
                break;
            case 1:
                Debug.Log("Switched to Cheetah");
                posSave = rabbit.transform.position;
                rabbit.gameObject.SetActive(false);
                cheetah.gameObject.SetActive(true);
                rhyno.gameObject.SetActive(false);
                cheetah.transform.position = posSave + rabbitCheetahDistance;
                TurnLeftRight();
                animal = 2;
                break;
            case 2:
                Debug.Log("Switched to Rhyno");
                posSave = cheetah.transform.position;
                rabbit.gameObject.SetActive(false);
                cheetah.gameObject.SetActive(false);
                rhyno.gameObject.SetActive(true);
                rhyno.transform.position = posSave + cheetahRhinoDistance;
                TurnLeftRight();
                animal = 3;
                break;
            case 3:
                Debug.Log("Switched to Rabbit");
                posSave = rhyno.transform.position;
                rabbit.gameObject.SetActive(true);
                cheetah.gameObject.SetActive(false);
                rhyno.gameObject.SetActive(false);
                rabbit.transform.position = posSave + rhinoRabbitDistance;
                TurnLeftRight();
                animal = 1;
                break;
        }
    }

    // ABSTRACTION
    private void TurnLeftRight()
    {
        if (!isCharacterFacingRight)
        {
            switch (animal)
            {
                case 1:
                    rabbitScript.TurnLeft();
                    break;
                case 2:
                    cheetahScript.TurnLeft();
                    break;
                case 3:
                    rhinoScript.TurnLeft();
                    break;
            }
        }
        else if (isCharacterFacingRight)
        {
            switch (animal)
            {
                case 1:
                    rabbitScript.TurnRight();
                    break;
                case 2:
                    cheetahScript.TurnRight();
                    break;
                case 3:
                    rhinoScript.TurnRight();
                    break;
            }
        }
    }
}
