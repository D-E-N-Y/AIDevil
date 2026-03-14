using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Hint Icons", menuName = "UI/HintIcons")]
public class HintIcons : ScriptableObject 
{
    [SerializeField] private List<HintIcon> hintIcons;

    public Sprite GetHintSpriteByType(HintType type)
    {
        foreach (HintIcon hintIcon in hintIcons)
        {
            if (hintIcon.type == type)
            {
                return hintIcon.sprite;
            }
        }

        return hintIcons.First().sprite;
    }
}