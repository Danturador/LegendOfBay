using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerHealth : HealthManager
{
    public float invulnerabilityDuration = 1.0f;
    private bool isInvulnerable = false;
    private ParticleSystem _particleSystem;


    public override void TakeDamage(int damage)
    {
        if (isInvulnerable == false) 
        {
            if(isInvulnerable == false)
            {

                currentHealth -= damage;
                CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
                StartCoroutine(InvulnerabilityCoroutine());
            }
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
      //  SceneManager.LoadScene(1);
    }
}
