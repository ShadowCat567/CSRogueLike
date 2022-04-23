using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyBeh enemy);
    public abstract void UpdateState(EnemyBeh enemy);
    public abstract void ExitState(EnemyBeh enemy);
}
