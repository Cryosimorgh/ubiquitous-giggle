using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackMessenger : MonoBehaviour
{
    public void OnKnockbackFinished()
    {
        var enemyBase = this.transform.parent.GetComponent<EnemyBase>();
        enemyBase.OnKnockbackFinish();
    }
}
