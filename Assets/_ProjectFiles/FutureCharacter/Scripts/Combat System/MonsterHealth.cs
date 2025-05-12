using System;
using UnityEngine;

public class MonsterHealth : HealthManager, IDamageable
{
    [SerializeField] private GameObject _hpBarGameobject;
    public event Action<float> HealthChanged;
    public Action OnDeath { get; set; }

    private void Start()
    {
        _hpBarGameobject = GetComponentInChildren<Canvas>()?.gameObject;
        if(_hpBarGameobject != null)
        {
            _hpBarGameobject.SetActive(false);
        } 
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        float curentHealthPercantage = (float)currentHealth / maxHealth;
        HealthChanged?.Invoke(curentHealthPercantage);
    }
    protected override void HealthUpdate()
    {
        base.HealthUpdate();
        if(_hpBarGameobject != null && _hpBarGameobject.activeSelf == false)
        {
            _hpBarGameobject.SetActive(true);
        }
    }

    protected override void Die()
    {
        OnDeath?.Invoke();
        OnDeath = null;
        Destroy(this.gameObject);
    }
}