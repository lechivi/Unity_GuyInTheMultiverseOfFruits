using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject follower;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed;

    private int curentPoint = 0;

    private void Update()
    {
        if (Vector2.Distance(this.waypoints[curentPoint].transform.position, this.follower.transform.position) < 0.1f)
        {
            this.curentPoint++;
            if (this.curentPoint >= this.waypoints.Length)
            {
                this.curentPoint = 0;
            }
        }

        this.follower.transform.position = Vector2.MoveTowards(this.follower.transform.position, this.waypoints[curentPoint].transform.position, this.speed * Time.deltaTime);
    }
}
