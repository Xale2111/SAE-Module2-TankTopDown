using System;
using UnityEngine;


public class Impact : MonoBehaviour
{
        [SerializeField] private ParticleSystem _particleSystem;

        void Start() => Destroy(gameObject,_particleSystem.main.duration *0.95f);
}
