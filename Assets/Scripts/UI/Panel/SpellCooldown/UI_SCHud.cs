using UnityEngine;
using UnityEngine.UI;

public class UI_SCHud : UI_WorldSpellCooldown
{
    public override void SetCooldown(float value)
    {
        ui_cooldownImage.fillAmount = 1f - value;
    }
}