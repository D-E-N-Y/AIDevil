using UnityEngine;

public class UI_SCCircle : UI_SpellCooldown
{
    protected RectTransform rectTransformCooldownImage;

    public override void Initialize(Spell spell)
    {
        base.Initialize(spell);

        rectTransformCooldownImage = ui_cooldownImage.GetComponent<RectTransform>();

        _rectTransform.sizeDelta = new Vector2(
            spell.RangeAttack() * 2,
            spell.RangeAttack() * 2
        );
    }

    public override void SetCooldown(float value)
    {
        rectTransformCooldownImage.localScale = new Vector3(
            value,
            value,
            value
        );
    }

    public void SetPosition(Vector3 position)
    {
        // Vector3 _localPosition = rectTransformCooldownImage.InverseTransformPoint(position);

        rectTransformCooldownImage.position = position;
    }
}