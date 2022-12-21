using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool applyCameraShake;
    [SerializeField] private int health = 50;
    [SerializeField] private bool isPlayer;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private int score = 50;
    
    private AudioPlayer _audioPlayer;
    private CameraShake _cameraShake;
    private LevelManager _levelManager;
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _levelManager = FindObjectOfType<LevelManager>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)  // check if the thing we collided with should deal damage to us
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            _audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            _scoreKeeper.UpdateScore(score);
        }
        else
        {
            _levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effectInstance.gameObject, effectInstance.main.duration + effectInstance.main.startLifetime.constantMax);
        }
    }

    private void ShakeCamera()
    {
        if (_cameraShake != null && applyCameraShake)
        {
            _cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
