using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PadSequenceComponent : IComponent
{
    public int currentSequenceIndex;
    public int[] correctSequence;
}