using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public List<Action> actions;

    private PlayerData playerData = new PlayerData();

    [SerializeField]
    public RoomGenerator generator;

    [SerializeField]
    public AudioSource source;

    [SerializeField]
    public LifeBar lifeBar;

    [SerializeField]
    public Animator uiAnimator;

    [SerializeField]
    public List<Button> actionButtons;

    [SerializeField]
    public AudioClip eatSound;

    Config config = ConfigLoader.getConfig();
    bool mutating = false;

    void Awake()
    {
        playerData.foodLevel = config.startingFood;
        playerData.x = Mathf.CeilToInt(Room.WIDTH / 2) + 1;
        playerData.y = Mathf.CeilToInt(Room.HEIGHT / 2) + 1;
        transform.position = new Vector3(playerData.x - .5f, playerData.y - .5f, transform.position.z);
        UpdateHealth();
    }

    void Update()
    {
        var roomPosition = playerData.GetCurrentRoomPosition();
        var room = generator.GetRoom(roomPosition[0], roomPosition[1]);
        var playerPosition = playerData.GetPlayerPositionInRoom();
        if (room.gameObjects.ContainsKey($"{playerPosition.x}_{playerPosition.y}")) {
            var foodObject = room.gameObjects[$"{playerPosition.x}_{playerPosition.y}"];
            if (foodObject.tag == "food")
            {
                room.gameObjects.Remove($"{playerPosition.x}_{playerPosition.y}");
                Destroy(foodObject);
                AddFood(config.foodRestore);
                UpdateHealth();
            }
        }
        if (playerData.foodLevel <= 0)
        {
            if (actions.Count == 1)
            {
                uiAnimator.SetBool("gameOver", true);
                return;
            }
            ToggleMutation(true);
            return;
        }
        foreach (Action action in actions)
        {
            if (action.OnUpdate(this, generator, playerData))
            {
                playerData.foodLevel -= config.actionCost;
                UpdateHealth();
                break;
            }
        }

        UpdatePosition();
    }

    public void ToggleMutation(bool mutate)
    {
        uiAnimator.SetBool("mutate", mutate);
        mutating = true;
    }

    public void Mutate(Action removeAction)
    {
        if (!mutating || !actions.Contains(removeAction))
        {
            return;
        }
        actionButtons[removeAction.id].interactable = false;
        actions.Remove(removeAction);
        uiAnimator.SetBool("mutate", false);
        playerData.foodLevel = config.startingFood;
        mutating = false;
    }

    void UpdatePosition()
    {
        transform.position = new Vector3(playerData.x - .5f, playerData.y - .5f, transform.position.z);
    }

    void UpdateHealth()
    {
        lifeBar.SetLife(playerData.foodLevel);
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

    public void AddFood(int food)
    {
        playerData.foodLevel += food;
        PlayAudio(eatSound);
    }

    public Vector2Int getPlayerPositionInRoom()
    {
        return playerData.GetPlayerPositionInRoom();
    }
}
