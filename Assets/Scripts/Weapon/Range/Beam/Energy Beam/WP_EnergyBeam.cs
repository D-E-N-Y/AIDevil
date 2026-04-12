using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WP_EnergyBeam : Beam
{
    [Header("Beam")]
    [SerializeField, Range(0.1f, 5f)] private float beamRadius = 3f;
    [SerializeField, Range(0.1f, 15f)] private float beamLenght = 10f;
    private float _currentLenght;

    [Header("Physics")]
    [SerializeField, Range(0.01f, 0.1f)] private float intervalRaycast = 0.05f;
    [SerializeField, Range(0.01f, 1.0f)] private float intervalDamage = 0.25f;
    private int countRaycast;

    private Dictionary<Collider, float> _hits;
    private List<IDamagable> _damagables;

    private Coroutine _attacking;

    public override void Initialize(UnitFaction unitFaction)
    {
        base.Initialize(unitFaction);

        countRaycast = Mathf.CeilToInt(beamRadius / intervalRaycast);
        _hits = new Dictionary<Collider, float>();
        _damagables = new List<IDamagable>();
    }

    public override void StartAttack()
    {
        base.StartAttack();
        
        if (_attacking == null)
        {
            _attacking = StartCoroutine(Attacking());
        }
    }

    public override void FinishAttack()
    {
        if (_attacking != null)
        {
            StopCoroutine(_attacking);
            _attacking = null;
        }

        base.FinishAttack();
    }

    protected override void Raycasting()
    {
        if (!isAlive) return;
        
        Raycast();
        SortingHits();
        SetVisualMesh();
    }

    private void Raycast()
    {
        _hits.Clear();
        _currentPenetrationCount = 0;
        
        Vector3 step = transform.right * intervalRaycast;
        Vector3 startPos = _fireTransform.position - transform.right * (beamRadius / 2);
        
        for (int i = 0; i < countRaycast; i++)
        {
            RaycastHit[] hits = Physics.RaycastAll(startPos, transform.forward, beamLenght, _interactLayers);
        
            foreach (var hit in hits)
            {
                if (_ignoreTargets.Contains(hit.collider)) continue;
                
                if (!_hits.ContainsKey(hit.collider))                
                    _hits.Add(hit.collider, hit.distance);
            }

            startPos += step;
        }
    }

    private void SortingHits()
    {
        _damagables.Clear();

        if (_hits.Count > 0)
        {
            var ordered = _hits
                .OrderBy(x => x.Value)
                .ToList();

            foreach (var pair in ordered)
            {
                if (pair.Key.TryGetComponent<IDamagable>(out IDamagable damagable))
                {
                    _damagables.Add(damagable);
                    _currentLenght = pair.Value;

                    if (_currentPenetrationCount >= maxPenetrationCount)
                        break;

                    Penetration();
                }
            }
        }

        if (_damagables.Count <= maxPenetrationCount)
        {
            _currentLenght = beamLenght;
        }
    }

    private void SetVisualMesh()
    {
        Vector3 newMeshScale = new Vector3(beamRadius/2, _currentLenght / 2, beamRadius/2);
        
        Vector3 startPos = _fireTransform.position;
        Vector3 endPos = _fireTransform.position + (transform.forward * _currentLenght);

        Vector3 newMeshPosition = new Vector3(
            (startPos.x + endPos.x) / 2,
            startPos.y,
            (startPos.z + endPos.z) / 2
        );

        mesh.transform.position = newMeshPosition;
        mesh.transform.localScale = newMeshScale;
    }

    private IEnumerator Attacking()
    {
        while (isAlive)
        {
            if (_damagables.Count <= 0)
            {
                yield return null;
                continue;
            }
            
            for (int i = _damagables.Count - 1; i >= 0; i--)
            {
                ApplyDamage(_damagables[i].GetHealth());
            }

            yield return new WaitForSeconds(intervalDamage);
        }
    }
}