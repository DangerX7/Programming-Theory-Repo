using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private Master_Script masterScript;
    private Color_Script colorScript;
    [SerializeField] int CheckpointNumber;
    private void Start()
    {
        masterScript = GameObject.Find("MasterObject").GetComponent<Master_Script>();
        colorScript = GetComponent<Color_Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Animal"))
        {
            masterScript.checkpoint = CheckpointNumber;
            masterScript.SaveState();
        }
    }
}
