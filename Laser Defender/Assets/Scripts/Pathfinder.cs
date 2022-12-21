using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WaveConfigSO _waveConfigSO;
    private int _waypointIndex = 0;
    private List<Transform> _waypoints;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        _waveConfigSO = _enemySpawner.GetCurrentWave();
        _waypoints = _waveConfigSO.GetWaypoints();
        transform.position = _waypoints[_waypointIndex].position;
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            Vector3 targetPosition = _waypoints[_waypointIndex].position;
            float delta = _waveConfigSO.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else  // enemy has reached end of path
        {
            Destroy(gameObject);
        }
    }
}
