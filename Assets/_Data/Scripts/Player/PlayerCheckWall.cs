using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckWall : MonoBehaviour
{
    public bool IsHitWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.IsHitWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.IsHitWall = false;
        }
    }
}
