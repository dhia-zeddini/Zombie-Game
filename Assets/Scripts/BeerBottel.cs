using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottel : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();

    public void Shatter()
    {
        foreach(Rigidbody part in allParts)
        {
            //isKinematic: controls whether phisucs affects the rigidbody
            //if its enabled forces or collisions or joints will not affect the rigidbody anymore

            part.isKinematic = false;
        }
    }



}
