using UnityEngine;

public class UI_SCLine : UI_SpellCooldown
{
    public override void Initialize(Spell spell)
    {
        base.Initialize(spell);

        _rectTransform.sizeDelta = new Vector2(
            1f,
            spell.RangeAttack()
        );
    }

    public override void SetCooldown(float value)
    {
        ui_cooldownImage.fillAmount = value;
    }

    // public void SetRotation(Quaternion rotation)
    // {
    //     float yAngle = rotation.eulerAngles.y;
    //     transform.rotation = Quaternion.Euler(0f, 0f, yAngle);
    // }
}