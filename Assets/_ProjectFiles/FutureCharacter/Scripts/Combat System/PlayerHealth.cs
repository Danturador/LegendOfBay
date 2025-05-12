using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using Zenject;

public class PlayerHealth : HealthManager
{
    public float invulnerabilityDuration = 1.0f;
    private bool isInvulnerable = false;
    [SerializeField] private ParticleSystem _dieEffect;
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
	public void UpdateHealth()
	{
		currentHealth = maxHealth;
		HealthChanged?.Invoke(currentHealth);
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
            Transform diePosition = gameObject.transform;
            _die = true;
            _animator.SetTrigger("PlayerDie");
            inputController.Gameplay.Disable();
            ParticleSystem effect = Instantiate(_dieEffect,diePosition.position, Quaternion.identity);
            gameObject.GetComponentInParent<BoxCollider2D>().enabled = false;
            StartCoroutine(PlayerDieCoroutine());
            Debug.Log("Player Die");
        }
    }
}
