using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instace { get; set; }
    public Weapon hoveredWeapon = null;

    public AmmoBox hoveredAmmoBox = null;
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

    private AmmoBox GetHoveredAmmoBox()
    {
        return hoveredAmmoBox;
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if (objectHitByRaycast.GetComponent<Weapon>()&& objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {
                hoveredWeapon = objectHitByRaycast.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;
               
               // print("weapon selected");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instace.PickupWeapon(objectHitByRaycast.gameObject);
                    //hoveredWeapon.GetComponent<Weapon>().enabled = true;
                }
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                   // hoveredWeapon.GetComponent<Weapon>().enabled = false;
                }
            }

            //ammoBox
            if (objectHitByRaycast.GetComponent<AmmoBox>() )
            {
                hoveredAmmoBox = objectHitByRaycast.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                //print("AmmoBox selected");
                    WeaponManager.Instace.PickupAmmoBox(hoveredAmmoBox);
                    Destroy(objectHitByRaycast.gameObject);
                    
                }
            }
            else
            {
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    
                }
            }
        }
      
    }
}
