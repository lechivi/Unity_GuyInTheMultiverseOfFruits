using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset;

    private void FixedUpdate()
    {
        if (this.target == null) return;
        this.transform.position = new Vector3(this.target.transform.position.x + this.offset.x, this.target.transform.position.y + this.offset.y, transform.position.z);
    }
}
