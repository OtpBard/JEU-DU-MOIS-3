using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject TopLeft;
    public GameObject BottomRight;

    public bool IsIn(float x, float y)
    {
        return x > TopLeft.transform.position.x && x < BottomRight.transform.position.x && y > BottomRight.transform.position.y && y < TopLeft.transform.position.y;
    }
}
