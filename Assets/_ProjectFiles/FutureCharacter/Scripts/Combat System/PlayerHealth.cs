using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class PlayerHealth : HealthManager
{
    public float invulnerabilityDuration = 1.0f;
    private bool isInvulnerable = false;
    private ParticleSystem _particleSystem;

    public event Action<float> HealthChanged;

    public override void TakeDamage(int damage)
    {
        if (isInvulnerable == false) 
        {
            currentHealth -= damage;
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            StartCoroutine(InvulnerabilityCoroutine());
            float curentHealthPercantage = (float) currentHealth/maxHealth;
            HealthChanged?.Invoke(curentHealthPercantage);
            
            if (currentHealth <= 0)
            {
                HealthChanged?.Invoke(0);
                Die();
            }
        }
        
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    protected override void Die()
    {

        Debug.Log("Player Die");
        
    }
}
