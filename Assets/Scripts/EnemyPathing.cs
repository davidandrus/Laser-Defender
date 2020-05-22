using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;

    int waypointIndex = 0;
    private List<Transform> waypoints;
    private float moveSpeed;

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
        transform.position = waypoints[waypointIndex].position;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

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
