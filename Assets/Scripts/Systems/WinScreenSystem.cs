using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class WinScreenSystem : ReactiveSystem<GameEntity>
{
    private GameObject winScreen;

    public WinScreenSystem(Contexts contexts) : base(contexts.game)
    {
        winScreen = GameObject.Find("WinScreen");
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.WinScreen);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasWinScreen;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.winScreen.isVisible && winScreen != null)
            {
                winScreen.SetActive(true);
            }
        }
    }
}