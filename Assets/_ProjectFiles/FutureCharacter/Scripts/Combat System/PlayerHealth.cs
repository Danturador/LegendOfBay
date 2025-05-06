using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using _ProjectFiles.Menu.InGameMenuButtons;
using Zenject;

public class PlayerHealth : HealthManager
{
    public float invulnerabilityDuration = 1.0f;
    private bool isInvulnerable = false;
    private ParticleSystem _particleSystem;
    [SerializeField] private Animator _animator;
    private bool _die = false;
    [SerializeField] GameObject enableMenu;
    [Inject] private InputController inputController;

    public event Action<float> HealthChanged;

    private void Start()
    {
        _animator = GetComponent<Animator>();
      
    }
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
    private IEnumerator PlayerDieCoroutine()
    {
       
        yield return new WaitForSeconds(2f);
        if (enableMenu != null)
        {
            enableMenu.SetActive(true);
        }
    }

    protected override void Die()
    {
        if (_die == false)
        {
            _die = true;
            _animator.SetTrigger("PlayerDie");
            inputController.Gameplay.Disable();
            StartCoroutine(PlayerDieCoroutine());
            Debug.Log("Player Die");
        }
    }
}
