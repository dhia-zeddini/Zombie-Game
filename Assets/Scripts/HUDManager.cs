using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instace { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;


    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;


    public Sprite emptySlot;
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

    private void Update()
    {
        Weapon activeWeapon = WeaponManager.Instace.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon=GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();

        if (activeWeapon!=null)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{activeWeapon.magazineSize / activeWeapon.bulletsPerBurst}";

            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);

            activeWeaponUI.sprite= GetWeaponSprite(model);
            if (unActiveWeapon)
            {
                unActiveWeaponUI.sprite= GetAmmoSprite(unActiveWeapon.thisWeaponModel);
            }
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";
            ammoTypeUI.sprite = emptySlot;
            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;

        }

    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                return Instantiate(Resources.Load<GameObject>("PisTol_weapon")).GetComponent<SpriteRenderer>().sprite;
        
            case Weapon.WeaponModel.Shotgun:
                return Instantiate(Resources.Load<GameObject>("Shotgun_weapon")).GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.M16:
                return Instantiate(Resources.Load<GameObject>("M16_weapon")).GetComponent<SpriteRenderer>().sprite;

            default:
                return null;
        }
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                return Instantiate(Resources.Load<GameObject>("PisTol_Ammo")).GetComponent<SpriteRenderer>().sprite;

             case Weapon.WeaponModel.Shotgun:
                 return Instantiate(Resources.Load<GameObject>("Shotgun_Ammo")).GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.M16:
                return Instantiate(Resources.Load<GameObject>("M16_Ammo")).GetComponent<SpriteRenderer>().sprite;

            default:
                return null;
        }
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        foreach (GameObject weaponSlot in WeaponManager.Instace.weaponSlots)
        {
            if (weaponSlot != WeaponManager.Instace.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }

        return null;
    }
}
