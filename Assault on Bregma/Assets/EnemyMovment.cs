using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{

	[SerializeField] private float waitTime = 1f;

	// Use this for initialization
	void Start ()
	{
	    Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
	    var path = pathfinder.GetPath();
	    StartCoroutine(FollowPath(path));

	}

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(waitTime);
        }
    }

}
