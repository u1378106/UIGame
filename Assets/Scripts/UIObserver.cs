using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


    public class UIObserver : MonoBehaviour
    {
        [SerializeField] Fuel _fuelToObserve = null;

        [SerializeField]
        Image gameOverScreen;

        [SerializeField]
        Image winScreen;

        [SerializeField]
        TextMeshProUGUI fuelCollected;

        [SerializeField]
        TextMeshProUGUI score;

    [SerializeField] 
        float _textPopupDuration = 1;

        Fuel _observedFuel = null;
        Coroutine _popupRoutine = null;

        private void Awake()
        {
            StartObservingFuel(_fuelToObserve);
        }

        private void Start()
        {
            fuelCollected.text = "Parts collected : " + _observedFuel.CurrentStars + "/16";
        }

    public void StartObservingFuel(Fuel newHealthToObserver)
        {
        _observedFuel = newHealthToObserver;

        _observedFuel.Gained += OnObservedFuelGained;
        _observedFuel.Drained += OnObservedFuelDrained;
        _observedFuel.Killed += OnObservedFuelEmpty;
        }

        public void StopObservingHealth()
        {
        _observedFuel.Drained -= OnObservedFuelDrained;
        _observedFuel.Killed -= OnObservedFuelEmpty;

        _observedFuel = null;
        }

        void OnObservedFuelDrained(float drained)
        {
            
        }

        void OnObservedFuelGained(float gained)
        {
            fuelCollected.text = "Parts collected : " + _observedFuel.CurrentStars + "/16";
        }

    IEnumerator ImagePopup()
        {
            yield return null;
            gameOverScreen.gameObject.SetActive(true);
        }

    void OnObservedFuelEmpty()
        {
            if (_popupRoutine != null)
                StopCoroutine(_popupRoutine);
            _popupRoutine = StartCoroutine(ImagePopup());
            StopObservingHealth();
        }
    }

