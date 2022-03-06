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

    [SerializeField]
    private List<GameObject> food;

    [SerializeField]
    private GameObject obstacle;

    private Dictionary<string, Room> _map = new Dictionary<string, Room>();
    private RoomLoader _roomLoader = RoomLoader.GetInstance();
    private Config config = ConfigLoader.getConfig();

    // Use this for initialization
    void Start()
    {
        AddMoreRoomsIfNeccessary();
    }

    // Update is called once per frame
    void Update()
    {
        AddMoreRoomsIfNeccessary();
        DrawRelevantRooms();
    }

    private void AddMoreRoomsIfNeccessary()
    {
        var playerRoom = playerController.GetCurrentRoomPosition();
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                AddRoom(playerRoom[0] + x, playerRoom[1] + y);
            }
        }
    }

    private void AddRoom(int x, int y)
    {
        if (HasRoom(x, y))
            return;
        var room = _roomLoader.GetRandomRoom();
        AddInteractables(room, x, y);
        _map.Add($"{x}_{y}", room);
    }

    private bool HasRoom(int x, int y)
    {
        return _map.ContainsKey($"{x}_{y}");
    }

    public Room GetRoom(int x, int y)
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

    private void DrawRelevantRooms()
    {
        tileMap.ClearAllTiles();
        var playerRoom = playerController.GetCurrentRoomPosition();
        for(int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                DrawRoom(playerRoom[0] + x, playerRoom[1] + y);
            }
        }
        tileMap.RefreshAllTiles();
    }

    private void AddInteractables(Room room, int roomX, int roomY)
    {
        var roomTiles = room.GetTiles();
        for (int y = 0; y < Room.HEIGHT; y++)
        {
            for (int x = 0; x < Room.WIDTH; x++)
            {
                if (roomTiles[x, y] == TileType.OBSTACLE && !room.gameObjects.ContainsKey($"{x}_{y}")) {
                    InstantiateInRoom(room, roomX, roomY, x, y, obstacle);
                }
            }
        }
        int foodCount = 0;
        while (foodCount < config.foodItemsPerRoom)
        {
            var x = Random.Range(0, Room.WIDTH);
            var y = Random.Range(0, Room.HEIGHT);
            if (roomTiles[x, y] == TileType.GROUND && !room.gameObjects.ContainsKey($"{x}_{y}"))
            {
                InstantiateInRoom(room, roomX, roomY, x, y, food[Random.Range(0, food.Count)]);
                foodCount++;
            }
        }
    }

    private void InstantiateInRoom(Room room, int roomX, int roomY, int x, int y, GameObject gameObject)
    {
        var objectInstance = Instantiate(gameObject, new Vector3(roomX * Room.WIDTH + x + .5f, roomY * Room.HEIGHT + y + .5f, -.1f), Quaternion.identity);
        objectInstance.name = $"{x}_{y}-{gameObject.name}";
        room.gameObjects.Add($"{x}_{y}", objectInstance);
    }
}
