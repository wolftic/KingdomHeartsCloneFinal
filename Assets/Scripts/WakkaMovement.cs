using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class WakkaMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Wakka _wakka;
    [SerializeField] private float _radius;

    public bool IsWalking
    {
        get
        {
            var walking = (_navMeshAgent.remainingDistance > 0);
            return walking;
        }
    }

    private void Update()
    {
        if (!IsWalking) return;
        var position = _navMeshAgent.nextPosition;
        transform.LookAt(position, transform.up);
    }

    private void Start ()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
		_wakka = GetComponent<Wakka> ();
	}

	public void WanderToNextPoint ()
	{
	    var pos = Random.insideUnitSphere * _radius;
	    pos.y = 0f;
	    pos = transform.position + pos;
	    _navMeshAgent.SetDestination(pos);
	}
}
