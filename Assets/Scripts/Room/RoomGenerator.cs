using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class RoomGenerator: MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerController;

    [SerializeField]
    private Tilemap tileMap;

    [SerializeField]
    private List<TileTypeToTileBase> tiles;
        
    private Dictionary<string, Room> _map = new Dictionary<string, Room>();
    private RoomLoader _roomLoader = RoomLoader.GetInstance();

    // Use this for initialization
    void Start()
    {
        AddMoreRoomsIfNeccessary();
    }

    // Update is called once per frame
    void Update()
    {
        AddMoreRoomsIfNeccessary();
    }

    private void AddMoreRoomsIfNeccessary()
    {
        var playerRoom = playerController.GetCurrentRoomPosition();
        AddRoom(playerRoom[0], playerRoom[1]);
        AddRoom(playerRoom[0] + 1, playerRoom[1]);
        AddRoom(playerRoom[0] - 1, playerRoom[1]);
        AddRoom(playerRoom[0], playerRoom[1] + 1);
        AddRoom(playerRoom[0], playerRoom[1] - 1);
        tileMap.RefreshAllTiles();
    }

    private void AddRoom(int x, int y)
    {
        if (HasRoom(x, y))
            return;
        _map.Add($"{x}_{y}", _roomLoader.GetRandomRoom());
        DrawRoom(x, y);
    }

    private bool HasRoom(int x, int y)
    {
        return _map.ContainsKey($"{x}_{y}");
    }

    private Room GetRoom(int x, int y)
    {
        if (!HasRoom(x, y))
            return null;
        return _map[$"{x}_{y}"];
    }

    private void DrawRoom(int roomX, int roomY)
    {
        if (!HasRoom(roomX, roomY))
            return;
        Room room = GetRoom(roomX, roomY);
        TileType[,] roomTiles = room.GetTiles();
        for (int y = 0; y < Room.HEIGHT; y++) {
            for (int x = 0; x < Room.WIDTH; x++) {
                Vector3Int position = new Vector3Int(roomX * Room.WIDTH + x, roomY * Room.HEIGHT + y, 0);
                var tile = GetTileBase(roomTiles[x, y]);
                if (tile == null)
                {
                    break;
                }
                tileMap.SetTile(position, tile);
            }
        }
    }

    private TileBase GetTileBase(TileType tileType)
    {
        var value = tiles.Find(map => map.tileType == tileType);
        if (value == null)
        {
            return null;
        }
        return value.tileBase;
    }
}
