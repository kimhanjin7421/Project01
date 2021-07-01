using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun;

    private float currentFireRate;

    private bool isReload = false;
    public bool isFineSightMode = false;

    // original position
    [SerializeField]
    private Vector3 originPos;

    private AudioSource audioSource;



    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
    }



    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime; //== 1/60 frame
    }
    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }
    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
            {
              //  CancelFineSight();
              //  StartCoroutine(ReloadCoroutine());
            }


        }
    }
    private void Shoot()
    {
       // currentGun.currentBulletCount--;
       // currentFireRate = currentGun.fireRate; // Shooting Velocity 
      //  PlaySE(currentGun.fire_Sound);
      currentGun.muzzleFlash.Play();
     //   StopAllCoroutines();
      //  StartCoroutine(RetroActionCoroutine());

        Debug.Log("ÃÑ¾Ë ¹ß»çÇÔ");

    }
}
