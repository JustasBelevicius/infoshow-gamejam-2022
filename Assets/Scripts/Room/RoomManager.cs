using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoomManager
{
    private const string ROOM_FOLDER = "Rooms";
    private List<Room> _rooms;
    private static RoomManager _instance;

    public static RoomManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new RoomManager();
        }
        return _instance;
    }

    private RoomManager()
    {
        _rooms = _loadRooms();
    }

    private List<Room> _loadRooms()
    {
        var rooms = new List<Room>();
        var configPath = Path.Combine(Application.streamingAssetsPath, ROOM_FOLDER);
        foreach (string path in System.IO.Directory.GetFiles(configPath))
        {
            if (path.EndsWith(".csv"))
            {
                rooms.Add(_loadRoomFromPath(path));
            }
        }
        return rooms;
    }

    private Room _loadRoomFromPath(string path)
    {
        StreamReader reader = new StreamReader(path);
        TileType[,] tiles = new TileType[Room.WIDTH, Room.HEIGHT];
        for (int y = 0; y < Room.HEIGHT; y++)
        {
            var line = reader.ReadLine();
            var elements = line.Split(',');
            for (int x = 0; x < Room.WIDTH; x++)
            {
                int tileValue = Int32.Parse(elements[x]);
                tiles[x, y] = (TileType)tileValue;
            }
        }
        return new Room(tiles);
    }

    public Room GetRandomRoom()
    {
        int roomId = UnityEngine.Random.Range(0, _rooms.Count);
        return _rooms[roomId];
    }
}
