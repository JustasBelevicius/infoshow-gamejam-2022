using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;

    // Update is called once per frame
    void Update()
    {
        var room = playerManager.GetCurrentRoomPosition();
        float x = (room[0] + 1) * Room.WIDTH - Room.WIDTH / 2 - .5f;
        float y = (room[1] + 1) * Room.HEIGHT - Room.HEIGHT / 2 - .5f;
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), Time.deltaTime * 2f);
    }
}
