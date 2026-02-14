using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IUnit
{
    [SerializeField] private string _name;
    [SerializeField] private PlayerCharacterStats _stats;
    [SerializeField] private PlayerCharacterMovement _movement;
    [SerializeField] private UI_UnitHPIndicator _ui_hpIndicator;
    [SerializeField] private PickupSensor _pickupSensor;
    [SerializeField] private SpellContainer _spellContainer;
    [SerializeField] private StartItems _startItems;

    private UnitHealth _health;
    private Inventory _inventory;
    private Wallet _wallet;
    private SpellController _spellController;
    private ItemContext _itemContext;

    public event Action<IUnit> OnDead;

    protected UnitFaction _unitFaction;
    public UnitFaction UnitFaction => _unitFaction;

    public virtual void Initialize(FixedJoystick joystick) 
    {
        _unitFaction = UnitFaction.Player;
        gameObject.layer = LayerMask.NameToLayer(_unitFaction.ToString());

        _stats.Initialize();

        _health = new UnitHealth(_stats);
        _spellController = new SpellController(_unitFaction, _stats, _spellContainer);
        
        _movement.Initialize(_stats);
        _movement.SetControlers(joystick);

        _ui_hpIndicator.Initialize(_health);
        _health.OnDead += Death;

        _inventory = new Inventory();
        _wallet = new Wallet();
        _wallet.AddMoney(9999);

        _itemContext = new ItemContext(_stats, _inventory, _spellController, _health, _wallet);

        _pickupSensor.Initialize(_itemContext);

        _inventory.SetContext(_itemContext);
        _inventory.AddItems(_startItems.GetStartItems());

        gameObject.SetActive(true);
    }

    private void Death()
    {
        OnDead?.Invoke(this);
        gameObject.SetActive(false);
    }

    public string GetName() => _name;
    public UnitStats GetStats() => _stats;
    public UnitHealth GetHealth() => _health;
    public Inventory GetInventory() => _inventory;
    public Wallet GetWallet() => _wallet;
    public StartItems GetStartItems() => _startItems;
    public ItemContext GetItemContext() => _itemContext;

    public SpellController GetSpellController() => _spellController;
    public PlayerCharacterMovement GetMovement() => _movement;
}
