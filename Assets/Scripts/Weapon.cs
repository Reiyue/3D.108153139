using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Sample")]
    public GameObject PlayerObejct;
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("Bullet")]
    public GameObject bullet;

    [Header("Gun")]
    public bool isGun;
    public int magazineSize;        
    public int bulletsLeft;        
    public float reloadTime;        
    public float recoilForce;  

    bool reloading;            

    [Header("UI")]
    public TextMeshProUGUI ammunitionDisplay; 
    public TextMeshProUGUI reloadingDisplay; 

    private void Start()
    {
        bulletsLeft = magazineSize;       
        reloadingDisplay.enabled = false;  

        ShowAmmoDisplay();             
    }

    public void Attack()
    {
        if (isGun && bulletsLeft > 0 && !reloading)
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  
            RaycastHit hit;  
            Vector3 targetPoint;  

            if (Physics.Raycast(ray, out hit) == true)
                targetPoint = hit.point;         
            else
                targetPoint = ray.GetPoint(75);  

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); 

            Vector3 shootingDirection = targetPoint - attackPoint.position; 
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); 
            currentBullet.transform.forward = shootingDirection.normalized; 

            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 30, ForceMode.Impulse); 
 
            bulletsLeft--;    
   
            PlayerObejct.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);
        }

        ShowAmmoDisplay();            

        if (transform.GetChild(0).GetComponent<Animator>() != null)
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fire"); 
    }

    private void OnEnable()
    {
        ShowAmmoDisplay();              
    }

    public void Reload()
    {
        if (bulletsLeft < magazineSize && !reloading)
        {
            reloading = true;                      
            reloadingDisplay.enabled = true;       
            Invoke("ReloadFinished", reloadTime); 
        }
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;           
        reloading = false;                     
        reloadingDisplay.enabled = false;      
        ShowAmmoDisplay();
    }

    private void ShowAmmoDisplay()
    {
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText($"Ammo {bulletsLeft} / {magazineSize}");
    }
}
