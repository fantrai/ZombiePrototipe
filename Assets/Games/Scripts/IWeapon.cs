using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Fire();

    public IWeapon Create(Transform transform);
}
