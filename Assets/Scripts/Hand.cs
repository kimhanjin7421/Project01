using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public string handName; // Nuccle or Bare hand.
    public float range; // Attack Range 
    public int damage; // Attack damage
    public float workSpeed; // Working Speed
    public float attackDelay; // Attack Delay
    public float attackDelayA; // Attack Active .
    public float attackDelayB; // Attack Disabled

    public Animator anim;// Animation

    
}
