using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour, IWeapon
{
    //-----------------Realizations--------------
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

            StartCoroutine(Cooldown(cooldownAttack));
        }
    }

    public IWeapon Create(Transform parent)
    {
        var weapon = Instantiate(gameObject, parent.position + Vector3.up, parent.rotation);
        weapon.transform.SetParent(parent);

        return weapon.GetComponent<IWeapon>();
    }

    //-----------------Class---------------------
    [SerializeField, Min(0)] int damage;
    [SerializeField, Min(0)] float distance;
    [SerializeField, Min(0)] int piercing;
    [SerializeField, Min(0)] float cooldownAttack;

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
