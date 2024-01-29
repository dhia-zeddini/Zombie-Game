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
    }
    public void PickupWeapon(GameObject pickedupWeapon)
    {
        AddWeaponIntoActiveSlot(pickedupWeapon);
       // Destroy(pickedupWeapon);
    }

    private void AddWeaponIntoActiveSlot(GameObject pickedupWeapon)
    {
        pickedupWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        Weapon weapon = pickedupWeapon.GetComponent<Weapon>();
        pickedupWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z);
        pickedupWeapon.transform.localRotation= Quaternion.Euler(1, 1, 1);
        pickedupWeapon.transform.localScale = new Vector3(1, 1, 1);
    }
}
