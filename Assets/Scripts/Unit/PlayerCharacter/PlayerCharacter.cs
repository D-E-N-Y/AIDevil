using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IUnit, IDamagable
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
    private ISpellController _spellController;
    private UnitContext _unitContext;

    protected UnitFaction _unitFaction;
    public UnitFaction UnitFaction => _unitFaction;

    private SpellContext _spellContext;

    public event Action<IDamagable> OnDead;

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
        _spellController = new PlayerCharacterSpellController(_spellContext, _spellContainer);

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

    public string Name => _name;
    public UnitStats Stats => _stats;
    public UnitHealth Health => _health;
    public IHealth IHealth => _health;
    public Inventory Inventory => _inventory;
    public Wallet Wallet => _wallet;
    public StartItems StartItems => _startItems;
    public UnitContext UnitContext => _unitContext;
    public ISpellController SpellController => _spellController;
    public UpgradesManager UpgradesManager => _upgradesManager;
    public PlayerCharacterMovement Movement => _movement;
}
