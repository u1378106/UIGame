using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Base_Script : MonoBehaviour
{
    public GameObject mainProjectile;
    public ParticleSystem mainParticleSystem;

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            mainProjectile.SetActive(true);
        }
        if (mainParticleSystem.IsAlive() == false)
            mainProjectile.SetActive(false);

    }
}
