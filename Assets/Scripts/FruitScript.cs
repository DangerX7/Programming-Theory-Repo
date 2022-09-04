using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    private Master_Script masterScript;
    [SerializeField] int fruitNumber;
    private void Start()
    {
        masterScript = GameObject.Find("MasterObject").GetComponent<Master_Script>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Animal"))
        {
            switch (fruitNumber)
            {
                case 1:
                    masterScript.fruit1taken = true;
                    masterScript.SaveState();
                    break;
                case 2:
                    masterScript.fruit2taken = true;
                    masterScript.SaveState();
                    break;
                case 3:
                    masterScript.fruit3taken = true;
                    masterScript.SaveState();
                    break;
                case 4:
                    masterScript.fruit4taken = true;
                    masterScript.SaveState();
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
