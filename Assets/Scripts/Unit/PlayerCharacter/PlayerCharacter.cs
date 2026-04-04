using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IUnit
{
    [SerializeField] private string _id;
    public string ID => _id;

    [SerializeField] private List<Cost> _cost;
    public IReadOnlyList<Cost> Cost => _cost;

    [SerializeField] private string _name;
    [SerializeField] private PlayerCharacterStats _stats;
    [SerializeField] private PlayerCharacterMovement _movement;
    [SerializeField] private PickupSensor _pickupSensor;
    [SerializeField] private SpellContainer _spellContainer;
    [SerializeField] private StartItems _startItems;

    [SerializeField, Range(1, 10)] private int _maxSpellsCount = 4;

    private UpgradesManager _upgradesManager;
    private UnitHealth _health;
    private Inventory _inventory;
    private Wallet _wallet;
    private SpellController _spellController;
    private UnitContext _unitContext;

    public event Action<IUnit> OnDead;

    protected UnitFaction _unitFaction;
    public UnitFaction UnitFaction => _unitFaction;

    private SpellContext _spellContext;

    public virtual void Initialize(FixedJoystick joystick) 
    {
        _unitFaction = UnitFaction.Player;
        gameObject.layer = LayerMask.NameToLayer(_unitFaction.ToString());

        _stats = Instantiate(_stats);
        _stats.Initialize();
        
        _upgradesManager = new UpgradesManager(_stats);

        _health = new UnitHealth(_stats);
        
        _movement.Initialize(_stats);
        _movement.SetControlers(joystick);

        _health.OnDead += Death;

        _inventory = new Inventory(_maxSpellsCount);
        _wallet = new Wallet();

        _spellContext = new SpellContext(_unitFaction, _stats, _health, _movement);
        _spellController = new SpellController(_spellContext, _spellContainer);

        _unitContext = new UnitContext(_stats, _inventory, _spellController, _health, _wallet, _movement);

        _pickupSensor.Initialize(_unitContext);

        _inventory.SetContext(_unitContext);
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
    public UnitContext GetUnitContext() => _unitContext;

    public SpellController GetSpellController() => _spellController;
    public UpgradesManager GetUpgradesManager() => _upgradesManager;
    public PlayerCharacterMovement GetMovement() => _movement;
}
