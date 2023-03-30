using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsCollected : MonoBehaviour
{
    public void Collected()
    {
        Destroy(gameObject);
    }
}
