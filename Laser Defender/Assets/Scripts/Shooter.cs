using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float baseFiringRate = 0.2f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    
    [Header("AI")]
    [SerializeField] private bool useAI;
    [SerializeField] private float firingRateVariance = 0f;
    [SerializeField] private float minFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    private AudioPlayer _audioPlayer;
    private Coroutine _firingCoroutine;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && _firingCoroutine == null)
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && _firingCoroutine != null)
        {
            StopCoroutine(_firingCoroutine);
            _firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rigidbody2D = projectileInstance.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = transform.up * projectileSpeed;
            }
            Destroy(projectileInstance, projectileLifetime);

            float timeToNextProjectile =
                Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);
            
            _audioPlayer.PlayShootingClip();
            
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
