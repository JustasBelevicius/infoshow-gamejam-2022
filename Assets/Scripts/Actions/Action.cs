using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    [SerializeField]
    protected string debugName;

    public bool OnUpdate(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player)
    {
        if (CheckInput(controller, roomGenerator, player))
        {
            DoAction(controller);
            Debug.Log($"Performing Action [{debugName}]");
            return true;
        }
        return false;
    }

    protected abstract bool CheckInput(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player);
    protected abstract void DoAction(PlayerManager controller);
}
