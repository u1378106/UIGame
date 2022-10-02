using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Observer.Pattern
{
    [RequireComponent(typeof(Fuel))]
    public class ShipFuel : MonoBehaviour
    {
        [SerializeField] Image _fuelvalue = null;

        public Fuel Fuel { get; private set; }

        private void Awake()
        {
            Fuel = GetComponent<Fuel>();

            _fuelvalue.fillAmount = Fuel.MaxFuel;
            _fuelvalue.fillAmount = Fuel.StartingFuel;
        }

        private void OnEnable()
        {
            Fuel.Drained += OnFuleDrain;
        }

        private void OnDisable()
        {
            Fuel.Drained -= OnFuleDrain;
        }

        void OnFuleDrain(float drainAmount)
        {
            if (Fuel.CurrentFuel > 0)
            {
                Fuel.CurrentFuel -= drainAmount * Time.deltaTime;
                _fuelvalue.fillAmount = Fuel.CurrentFuel;

                if (Fuel.CurrentFuel < 0.70f && Fuel.CurrentFuel > 0.30f)
                    _fuelvalue.color = Color.yellow;
                else if (Fuel.CurrentFuel < 0.30f && Fuel.CurrentFuel >= 0f)
                    _fuelvalue.color = Color.red;
                else
                    _fuelvalue.color = Color.green;
            }

            _fuelvalue.fillAmount = Fuel.CurrentFuel;         
        }

        private void Update()
        {   
            Fuel?.DrainFuel(0.0001f);
        }
    }
}

