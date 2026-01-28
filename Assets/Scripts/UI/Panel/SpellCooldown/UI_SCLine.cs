using UnityEngine;

public class UI_SCLine : UI_WorldSpellCooldown
{
    public override void Initialize(Spell spell)
    {
        base.Initialize(spell);

        _rectTransform.sizeDelta = new Vector2(
            1f * spell.AreaModifier,
            spell.RangeAttack()
        );
    }

    public override void SetCooldown(float value)
    {
        ui_cooldownImage.fillAmount = value;
    }

    public void SetRotation()
    {
        Quaternion _rotation = Quaternion.Euler(90, _spell.transform.eulerAngles.y, -90);
        canvasTransform.rotation = _rotation;
    }

    protected override void AddSubscriptions()
    {
        base.AddSubscriptions();
        _spell.onStartCooldown += SetRotation;
    }

    protected override void ClearSubscriptions()
    {
        base.ClearSubscriptions();
        _spell.onStartCooldown -= SetRotation;
    }
}