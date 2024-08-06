using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : IEntity
{
    public Vector3 Position { get; }
}
