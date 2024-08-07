using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class AbstractWeapon : MonoBehaviour, IWeapon
{
    //-----------------Realizations--------------
    public Transform Transform => transform;

    public void Fire()
    {
        if (isReady)
        {
            Ray ray = new Ray(transform.position, transform.forward);

            var hits = Physics.RaycastAll(ray, distance);

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length && i <= piercing; i++)
                {
                    if (hits[i].transform.gameObject.TryGetComponent(out Enemy enemy))
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }
            audioManager.PlayOneShot(shoot);
            
            StartCoroutine(Cooldown(cooldownAttack));
        }
    }

    //-----------------Class---------------------
    [Inject] AudioManager audioManager;
    [SerializeField, Min(0)] int damage;
    [SerializeField, Min(0)] float distance;
    [SerializeField, Min(0)] int piercing;
    [SerializeField, Min(0)] float cooldownAttack;
    [SerializeField] AudioClip shoot;

    bool isReady = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * distance);
    }

    IEnumerator Cooldown(float time)
    {
        isReady = false;
        yield return new WaitForSeconds(time);
        isReady = true;
    }
}
