using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public List<Action> actions;

    private PlayerData playerData = new PlayerData();

    [SerializeField]
    public RoomGenerator generator;

    [SerializeField]
    public AudioSource source;

    void Awake()
    {
        playerData.foodLevel = ConfigLoader.getConfig().startingFood;
        playerData.x = Mathf.CeilToInt(Room.WIDTH / 2) + 1;
        playerData.y = Mathf.CeilToInt(Room.HEIGHT / 2) + 1;
        transform.position = new Vector3(playerData.x - .5f, playerData.y - .5f, transform.position.z);
    }

    void Update()
    {
        foreach (Action action in actions)
        {
            if (action.OnUpdate(this, generator, playerData))
            {
                break;
            }
        }

        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position = new Vector3(playerData.x - .5f, playerData.y - .5f, transform.position.z);
    }

    public void Move(Direction direction, int ammount)
    {
        playerData = GetNextPlayerPosition(direction, ammount);
    }

    public PlayerData GetNextPlayerPosition(Direction direction, int ammount)
    {
        PlayerData player = playerData.Clone();
        switch (direction)
        {
            case Direction.UP:
                player.y += ammount;
                break;
            case Direction.RIGHT:
                player.x += ammount;
                break;
            case Direction.DOWN:
                player.y -= ammount;
                break;
            case Direction.LEFT:
                player.x -= ammount;
                break;
        }
        return player;
    }

    public int[] GetCurrentRoomPosition()
    {
        return playerData.GetCurrentRoomPosition();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(audioClip);
    }
}
