using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    [Header("Sample")]
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("Bullet")]
    public GameObject bullet;

    [Header("Gun")]
    public int magazineSize;        
    public int bulletsLeft;         
    public float reloadTime;        
    public float recoilForce;   

    bool reloading;                

    [Header("UI")]
    public TextMeshProUGUI ammunitionDisplay;
    public TextMeshProUGUI reloadingDisplay;  

    [Header("Weapon")]
    public GameObject[] weaponObjects;        

    int weaponNumber = 0;                     
    GameObject weaponInUse;                

    private void Start()
    {
        bulletsLeft = magazineSize;       
        reloadingDisplay.enabled = false;  

        ShowAmmoDisplay();                 
        weaponInUse = weaponObjects[0];   
    }

    private void Update()
    {
        MyInput();
    }

  
    private void MyInput()
    {
     
        if (Input.GetMouseButtonDown(0) == true)
        {
         
            if (bulletsLeft > 0 && !reloading)
            {
                Shoot();
            }
        }

     
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0, 0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(0, 1);


        if (Input.GetAxis("Mouse ScrollWheel") > 0f)   
            SwitchWeapon(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
            SwitchWeapon(-1);
    }

    private void Shoot()
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

        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 20, ForceMode.Impulse); 


        bulletsLeft--;   

        this.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);

        ShowAmmoDisplay();       

        weaponInUse.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fire");  
    }

    private void Reload()
    {
        reloading = true;                    
        reloadingDisplay.enabled = true;       
        Invoke("ReloadFinished", reloadTime); 
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

    private void SwitchWeapon(int _addNumber, int _weaponNumber = 0)
    {

        foreach (GameObject item in weaponObjects)
        {
            item.SetActive(false);
        }

        switch (_addNumber)
        {
            case 0:                                                  
                weaponNumber = _weaponNumber;
                break;
            case 1:                                                  
                if (weaponNumber == weaponObjects.Length - 1)        
                    weaponNumber = 0;
                else
                    weaponNumber += 1;

                break;
            case -1:                                               
                if (weaponNumber == 0)                              
                    weaponNumber = weaponObjects.Length - 1;
                else
                    weaponNumber -= 1;

                break;
        }
        weaponObjects[weaponNumber].SetActive(true);    
        weaponInUse = weaponObjects[weaponNumber];    
    }
}
