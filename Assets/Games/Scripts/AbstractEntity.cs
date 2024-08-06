using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour, IEntity
{ 
    //----------------Static--------------------
    protected const int TIME_LIVE_CORPSES = 10;

    //----------------Realizations---------------
    public int HP => hp;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
            Dead();
    }

    //---------------Abstraction-----------------
    abstract protected void Move();

    //---------------Class-----------------------
    [SerializeField, Min(0)] protected int hp;
    [SerializeField, Min(0)] protected float speed;

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Dead()
    {
        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(TIME_LIVE_CORPSES);
        Destroy(gameObject);
    }
}
