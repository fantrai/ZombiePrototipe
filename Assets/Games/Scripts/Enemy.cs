using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : AbstractEntity
{
    //-----------------Realizations--------------
    protected override void Move()
    {
        Vector3 moveVector = (player.Position - transform.position).normalized;
        transform.Translate(speed * moveVector, Space.World);
        transform.LookAt(player.Position);
    }

    //-----------------Class---------------------
    [Inject] IPlayer player;
}
