using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectable : MonoBehaviour
{
    public static int collectionCounter = 0;

    public Vector3 lastCheckPos;

    FuelManager fuelManager;
    AudioManager audioManager;

    public GameObject winScreen, runeVfx;

    public Fuel fuel { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        fuel = GameObject.FindObjectOfType<Fuel>();

        fuelManager = GameObject.FindObjectOfType<FuelManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("checkpoint"))
        {          
           
            lastCheckPos = other.transform.position;
            StartCoroutine(HandleCheckpointCollider(other.gameObject));      
            //other.gameObject.tag = null;
            //Destroy(other.gameObject);
        }

        if (other.gameObject.tag.Equals("collectable"))
        {
            collectionCounter++;
            ScoreManager.score += 20;
            Debug.Log("Collection counter : " + collectionCounter);

            fuel.GetFuel(1);
            fuel.GetStars(1);
            fuel.CurrentStars = collectionCounter;

            audioManager.coinAudio.Play();

            Destroy(other.gameObject);
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (collectionCounter == 16)
            winScreen.SetActive(true);
    }

    IEnumerator HandleCheckpointCollider(GameObject other)
    {
        other.GetComponent<Collider>().enabled = false;
        audioManager.checkpointAudio.Play();
        GameObject rune = Instantiate(runeVfx, other.transform.localPosition, Quaternion.identity, other.transform);
        rune.transform.localScale = new Vector3(4, 4, 4);
        yield return new WaitForSeconds(10f);
        other.GetComponent<Collider>().enabled = true;
    }
}
