using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class PadSequenceComponent : IComponent
{
    public int currentSequenceIndex;
    public int[] correctSequence;
}