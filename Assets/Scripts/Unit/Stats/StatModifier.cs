using System;
using UnityEngine;

[Serializable]
public class StatModifier
{
    public StatType stat;
    public StatValueType valueType;
    public float value;

    public void Validate()
    {
        if (valueType == StatValueType.Percent)
        {
            value = Mathf.Clamp(value, -2f, 2f);
        }
        else
        {
            // int _value = Math.Clamp((int)value, -999, 999);
            value = Math.Clamp((int)value, -999, 999);
        }
    }
}

public enum StatValueType
{
    Flat,      // +10 HP
    Percent    // +20%
}