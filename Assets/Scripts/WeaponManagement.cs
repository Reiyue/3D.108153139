using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject[] weaponObjects;

    int weaponNumber = 0;
    GameObject weaponInUse;

    private void Start()
    {
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

            weaponInUse.GetComponent<Weapon>().Attack();
        }

        if (Input.GetKeyDown(KeyCode.R))
            weaponInUse.GetComponent<Weapon>().Reload();


        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0, 0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(0, 1);


        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            SwitchWeapon(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            SwitchWeapon(-1);
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
                if (weaponNumber == 0)                                 // ��{�`���Ʀr�A���w�쥻���Z���}�C��}�O�Ĥ@�ӪZ���A�h�N�Z���}�C��}���M�檺�̫�@�Ӧ�}
                    weaponNumber = weaponObjects.Length - 1;
                else
                    weaponNumber -= 1;
                break;
        }
        weaponObjects[weaponNumber].SetActive(true);
        weaponInUse = weaponObjects[weaponNumber];
    }
}
