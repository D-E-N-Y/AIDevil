using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Tree", menuName = "Upgrade/UpgradeTree")]
public class UpgradeTree : ScriptableObject 
{
    [SerializeField] private PlayerCharacter _playerCharacter;
    public PlayerCharacter PlayerCharacter => _playerCharacter;

    [SerializeField] private List<Upgrade> _upgrades;
    public IReadOnlyList<Upgrade> Upgrades => _upgrades;

    public Upgrade GetUpgradeByID(string id)
    {
        foreach (Upgrade upgrade in _upgrades)
        {
            if (upgrade.ID == id)
            {
                return upgrade;
            }
        }

        return null;
    }
}