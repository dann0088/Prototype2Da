using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float patrolRange = 6f; //range of the patrolling
    [SerializeField] private float radius = 3f; //range of the detection
    
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private bool showGizmos = true;

    private Enemy enemy;

    NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    private void Update() {
        if (enemy.GetEnemyDeadStatus() == false) {
            // Set the agent to target the player
            //agent.SetDestination(target.position);
            var playerDetect = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
            //Debug.Log(playerDetect);
            if (playerDetect != null) {
                agent.SetDestination(target.position);
            } else {
                if (agent.remainingDistance <= agent.stoppingDistance) { //done with path
                    Vector3 point;
                    if (RandomPoint(transform.position, patrolRange, out point)) { //pass in our centre point and radius of area
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); // so you can see with gizmos
                        agent.SetDestination(point);
                    }
                }
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) { //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation

            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void OnDrawGizmos() {
        if (showGizmos) {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.position, radius);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawSphere(transform.position, patrolRange);
        }
    }
}

