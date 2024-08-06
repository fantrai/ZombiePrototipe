using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour, IEntity
{ 
    //----------------Static--------------------
    protected const int TIME_LIVE_CORPSES = 10;

    //----------------Realizations---------------
    public int HP => hp;

    public virtual void TakeDamage(int damage)
    {
        if (IsLive) 
        {
            hp -= damage;
            Instantiate(takeDamageParticle, transform.position, transform.rotation);
            if (hp < 0)
                Dead();
        }
    }

    //---------------Abstraction-----------------
    abstract protected void Move();

    //---------------Class-----------------------
    [SerializeField, Min(0)] protected int hp;
    [SerializeField, Min(0)] protected float speed;
    [SerializeField] ParticleSystem takeDamageParticle;

    bool IsLive = true;

    protected virtual void FixedUpdate()
    {
        if (IsLive) Move();
    }

    protected virtual void Dead()
    {
        IsLive = false;
        gameObject.layer = 9;
        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(TIME_LIVE_CORPSES);
        do
        {
            transform.Translate(Vector3.down * speed * 0.5f);
            yield return new WaitForFixedUpdate();
        } while (transform.position.y > -2);
        Destroy(gameObject);
    }
}
