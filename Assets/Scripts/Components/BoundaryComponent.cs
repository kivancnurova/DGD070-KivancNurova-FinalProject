using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class BoundaryComponent : IComponent
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
}