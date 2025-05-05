public class BossHealth : MonsterHealth, IDamageable
{
	public bool isHumanForm;
	protected override void Die()
	{
		if (isHumanForm)
		{
			Destroy(this.gameObject);
		}
	}
}