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
        _observedFuel.Scored += OnObservedScored;
        _observedFuel.Killed += OnObservedFuelEmpty;
        }

        public void StopObservingHealth()
        {
        _observedFuel.Killed -= OnObservedFuelEmpty;

        _observedFuel = null;
        }

        void OnObservedFuelGained(float gained)
        {
            fuelCollected.text = "Parts collected : " + _observedFuel.CurrentStars + "/16";

        if (_observedFuel.CurrentStars == _observedFuel.MaxStars)
            winScreen.gameObject.SetActive(true);
        }

        void OnObservedScored(float scored)
        {
        score.text = "Score : " + _observedFuel.CurrentScore;
        }

    IEnumerator LoseImagePopup()
        {
            yield return null;
            gameOverScreen.gameObject.SetActive(true);
        }

    void OnObservedFuelEmpty()
        {
            if (_popupRoutine != null)
                StopCoroutine(_popupRoutine);
            _popupRoutine = StartCoroutine(LoseImagePopup());
            StopObservingHealth();
        }
    }

