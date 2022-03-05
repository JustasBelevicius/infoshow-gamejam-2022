using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PushAction", menuName = "Actions/PushAction", order = 1)]
public class PushAction : Action
{
    [SerializeField]
    List<TileType> allowedDestinationTiles;

    int targetDistance;

    protected override bool CheckInput(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player)
    {
        if (!ValidateTiles(controller, roomGenerator, player))
        {
            return false;
        }
        return ValidateTile(controller, roomGenerator, player, 2, allowedDestinationTiles);
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
