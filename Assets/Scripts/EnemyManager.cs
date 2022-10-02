using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool isRotateAround;

    public GameObject star3, destroyPrefab, mainCam;

    AudioManager audioManager;

    public Fuel fuel { get; private set; }

    private void Start()
    {
        fuel = GameObject.FindObjectOfType<Fuel>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Projectile"))
        {
            fuel.GetScore(10);
            audioManager.enemyDestroy.Play();
            Instantiate(destroyPrefab, this.transform.localPosition, Quaternion.identity);
            mainCam.GetComponent<CameraShake>().enabled = true;

            Destroy(this.gameObject);
        }

        if(other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Game Over !!!!");
            audioManager.enemyDestroy.Play();
        }
    }

    private void Update()
    {
        if (isRotateAround)
        {
            if(star3)
            transform.RotateAround(star3.transform.position, Vector3.up, 50 * Time.deltaTime);
        }
    }
}
