using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthManager, IDamageable
{
    public float invulnerabilityDuration = 1.0f;
    private bool isInvulnerable = false;
    public override void TakeDamage(int damage)
    {
        if (isInvulnerable == false) 
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
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
