using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterHealth : HealthManager, IDamageable
{
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    protected override void HealthUpdate()
    {
        base.HealthUpdate();
    }

    protected override void Die()
    {
        Debug.Log("Monster Die");
        Destroy(this.gameObject);
    }
}
