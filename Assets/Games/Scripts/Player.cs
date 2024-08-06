using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractEntity, IPlayer
{
    //-----------------Realizations--------------
    public Vector3 Position => transform.position;

    protected override void Move()
    {
        if (MoveVector != Vector3.zero)
        {
            float modSpeed = 1f;
            if (Mathf.Abs(MoveVector.x) > 0.5 && Mathf.Abs(MoveVector.z) > 0.5) modSpeed = 0.6f;
            transform.Translate(MoveVector * speed * modSpeed, Space.World);
            IsRunAnim = true;
        }
        else
        {
            IsRunAnim = false;
        }
    }

    //-----------------Class---------------------
    Animator animator; 
    protected Vector3 MoveVector { get { return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); } }
    protected bool IsRunAnim { set { animator.SetBool("IsRun", value); } }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        LookOnTarget();
    }

    void LookOnTarget()
    {
        var mousePos = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mousePos = hit.point;
        }

        Vector3 deltaPos = mousePos - transform.position;
        float angle = Mathf.Atan2(deltaPos.x, deltaPos.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
