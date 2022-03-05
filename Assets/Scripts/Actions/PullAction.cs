using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PullAction", menuName = "Actions/PullAction", order = 1)]
public class PullAction : Action
{
    [SerializeField]
    List<TileType> allowedIntermediateTiles;

    int targetDistance;

    protected override bool CheckInput(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player)
    {
        if(!ValidateTile(controller, roomGenerator, player, 0, allowedCurrentTiles))
        {
            return false;
        }
        targetDistance = 1;
        for (; targetDistance <= actionDistance; targetDistance++)
        {
            if (targetDistance > 1 && ValidateTile(controller, roomGenerator, player, targetDistance, allowedTargetTiles))
            {
                return true;
            }
            if (!ValidateTile(controller, roomGenerator, player, targetDistance, allowedIntermediateTiles))
            {
                return false;
            }
        }
        return false;
    }

    protected override void DoAction(PlayerManager controller)
    {
        controller.PlayAudio(audio);
        return;
    }

    protected override Direction GetDirection(PlayerManager controller)
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            return Direction.UP;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            return Direction.LEFT;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            return Direction.DOWN;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            return Direction.RIGHT;
        }
        return Direction.NONE;
    }
}
