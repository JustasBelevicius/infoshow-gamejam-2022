using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{

    public const int WIDTH = 19;
    public const int HEIGHT = 13;

    private TileType[,] _tiles;

    public Room(TileType[,] tiles)
    {
        _tiles = tiles;
    }

    public TileType[,] GetTiles()
    {
        return _tiles;
    }
}