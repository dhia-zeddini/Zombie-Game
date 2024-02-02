using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{

    public bool isActiveWeapon;

    //  public Camera playerCamera;

    [Header("Shooting")]
    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    [Header("Burst")]
    //brust
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    [Header("Spread")]
    //spread
    public float spreadIntensity;
    public float hipSpreadIntensity;
    public float adsSpreadIntensity;

    [Header("Bullet")]
    //bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    [Header("Animation")]
    public GameObject muzzleEffect;
    private Animator animator;

    [Header("Loading")]
    //loading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    //original weapon position
    public Vector3 spawnPosition;

   //weapon model
   public enum WeaponModel
    {
        Pistol,
        Shotgun,
        M16
    }
    public WeaponModel thisWeaponModel;

    //shooting mode
    public enum ShootingMode
    {
        single,
        Brust,
        Auto
    }

    public ShootingMode currentShootingMode;


    //ADS
    bool isADS;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator=GetComponent<Animator>();
        bulletsLeft = magazineSize;
        spreadIntensity = hipSpreadIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveWeapon)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Outline>().enabled = false;
            if (Input.GetMouseButtonDown(1)) 
            {
                EnterADS();

            }
            if (Input.GetMouseButtonUp(1))
            {
              ExitADS();
            }
           
            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instace.emptyMagazineSound.Play();
            }
            if (currentShootingMode == ShootingMode.Auto)
            {
                //holding down left mouse btn
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.single || currentShootingMode == ShootingMode.Brust)
            {
                //clicking left mouse btn once
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }
            //reload manualy
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false && WeaponManager.Instace.CheckAmmoLeftFor(thisWeaponModel)>0)
            {
                Reload();
            }
            //auto reload when the magazine is empty
            if (readyToShoot && isShooting == false && isReloading == false && bulletsLeft <= 0)
            {
                // Reload();
            }

          /*  if (AmmoManager.Instace.ammoDisplay != null)
            {
                AmmoManager.Instace.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";
            }*/
        }
    }


    private void EnterADS() {
        animator.SetTrigger("enterADS");
        isADS = true;
        HUDManager.Instace.middleDot.SetActive(false);
        spreadIntensity = adsSpreadIntensity;
    }
    private void ExitADS()
    {
        animator.SetTrigger("exitADS");
        isADS = false;
        HUDManager.Instace.middleDot.SetActive(true);
        spreadIntensity = hipSpreadIntensity;
    }
    private void FireWeapon()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();

        if (isADS)
        {
            //recoil ads
            animator.SetTrigger("RECOIL_ADS");
          
        }
        else
        {
             animator.SetTrigger("RECOIL");

        }

        // SoundManager.Instace.shootingSound.Play();
        SoundManager.Instace.PlayShootingSound(thisWeaponModel);


        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //instatiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //poiting the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        //shoot bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        // distroy the bullet after delay
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));


        //checking if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }
        if (currentShootingMode == ShootingMode.Brust && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }

    }

    private void Reload()
    {
       // SoundManager.Instace.reloadingSound.Play();
        SoundManager.Instace.PlayReloadSound(thisWeaponModel);

       // animator.SetTrigger("RELOAD");
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    public void ReloadCompleted()
    {
        if (WeaponManager.Instace.CheckAmmoLeftFor(thisWeaponModel)>magazineSize)
        {          
             bulletsLeft = magazineSize;
            WeaponManager.Instace.DecreaseTotalAmmo(bulletsLeft, thisWeaponModel);
        }
        else
        {
            bulletsLeft = WeaponManager.Instace.CheckAmmoLeftFor(thisWeaponModel);
            WeaponManager.Instace.DecreaseTotalAmmo(bulletsLeft, thisWeaponModel);
        }
        isReloading = false;
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        //shooting from the middle of the screen to check where we pointing at

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            //hitting something
            targetPoint = hit.point;
        }
        else
        {
            //shooting at the air
            targetPoint = ray.GetPoint(100);
        }
        Vector3 direction = targetPoint - bulletSpawn.position;
        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        // returnig the shooting direction and spread
        return direction + new Vector3(0, y, z);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


}
