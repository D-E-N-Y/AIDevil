using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(EnemySpellController))]
public class Enemy : MonoBehaviour, IUnit, IDamagable
{
    [Header("Enemy Info")]
    [SerializeField] protected EnemyType _type;
    [SerializeField] protected string _id;
    [SerializeField] protected string _name;
    [SerializeField] protected EnemyStats _stats;

    protected UnitHealth _health;
    protected bool _isDead;
    public event Action<IDamagable> OnDead;

    protected EnemySpellController _spellController;
    protected EnemyMovement _movement;

    [Header("Visual")]
    [SerializeField] UI_HPIndicator ui_hpIndicator;

    [Header("Target")]
    [SerializeField] protected Transform _target;

    protected UnitFaction _unitFaction;
    protected SpellContext _spellContext;

    private EnemyState _state;

    public virtual void Initialize()
    {
        _unitFaction = UnitFaction.Enemy;
        gameObject.layer = LayerMask.NameToLayer(_unitFaction.ToString());

        _stats.Initialize();

        _health = new UnitHealth(_stats);
        _health.OnDead += Die; 
        _isDead = false;

        ui_hpIndicator.Initialize(_health);
        ui_hpIndicator.gameObject.SetActive(true);

        _movement = GetComponent<EnemyMovement>();
        _movement.Initialize(_stats);

        _spellContext = new SpellContext(_unitFaction, _stats, _health, _movement);
        _spellController = GetComponent<EnemySpellController>();
        _spellController.Initialize(_spellContext);

        _movement.SetStopDistance(_spellController.OptimalAttackRange);
        _repathTimer = 0f;

        _state = EnemyState.Idle;
        gameObject.SetActive(true);
    }

    public virtual void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Moving:
                Move();
                break;

            case EnemyState.Attacking:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        if (!_target) return;
        
        _state = EnemyState.Moving;
    }

    private void Attack()
    {
        if (!_target) return;

        if (IsInRange())
        {
            _spellController.CastRandomSpell();
        }
        else if(!_spellController.IsAnySpellAttacking())
        {
            _state = EnemyState.Moving;
        }
    }

    private float _repathTimer;
    private void Move()
    {
        if (!_target) return;

        _repathTimer += Time.deltaTime;

        if (_repathTimer >= 0.25f)
        {
            _movement.MoveTo(_target.position);
            _repathTimer = 0f;
        }

        if (IsInRange())
        {
            _movement.Stop();
            _state = EnemyState.Attacking;
        }
    }

    private bool IsInRange()
    {
        float sqrDist = (transform.position - _target.position).sqrMagnitude;
        return sqrDist <= _spellController.OptimalAttackRange * _spellController.OptimalAttackRange;
    }

    private void Die()
    {
        _isDead = true;
        OnDead?.Invoke(this);
        gameObject.SetActive(false);
    }

    public string Name => _name;
    public string ID => _id;
    public EnemyType Type => _type;
    public UnitStats Stats => _stats;
    public UnitHealth Health => _health;
    public IHealth IHealth => _health;
    public bool IsDead => _isDead;
    public EnemyState State => _state;

    public EnemySpellController SpellController => _spellController;

    public int DropMoney => _stats.DropMoney;
}