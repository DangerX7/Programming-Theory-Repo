using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript: MonoBehaviour
{
    public PlayerScript playerRef;
    public GameObject Rabbit, Cheetah, Rhino;
    private Vector3 rabbitPos, cheetahPos, rhinoPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION
        rabbitPos = new Vector3(Rabbit.transform.position.x, Rabbit.transform.position.y, -10);
        cheetahPos = new Vector3(Cheetah.transform.position.x, Cheetah.transform.position.y, -10);
        rhinoPos = new Vector3(Rhino.transform.position.x, Rhino.transform.position.y, -10);

        switch (playerRef.animal)
        {
            case 1:
                transform.position = rabbitPos;
                break;
            case 2:
                transform.position = cheetahPos;
                break;
            case 3:
                transform.position = rhinoPos;
                break;
        }
    }
}
