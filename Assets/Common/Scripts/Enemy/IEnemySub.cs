using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySub
{
    public void OnTriggered();

    public bool OnDeactivated();
}
