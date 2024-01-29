using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instace { get; set; }

    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlot;
    private void Awake()
    {
        if (Instace != null && Instace != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instace = this;
        }
    }


    private void Start()
    {
        activeWeaponSlot = weaponSlots[0];
    }
    private void Update()
    {
        foreach(GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlot)
            {
                 weaponSlot.SetActive(true);
            }
            else
            {
                weaponSlot.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1);
        }
    }
    public void PickupWeapon(GameObject pickedupWeapon)
    {
        AddWeaponIntoActiveSlot(pickedupWeapon);
       // Destroy(pickedupWeapon);
    }

    private void AddWeaponIntoActiveSlot(GameObject pickedupWeapon)
    {
        DropCurrentWeapon(pickedupWeapon);

        pickedupWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        Weapon weapon = pickedupWeapon.GetComponent<Weapon>();

        pickedupWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z);
        pickedupWeapon.transform.localRotation= Quaternion.Euler(1, 1, 1);
        pickedupWeapon.transform.localScale = new Vector3(1, 1, 1);
        weapon.isActiveWeapon = true;
    }

    private void DropCurrentWeapon(GameObject pickedupWeapon)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;

            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;

            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent);
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition;
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation;
            weaponToDrop.transform.localScale = pickedupWeapon.transform.localScale;

        }
    }

    public void SwitchActiveSlot(int slotNumber)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            Weapon currentWeapn = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            currentWeapn.isActiveWeapon = false;
        }

        activeWeaponSlot = weaponSlots[slotNumber];
        if (activeWeaponSlot.transform.childCount > 0)
        {
            Weapon newWeapn = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            newWeapn.isActiveWeapon = true;
        }
    }
}
