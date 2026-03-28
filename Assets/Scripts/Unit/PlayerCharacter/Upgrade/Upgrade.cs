using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Upgrade")]
public class Upgrade : ScriptableObject 
{   
    [SerializeField] private string _id;
    public string ID => _id;
    
    [SerializeField] private string _name;
    public string Name => _name;
    
    [SerializeField] private List<StatModifier> _modifiers;
    public IReadOnlyList<StatModifier> Modifiers => _modifiers;

    [SerializeField] private List<Cost> _cost;
    public IReadOnlyList<Cost> Cost => _cost;

    [SerializeField] private List<Upgrade> _requiredUpgrades;
    public IReadOnlyList<Upgrade> RequiredUpgrades => _requiredUpgrades;

    private void OnValidate()
    {
        foreach (var mod in _modifiers)
        {
            mod.Validate();
        }
    }
}