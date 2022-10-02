using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Observer.Pattern;

public class _SpaceshipController : MonoBehaviour
{
    float speed;
    public int cruiseSpeed;
    float deltaSpeed;
    public int minSpeed;
    public int maxSpeed;
    float accel, decel;
    public GameObject projectile;

    Vector3 angVel;
    Vector3 shipRot;
    public int sensitivity;

    public Vector3 cameraOffset;

    FuelManager fuelManager;

    float height;

    Collectable collectable;

    public GameObject warningText;

    public Fuel fuel { get; private set; }

    void Start()
    {
        collectable = GameObject.FindObjectOfType<Collectable>();
        fuel = GameObject.FindObjectOfType<Fuel>();

        speed = cruiseSpeed;
        height = this.transform.position.y;
        warningText.SetActive(false);
    }

    void FixedUpdate()
    {
        shipRot = transform.GetChild(1).localEulerAngles; 

        if (shipRot.x > 180) shipRot.x -= 360;
        if (shipRot.y > 180) shipRot.y -= 360;
        if (shipRot.z > 180) shipRot.z -= 360;

        if(Input.GetKey(KeyCode.W))
        {
            speed += accel * 0.8f * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            speed -= accel * 0.1f * Time.fixedDeltaTime;
        }

        
        float turn = Input.GetAxis("Horizontal") * Mathf.Abs(Input.GetAxis("Horizontal")) * sensitivity * Time.fixedDeltaTime;
        angVel.y += turn * .5f;
       
        angVel /= 1 + deltaSpeed * .001f;

        angVel -= angVel.normalized * angVel.sqrMagnitude * .08f * Time.fixedDeltaTime;
      
        transform.GetChild(1).Rotate(angVel * Time.fixedDeltaTime);

        transform.GetChild(1).Rotate(-shipRot.normalized * .015f * (shipRot.sqrMagnitude + 500) * (1 + speed / maxSpeed) * Time.fixedDeltaTime);

        deltaSpeed = speed - cruiseSpeed;
     
        decel = speed - minSpeed;
        accel = maxSpeed - speed;

       
        if (Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.LeftShift))
            speed += accel * 2 * Time.fixedDeltaTime;
        
        else if (Mathf.Abs(deltaSpeed) > .1f)
            speed -= Mathf.Clamp(deltaSpeed * Mathf.Abs(deltaSpeed), -30, 100) * Time.fixedDeltaTime;

        transform.GetChild(0).localPosition = cameraOffset + new Vector3(0, 0, -deltaSpeed * .02f);


        float sqrOffset = transform.GetChild(1).localPosition.sqrMagnitude;
        Vector3 offsetDir = transform.GetChild(1).localPosition.normalized;


       
        transform.GetChild(1).Translate(-offsetDir * sqrOffset * 20 * Time.fixedDeltaTime);
     
        transform.Translate((offsetDir * sqrOffset * 50 + transform.GetChild(1).forward * speed) * Time.fixedDeltaTime, Space.World);

        
        transform.Rotate(shipRot.x * Time.fixedDeltaTime, (shipRot.y * Mathf.Abs(shipRot.y) * .02f) * Time.fixedDeltaTime, shipRot.z * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

            if (Input.GetKeyDown(KeyCode.C))
        {        
            FireProjectile();
        }

        height = this.transform.position.y;

        if(height > 10.0f)
        {
            if (height > 40.0f)
                warningText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                warningText.SetActive(false);
                this.GetComponentInChildren<Rigidbody>().isKinematic = true;
                this.transform.position = collectable.lastCheckPos + new Vector3(5,5.83f,0);
                this.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }
        }
    }

    void FireProjectile()
    {
        GameObject shoot = GameObject.Instantiate(projectile, this.transform);
        shoot.GetComponent<Rigidbody>().AddForce(this.transform.forward * 400f);
        fuel.DrainFuel(0.025f);
    }
}