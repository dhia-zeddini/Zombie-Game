using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instace { get; set; }

    public AudioSource shootingSound;
    public AudioSource reloadingSound;
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
}
