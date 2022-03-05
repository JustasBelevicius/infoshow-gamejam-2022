using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "Actions/MoveAction", order = 1)]
public class MoveAction : Action
{

    protected override bool CheckInput(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player)
    {
        return ValidateTiles(controller, roomGenerator, player);
    }

    protected override void DoAction(PlayerManager controller)
    {
        controller.Move(GetDirection(controller), actionDistance);
        controller.PlayAudio(audio);
    }
    protected override Direction GetDirection(PlayerManager controller)
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            return Direction.UP;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            return Direction.LEFT;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            return Direction.DOWN;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            return Direction.RIGHT;
        }
        return Direction.NONE;
    }
}
