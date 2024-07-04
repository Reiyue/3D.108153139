using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Track")]
    public string targetName = "Player";                    
    public float minimunTraceDistance = 5.0f;

    [Header("Hp")]
    public float maxLife = 10.0f;              
    public Image lifeBarImage;               
    float lifeAmount;                         

    NavMeshAgent navMeshAgent;
    GameObject targetObject = null;
    
    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName); 
        navMeshAgent = GetComponent<NavMeshAgent>();
        lifeAmount = maxLife;
    }

    void Update()
    {
      
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (distance <= minimunTraceDistance)
            navMeshAgent.enabled = true;
        else
            navMeshAgent.enabled = false;

        faceTarget(); 

        if (lifeAmount <= 0.0f)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(targetObject.transform.position);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            lifeAmount -= 1.0f;
            lifeBarImage.fillAmount = lifeAmount / maxLife;
        }
    }

    void faceTarget()
    {
        Vector3 targetDir = targetObject.transform.position - transform.position;                               // �p��ĤH�P���⤧�����V�q
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.1f * Time.deltaTime, 0.0F);      // �̷ӼĤHZ�b�V�q�P��̶��V�q�A�i�H�p��X�ݭn���઺����
        transform.rotation = Quaternion.LookRotation(newDir);                                                   // �i�����
    }
}
