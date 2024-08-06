using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity : IDamageble
{
    public int HP { get; }
}
