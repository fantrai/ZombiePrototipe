using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveCamera : MonoBehaviour
{
    [Inject] IPlayer player;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.Position + offset;
    }
}
