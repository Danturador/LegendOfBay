using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthBarImg;
    [SerializeField] private PlayerHealth playerHealth;
    private Camera _camera;

    private void Awake()
    {
        playerHealth.HealthChanged += OnHealthChanger;
        _camera = Camera.main;
    }

    private void OnDestroy()
    {
        playerHealth.HealthChanged -= OnHealthChanger;
    }

    private void OnHealthChanger(float valueAsPercantage)
    {
        _healthBarImg.fillAmount = valueAsPercantage;
    }
}

