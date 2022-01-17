using UnityEngine;
using Leopotam.Ecs;

struct UnitStack
{
    public UnitData unitData;
    public Transform transform;
    public Vector3Int tilePos;
    public int defaultInitiative;
    public int currentInitiative;
    public int moveRange;
    public int unitsCount;
    public int unitHealth;
    public int stackHealth;
    public bool leftTeam;
}




struct MeleeAttacker
{
    public int minDamageDefault;
    public int minDamage;
    public int maxDamageDefault;
    public int maxDamage;
}
struct RangeAttacker
{
    public int minDamageDefault;
    public int minDamage;
    public int maxDamageDefault;
    public int maxDamage;
    public int minRange;
    public int maxRange;
}
#region States

struct MoveState { }
struct AttackState { }
struct AbilityState { }

#endregion

struct Turn { }

struct Knight
{
    public int abilityStacks;

    public int GetReducedDamage(int normalDmg)
    {
        if (abilityStacks == 0) return normalDmg;

        float reductionPerStack = 0.33f;

        int reducedDamage = normalDmg - (int)(normalDmg * reductionPerStack * abilityStacks);
        abilityStacks--;
        return reducedDamage;
    }
}

struct Archer { }

struct Skeleton { }

struct Zombie { }

struct DamageRequest
{
    public EcsEntity dealer;
    public int value;
}

struct DeathRequest { }

