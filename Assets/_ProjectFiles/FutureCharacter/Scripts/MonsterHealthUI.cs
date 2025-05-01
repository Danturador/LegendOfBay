using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthBarImg;
    [SerializeField] private MonsterHealth monsterHealth;
    [SerializeField] private Gradient _healthBarGradient;
    private Camera _camera;

    private void Awake()
    {
        monsterHealth = GetComponentInParent<MonsterHealth>();
        _healthBarImg = GetComponent<Image>();
        monsterHealth.HealthChanged += OnHealthChanger;
        _camera = Camera.main;
    }

    private void OnDestroy()
    {
        monsterHealth.HealthChanged -= OnHealthChanger;
    }

    private void OnHealthChanger(float valueAsPercantage)
    {
        _healthBarImg.fillAmount = valueAsPercantage;
        //_healthBarImg.color = _healthBarGradient.Evaluate(_healthBarImg.fillAmount);
    }
}
