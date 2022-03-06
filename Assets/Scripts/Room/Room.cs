using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{

    public const int WIDTH = 19;
    public const int HEIGHT = 13;

    private TileType[,] _tiles;
    public Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();

    public Room(TileType[,] tiles)
    {
        _tiles = tiles;
    }

    public TileType[,] GetTiles()
    {
        return _tiles;
    }

    public Room Clone()
    {
        return new Room((TileType[,])_tiles.Clone());
    }
}