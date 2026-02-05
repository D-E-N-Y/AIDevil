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

    [SerializeField] protected ItemRare _rare;
    public ItemRare Rare => _rare;

    public abstract void Apply(ItemContext context);
    public virtual void Remove(ItemContext context) { }
}