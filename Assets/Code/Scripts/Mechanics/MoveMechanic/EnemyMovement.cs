using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : BaseMovement
    {
        private NavMeshAgent _agent;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public override void Move(Vector3 position)
        {
            transform.LookAt(transform.position+position);
            _agent.SetDestination(position);
        }
    }
