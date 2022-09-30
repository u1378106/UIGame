using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField]
    private Image fuelMeter;

    [SerializeField]
    public float fuelAmount = 1;

    private float kFuelLoss = 0f;

    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        kFuelLoss = 0.01f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameStartManager.isGameStart)
        {
            if (fuelAmount > 0)
            {
                fuelAmount -= kFuelLoss * Time.deltaTime;
                fuelMeter.fillAmount = fuelAmount;

                if (fuelAmount < 0.70f && fuelAmount > 0.30f)
                    fuelMeter.color = Color.yellow;
                else if (fuelAmount < 0.30f && fuelAmount >= 0f)
                    fuelMeter.color = Color.red;
                else
                    fuelMeter.color = Color.green;
            }
            else
            {
                gameOver.SetActive(true);
            }
        }
    }
}
