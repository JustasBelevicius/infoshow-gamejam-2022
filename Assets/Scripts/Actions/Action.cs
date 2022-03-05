using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    [SerializeField]
    private float inputTimeout = .3f;
    private float timer = 0;

    public void OnUpdate(PlayerController controller)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        if (CheckInput(controller))
        {
            DoAction(controller);
            timer = inputTimeout;
        }
    }

    protected abstract bool CheckInput(PlayerController controller);
    protected abstract void DoAction(PlayerController controller);
}
