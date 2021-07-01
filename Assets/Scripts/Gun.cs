using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public string gunName; // Gun name.
    public float range; // Shooting Range
    public float accuracy; // Accuracy
    public float fireRate; 
    public float reloadTime; // Reloding Spped.

    public int damage; // Gun damage.

    public int reloadBulletCount; 
    public int currentBulletCount; // Remain Gun Bullet count
    public int maxBulletCount; // Maximum Buulet
    public int carryBulletCount; // Current Bullet Count

    public float retroActionForce; // ReBund Force
    public float retroActionFineSightForce; // FinShoot ReBund Force

    public Vector3 fineSightOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlash; // FIre Flash

    public AudioClip fire_Sound; //Sound
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
