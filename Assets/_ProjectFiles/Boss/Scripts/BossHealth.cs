using System;

public class BossHealth : HealthManager, IDamageable
{
	public bool isHumanForm;
	public event Action<float> HealthChanged;
	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);
		var curentHealthPercantage = (float)currentHealth / maxHealth;
		HealthChanged?.Invoke(curentHealthPercantage);
	}
	protected override void Die()
	{
		if (isHumanForm)
		{
			Destroy(this.gameObject);
		}
	}
}