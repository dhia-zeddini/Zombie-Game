using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instace { get; set; }

    public AudioSource shootingSound;
   

    public AudioClip PistolShot;
    public AudioClip M16Shot;
    public AudioClip ShotgunShot;

    public AudioSource reloadingSoundPistol;
    public AudioSource reloadingSoundM16;
    /*public AudioSource shootingSoundShotgun;
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

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                shootingSound.PlayOneShot(PistolShot);
                break;
            case WeaponModel.Shotgun:
                //shotgun sound
                shootingSound.PlayOneShot(ShotgunShot);
                break;
            case WeaponModel.M16:
                //M16 sound
                shootingSound.PlayOneShot(M16Shot);
                break;
        }
    }
    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                reloadingSoundPistol.Play();
                break;
            case WeaponModel.Shotgun:
                //shotgun sound
                //reloadingSoundShotgun.Play();
                reloadingSoundM16.Play();
                break;
            case WeaponModel.M16:
                //M16 sound
                reloadingSoundM16.Play();
                break;
        }
    }
}
