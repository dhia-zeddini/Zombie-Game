using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instace { get; set; }
    public Weapon hoveredWeapon = null;

   
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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if (objectHitByRaycast.GetComponent<Weapon>())
            {
                hoveredWeapon = objectHitByRaycast.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;
               
                print("weapon selected");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instace.PickupWeapon(objectHitByRaycast.gameObject);
                    hoveredWeapon.GetComponent<Weapon>().enabled = true;
                }
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                    hoveredWeapon.GetComponent<Weapon>().enabled = false;
                }
            }
        }
      
    }
}
