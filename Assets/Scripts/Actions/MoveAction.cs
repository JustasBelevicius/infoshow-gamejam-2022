using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MoveAction", menuName = "Actions/MoveAction", order = 1)]
public class MoveAction : Action
{
    [SerializeField]
    List<TileType> toTile;

    [SerializeField]
    List<TileType> fromTile;

    [SerializeField]
    int moveSpeed;

    protected override bool CheckInput(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player)
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        if (vertical == 0 && horizontal == 0)
            return false;
        return ValidateMove(controller, roomGenerator, player);

    }

    private bool ValidateMove(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player)
    {
        var fromRoomPosition = player.GetCurrentRoomPosition();
        var fromRoom = roomGenerator.GetRoom(fromRoomPosition[0], fromRoomPosition[1]);
        var playerData = controller.GetNextPlayerPosition(GetDirection(controller), moveSpeed);
        var toRoomPosition = playerData.GetCurrentRoomPosition();
        var toRoom = roomGenerator.GetRoom(toRoomPosition[0], toRoomPosition[1]);
        return fromTile.Contains(fromRoom.GetTiles()[Mod(player.x - 1, Room.WIDTH), Mod(player.y - 1, Room.HEIGHT)])
            && toTile.Contains(toRoom.GetTiles()[Mod(playerData.x - 1, Room.WIDTH), Mod(playerData.y - 1, Room.HEIGHT)]);
    }

    private int Mod(int a, int n)
    {
        var pos = a % n;
        return (n + a % n) % n;
    }

    protected override void DoAction(PlayerManager controller)
    {
        controller.Move(GetDirection(controller), moveSpeed);
    }
    private Direction GetDirection(PlayerManager controller)
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            // Move vertically
            return vertical < 0 ? Direction.DOWN : Direction.UP;
        } else
        {
            // Move horizontally
            return horizontal < 0 ? Direction.LEFT : Direction.RIGHT;
        }
    }

}
