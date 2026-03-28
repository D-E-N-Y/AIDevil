using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Icons", menuName = "UI/Stat/StatIcons", order = 0)]
public class StatIcons : ScriptableObject 
{
    [System.Serializable]
    public struct StatIcon
    {
        public StatType stat;
        public Sprite icon;
        public Color color;
    }

    [SerializeField] private List<StatIcon> statIcons;

    public StatIcon GetStatIcon(StatType stat)
    {
        foreach (var statIcon in statIcons)
        {
            if (statIcon.stat == stat)
            {
                return statIcon;
            }
        }
        return default;
    }
}