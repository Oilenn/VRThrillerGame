using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    private bool isQuestDone;

    public void Update()
    {
        if (!isQuestDone)
        {
            StepOn();
        }
    }

    public bool DoneQuest()
    {
        return isQuestDone = true;
    }

    public abstract void StepOn();
}
