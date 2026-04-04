using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField] protected string _name;
    public string Name => _name;

    [SerializeField] protected Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField, Range(0, 9999)] protected int _price;
    public int Price => _price;

    [SerializeField] protected ItemRarity _rarity;
    public ItemRarity Rarity => _rarity;

    public abstract ItemType Type { get; }

    public abstract void Apply(UnitContext context);
    public virtual void Remove(UnitContext context) { }
}