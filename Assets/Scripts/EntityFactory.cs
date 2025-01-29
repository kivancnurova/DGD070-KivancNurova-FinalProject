using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public static class EntityFactory
{
    public static GameEntity CreatePlayer(Contexts contexts, Vector2 position)
    {
        var entity = contexts.game.CreateEntity();
        entity.AddPosition(position);
        entity.AddMovable(5f);
        entity.isPlayer = true;

        GameObject playerPrefab = Resources.Load<GameObject>("Player");
        GameObject playerObject = Object.Instantiate(playerPrefab);
        playerObject.transform.position = position;

        entity.AddView(playerObject);

        return entity;
    }

    public static GameEntity CreatePad(Contexts contexts, Vector2 position, int padId)
    {
    var entity = contexts.game.CreateEntity();
    entity.AddPosition(position);
    entity.AddPad(padId, false);

    GameObject padPrefab = Resources.Load<GameObject>("Pad");
    GameObject padObject = Object.Instantiate(padPrefab);
    padObject.transform.position = position;

    SpriteRenderer spriteRenderer = padObject.GetComponent<SpriteRenderer>();

    entity.AddView(padObject);
    entity.view.spriteRenderer = spriteRenderer;

    return entity;
    }

    public static GameEntity CreateBoundary(Contexts contexts)
    {
        var entity = contexts.game.CreateEntity();
        entity.AddBoundary(-8f, 8f, -4f, 4f);
        return entity;
    }
}