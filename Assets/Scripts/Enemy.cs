using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Track")]
    public string targetName = "Player";                    
    public float minimunTraceDistance = 5.0f;            

    GameObject targetObject = null;                         
    bool enableMove = false;                                

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName);  
    }

    void Update()
    {
      
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (distance >= minimunTraceDistance)
            enableMove = false;
        else
            enableMove = true;
    }

    void FixedUpdate()
    {
  
        if (enableMove)
            transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, 0.01f); 
    }
}
