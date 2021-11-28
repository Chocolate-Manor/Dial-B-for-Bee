using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void OnHitBehavior(Collision2D other);

    void OnHitDamage(Collision2D other);
}
