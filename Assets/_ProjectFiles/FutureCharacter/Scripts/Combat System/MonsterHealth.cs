using System;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.SoundContainer;
using UnityEngine;
using EnemyType = _ProjectFiles.Enemy.Scripts.Core.EnemyType;
using Random = UnityEngine.Random;
using SoundType = _ProjectFiles.SoundContainer.SoundType;

public class MonsterHealth : HealthManager, IDamageable
{
    [SerializeField] private GameObject _hpBarGameobject;
    [SerializeField] private DestroyableSound _destroyableSound;
    [SerializeField] private EnemyContainer _container;
    [SerializeField] private SoundContainer _soundContainer;
    public Action OnDeath { get; set; }

    private void Start()
    {
        _hpBarGameobject = GetComponentInChildren<Canvas>()?.gameObject;
        if (_hpBarGameobject != null) _hpBarGameobject.SetActive(false);
    }

    public override void TakeDamage(int damage)
    {
        var sound = Instantiate(_destroyableSound);

        switch (_container.Profile.EnemyInfo.Type)
        {
            case EnemyType.Kirin:
                _soundContainer.SoundsStorage.TryGetValue(SoundType.KirinDamageTaken, out var kirinClip);
                sound.PlaySound(kirinClip);
                break;

            case EnemyType.Shishi:
                _soundContainer.SoundsStorage.TryGetValue(SoundType.ShishiDamageTaken1, out var shishiClip);
                sound.PlaySound(shishiClip);
                break;

            case EnemyType.Hundun:
                var sounds = new[]
                    { SoundType.HundunDamageTaken1, SoundType.HundunDamageTaken3, SoundType.HundunDamageTaken3 };

                var randomSound = sounds[Random.Range(0, sounds.Length)];

                _soundContainer.SoundsStorage.TryGetValue(randomSound, out var hundunClip);
                sound.PlaySound(hundunClip);
                break;
        }

        base.TakeDamage(damage);
        var curentHealthPercantage = (float)currentHealth / maxHealth;
        HealthChanged?.Invoke(curentHealthPercantage);
    }

    public event Action<float> HealthChanged;

    protected override void HealthUpdate()
    {
        base.HealthUpdate();
        if (_hpBarGameobject != null && _hpBarGameobject.activeSelf == false) _hpBarGameobject.SetActive(true);
    }

    protected override void Die()
    {
        OnDeath?.Invoke();
        OnDeath = null;
        var sound = Instantiate(_destroyableSound);

        switch (_container.Profile.EnemyInfo.Type)
        {
            case EnemyType.Kirin:
                _soundContainer.SoundsStorage.TryGetValue(SoundType.KirinDeath, out var kirinClip);
                sound.PlaySound(kirinClip);
                break;

            case EnemyType.Shishi:
                _soundContainer.SoundsStorage.TryGetValue(SoundType.ShishiDeath, out var shishiClip);
                sound.PlaySound(shishiClip);
                break;

            case EnemyType.Hundun:
                _soundContainer.SoundsStorage.TryGetValue(SoundType.HundunDeath, out var hundunDeath);
                sound.PlaySound(hundunDeath);
                break;
        }

        Destroy(gameObject);
    }
}