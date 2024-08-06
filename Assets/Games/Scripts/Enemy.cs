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
    [SerializeField] ParticleSystem onDeadParticle;

    protected Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Dead()
    {
        animator.SetTrigger("Dead");
        Instantiate(onDeadParticle, transform.position, onDeadParticle.transform.rotation);
        base.Dead();
    }
}
