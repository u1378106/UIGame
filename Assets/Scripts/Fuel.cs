using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public class Fuel : MonoBehaviour
    {
        public event Action<float> Drained = delegate { };
        public event Action<float> Gained = delegate { };
        public event Action Killed = delegate { };

        [SerializeField] float _startingFuel = 1;
        public float StartingFuel => _startingFuel;

        [SerializeField] float _maxFuel = 1;
        public float MaxFuel => _maxFuel;

        [SerializeField] float _startingStars = 0;
        public float StartingStars => _startingStars;

        [SerializeField] float _maxStars = 16;
        public float MaxStars => _maxStars;

    float _currentFuel;
        public float CurrentFuel
        {
            get => _currentFuel;
            set
            {
                if(value > _maxFuel)
                {
                    value = _maxFuel;
                }
            _currentFuel = value;
            }
        }

    float _currentStars;
    public float CurrentStars
    {
        get => _currentStars;
        set
        {
            if (value > _maxStars)
            {
                value = _maxStars;
            }
            _currentStars = value;
        }
    }

        private void Awake()
        {
            CurrentFuel = _startingFuel;
            CurrentStars = _startingStars;
        }

        public void GetFuel(float amount)
        {
            CurrentFuel += amount;
            Gained.Invoke(amount);
        }

        public void DrainFuel(float amount)
        {
            CurrentFuel -= amount;
            Drained.Invoke(amount);

            if(CurrentFuel <= 0)
            {
                Kill();
            }
        }

        public void GetStars(float amount)
        {
            CurrentStars += amount;
            Gained.Invoke(amount);
        }

    public void Kill()
        {
            Killed.Invoke();
            gameObject.SetActive(false);
        }
    }


