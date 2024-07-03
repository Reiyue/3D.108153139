using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Camera")]
    public float sensX;   
    public float sensY;   

    float xRotation;
    float yRotaiton;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;   
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;  

       
        xRotation -= mouseY; 
        yRotaiton += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 30f); 

        transform.rotation = Quaternion.Euler(xRotation, yRotaiton, 0); 
    }
}
