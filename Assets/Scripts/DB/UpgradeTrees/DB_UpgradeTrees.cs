using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DB Upgrade Trees", menuName = "DataBase/UpgradeTrees")]
public class DB_UpgradeTrees : ScriptableObject 
{
    [SerializeField] private List<UpgradeTree> upgradeTrees;
    public IReadOnlyList<UpgradeTree> UpgradeTrees => upgradeTrees;

    public UpgradeTree GetUpgradeTreeByCharacter(PlayerCharacter character)
    {
        foreach (UpgradeTree tree in upgradeTrees)
        {
            if (tree.PlayerCharacter == character)
            {
                return tree;
            }
        }

        return null;
    }

    public UpgradeTree GetUpgradeTreeByCharacterID(string characterID)
    {
        foreach (UpgradeTree tree in upgradeTrees)
        {
            if (tree.PlayerCharacter.ID == characterID)
            {
                return tree;
            }
        }

        return null;
    }
}