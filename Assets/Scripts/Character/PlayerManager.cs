using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public List<Action> actions;

    [SerializeField]
    public PlayerData playerData;

    void Awake()
    {
        playerData.foodLevel = ConfigLoader.getConfig().startingFood;
        transform.position = new Vector3(playerData.x - .5f, playerData.y - .5f, transform.position.z);
    }

    void Update()
    {
        foreach(Action action in actions)
        {
            action.OnUpdate(this);
        }
        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position = new Vector3(playerData.x - .5f, playerData.y - .5f, transform.position.z);
    }

    public void Move(Direction direction)
    {
        switch(direction)
        {
            case Direction.UP:
                playerData.y += 1;
                break;
            case Direction.RIGHT:
                playerData.x += 1;
                break;
            case Direction.DOWN:
                playerData.y -= 1;
                break;
            case Direction.LEFT:
                playerData.x -= 1;
                break;
        }
    }

    public int[] GetCurrentRoomPosition()
    {
        return new int[]{ Mathf.RoundToInt(((playerData.x - .5f) / (float)Room.WIDTH) - .5f), Mathf.RoundToInt(((playerData.y - .5f )/ (float)Room.HEIGHT) - .5f) };
    }
}
