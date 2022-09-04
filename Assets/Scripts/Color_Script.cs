using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Script : Master_Script
{
    public int colorSet;
    new private void Start()
    {
        SetColor();
        base.Start();
    }
    public void SetColor()
    {
        switch (colorSet)
        {
            case 0:
                color = Color.cyan;
                break;
            case 1:
                color = Color.red;
                break;
            case 2:
                color = Color.gray;
                break;
            case 3:
                color = Color.green;
                break;
            case 4:
                color = Color.blue;
                break;
            case 5:
                color = Color.yellow;
                break;
        }
    }
}
