using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instace { get; set; }

    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlot;

    [Header("Ammo")]
    public int totalM16Ammo = 0;
    public int totalPistolAmmo = 0;
    public int totalShotgunAmmo = 0;

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
        pickedupWeapon.transform.localRotation= Quaternion.Euler(0, 0, 0);
        pickedupWeapon.transform.localScale = new Vector3(1, 1, 1);
        weapon.isActiveWeapon = true;
        //weapon.GetComponent<Animator>().enabled = true;
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

    public void PickupAmmoBox(AmmoBox ammo)
    {
        switch(ammo.ammoType)
        {
            case AmmoBox.AmmoType.PistolAmmo:
                totalPistolAmmo += ammo.ammoAmount;
                break;
            case AmmoBox.AmmoType.M16Ammo:
                totalM16Ammo += ammo.ammoAmount;
                break;
            case AmmoBox.AmmoType.ShotgunAmmo:
                totalShotgunAmmo += ammo.ammoAmount;
                break;

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

    public void DecreaseTotalAmmo(int bulletsLeftsToDecrease, Weapon.WeaponModel thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case Weapon.WeaponModel.Pistol:
                totalPistolAmmo -= bulletsLeftsToDecrease;
                break;
            case Weapon.WeaponModel.M16:
                totalM16Ammo -= bulletsLeftsToDecrease;
                break;
            case Weapon.WeaponModel.Shotgun:
                totalShotgunAmmo -= bulletsLeftsToDecrease;
                break;


        }
    }
    public int CheckAmmoLeftFor(WeaponModel thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case WeaponModel.Pistol:
                return totalPistolAmmo;
            case WeaponModel.M16:
                return totalM16Ammo;
            case WeaponModel.Shotgun:
                return totalShotgunAmmo;

            default:
                return 0;
        }
    }
}
