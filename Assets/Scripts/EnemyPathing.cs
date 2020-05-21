using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    // Start is called before the first frame update
    int waypointIndex = 0;

    void Start()
    {
        transform.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIndex <= waypoints.Count - 1) {
            var moveDelta = moveSpeed * Time.deltaTime;
            var targetPos = waypoints[waypointIndex].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveDelta);

            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        } else
        {
            Destroy(gameObject);
        }
    }
}
