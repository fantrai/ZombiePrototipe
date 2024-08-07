using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class Player : AbstractEntity, IPlayer
{
    //-----------------Realizations--------------
    public Vector3 Position => transform.position;

    protected override void Move()
    {
        if (MoveVector != Vector3.zero)
        {
            transform.Translate(MoveVector * speed, Space.World);
            IsRunAnim = true;
        }
        else
        {
            IsRunAnim = false;
        }
    }

    public void AddWeapon(IWeapon weaponPrefab)
    {
        var weapon = container.InstantiatePrefab(weaponPrefab.Transform, transform.position + Vector3.up, transform.rotation, transform).GetComponent<IWeapon>();
        weapons.Push(weapon);
    }

    //-----------------Class---------------------
    [Inject] DiContainer container;

    protected Animator animator; 
    protected Stack<IWeapon> weapons = new Stack<IWeapon>();

    protected Vector3 MoveVector { get { return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); } }
    protected bool IsRunAnim { set { animator.SetBool("IsRun", value); } }
    protected bool IsAttack { set { animator.SetBool("IsAttack", value); } }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        LookOnTarget();
    }

    protected void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
        else
        {
            IsAttack = false;
        }
    }

    void LookOnTarget()
    {
        var mousePos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mousePos = hit.point;
        }

        Vector3 deltaPos = mousePos - transform.position;
        float angle = Mathf.Atan2(deltaPos.x, deltaPos.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    protected void Attack()
    {
        IsAttack = true;
        if (weapons.Count > 0) weapons.Peek().Fire();
    }
}
