using Leopotam.Ecs;


sealed class UpdateUISystem : IEcsInitSystem, IEcsDestroySystem
{
    EcsFilter<UnitStack> allUnits;

    EcsFilter<UnitStack, Turn> currentUnit;
    public void Init()
    {
        GlobalEvents.updateUI += UpdateUI;
        UpdateUI();
    }
    public void Destroy()
    {
        GlobalEvents.updateUI -= UpdateUI;
    }

    void UpdateUI()
    {
        foreach (var unitIndex in allUnits)
        {
            ref var unit = ref allUnits.Get1(unitIndex);
            unit.unitData.health.text = unit.unitsCount.ToString();
        }


        SceneData.singleton.abilityBtn.SetActive(currentUnit.GetEntity(0).Has<Archer>());
        SceneData.singleton.turnIndicator.position = currentUnit.Get1(0).transform.position;
    }


}
