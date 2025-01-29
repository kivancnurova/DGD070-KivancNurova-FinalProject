using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour
{
    private Systems _systems;

    private void Start()
    {
        var contexts = Contexts.sharedInstance;

        var sequenceEntity = contexts.game.CreateEntity();
        sequenceEntity.AddPadSequence(0, new int[] { 0, 1, 2, 3 });
        
        EntityFactory.CreatePlayer(contexts, Vector2.zero);
        EntityFactory.CreatePad(contexts, new Vector2(-5, -3), 0);
        EntityFactory.CreatePad(contexts, new Vector2(5, -3), 1);
        EntityFactory.CreatePad(contexts, new Vector2(-5, 3), 2);
        EntityFactory.CreatePad(contexts, new Vector2(5, 3), 3);
        EntityFactory.CreateBoundary(contexts);

        _systems = CreateSystems(contexts);
        _systems.Initialize();
    }

    private void Update()
    {
        _systems.Execute();
    }

    private Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Game")
            .Add(new PlayerMovementSystem(contexts))
            .Add(new PadTriggerSystem(contexts))
            .Add(new RenderPositionSystem(contexts))
            .Add(new PadColorChangeSystem(contexts));
    }
}