using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PadComponent : IComponent
{
    public int padId;
    public bool isTriggered;
}