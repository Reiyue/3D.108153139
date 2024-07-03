using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Sample")]
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("Bullet")]
    public float pushForce = 50.0f;
    public GameObject bullet;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
            RaycastHit hit;  
            Vector3 targetPoint;  

  
            if (Physics.Raycast(ray, out hit) == true)
                targetPoint = hit.point;       
            else
                targetPoint = ray.GetPoint(75);  

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10);
            Vector3 directionWithoutSpread = targetPoint - attackPoint.position; 
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); 
            currentBullet.transform.forward = directionWithoutSpread.normalized; 

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * pushForce, ForceMode.Impulse);
        }
    }
}
