using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Action : ScriptableObject
{
    [SerializeField]
    protected AudioClip audio;

    [SerializeField]
    protected string debugName;

    [SerializeField]
    public int id;

    [SerializeField]
    protected List<TileType> allowedTargetTiles;

    [SerializeField]
    protected List<TileType> allowedCurrentTiles;

    [SerializeField]
    protected int actionDistance;

    public bool OnUpdate(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player)
    {
        if (GetDirection(controller) == Direction.NONE)
        {
            return false;
        }
        if (CheckInput(controller, roomGenerator, player))
        {
            DoAction(controller);
            return true;
        }
        return false;
    }

    protected abstract bool CheckInput(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player);
    protected abstract void DoAction(PlayerManager controller);
    protected abstract Direction GetDirection(PlayerManager controller);

    protected bool ValidateTiles(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player)
    {
        return ValidateTile(controller, roomGenerator, player, 0, allowedCurrentTiles)
            && ValidateTile(controller, roomGenerator, player, actionDistance, allowedTargetTiles);
    }

    protected bool ValidateTile(PlayerManager controller, RoomGenerator roomGenerator, PlayerData player, int distance, List<TileType> allowedTiles) {
        var playerData = controller.GetNextPlayerPosition(GetDirection(controller), distance);
        var roomPosition = playerData.GetCurrentRoomPosition();
        var room = roomGenerator.GetRoom(roomPosition[0], roomPosition[1]);
        var playerPosition = playerData.GetPlayerPositionInRoom();
        return allowedTiles.Contains(room.GetTiles()[playerPosition.x, playerPosition.y]);
    }
}
