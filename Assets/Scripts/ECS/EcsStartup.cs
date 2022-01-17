using Leopotam.Ecs;
using UnityEngine;


sealed class EcsStartup : MonoBehaviour
{
    EcsWorld world;
    EcsSystems systems;

    void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
#endif
        systems
.Add(new TileManager())
.Add(new UnitInitSystem())
.Add(new MoveUnitsSystem())
.Add(new MoveDisplaySystem())
.Add(new AbilityDisplaySystem())
.Add(new AttackDisplaySystem())
.Add(new TurnManager())
.Add(new StateManager())
.Add(new UpdateUISystem())
.Add(new AttackSystem())
.Add(new DamageSystem())
.Add(new DeathSystem())

.Init();

    }

    void Update()
    {
        systems?.Run();
    }

    void OnDestroy()
    {
        if (systems != null)
        {
            systems.Destroy();
            systems = null;
            world.Destroy();
            world = null;
        }
    }
}
