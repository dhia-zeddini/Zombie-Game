using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instace { get; set; }

    public AudioSource shootingSound;
    public AudioSource reloadingSound;
   /* public AudioSource shootingSoundShotgun;
    public AudioSource reloadingSoundShotgun;*/
    public AudioSource emptyMagazineSound;
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

  /*  public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                shootingSound.Play();
                break;
            case WeaponModel.Shotgun:
                //shotgun sound
                shootingSoundShotgun.Play();
                break;
        }
    }*/
   /* public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                reloadingSound.Play();
                break;
            case WeaponModel.Shotgun:
                //shotgun sound
                reloadingSoundShotgun.Play();
                break;
        }
    }*/
}
