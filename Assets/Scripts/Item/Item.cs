using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField] protected string _name;
    public string Name => _name;

    [SerializeField] protected Sprite _icon;
    public Sprite Icon => _icon;

    public abstract void Apply(ItemContext context);
    public virtual void Remove(ItemContext context) { }
}