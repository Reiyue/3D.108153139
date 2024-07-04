using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Track")]
    public string targetName = "Player";                    
    public float minimunTraceDistance = 5.0f;

    NavMeshAgent navMeshAgent;
    GameObject targetObject = null;                                                        

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName); 
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
      
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (distance <= minimunTraceDistance)
            navMeshAgent.enabled = true;
        else
            navMeshAgent.enabled = false;
    }

    void FixedUpdate()
    {
       navMeshAgent.SetDestination(targetObject.transform.position);
    }
}
