using UnityEngine;

public abstract class Beam : RangeWeapon
{
    protected Transform _fireTransform;
    
    public bool isRaycasting { get; protected set; }

    void FixedUpdate()
    {
        if (isRaycasting)
        {
            Raycasting();
        }

        Living();
    }

    protected abstract void Raycasting();

    public override void StartAttack()
    {
        base.StartAttack();
        
        isAvaliable = false;
        
        mesh.gameObject.SetActive(true);
        gameObject.SetActive(true);

        impactEffect.Stop();

        isRaycasting = true;
    }

    public override void PrepareAttack(Transform fireTransfrom, Vector3 target)
    {
        isCanAttack = true;
        
        _fireTransform = fireTransfrom;

        transform.position = _fireTransform.position;
        transform.rotation = Quaternion.identity;
        
        _currentPenetrationCount = 0;
        _timeAlive = 0f;

        RotateToTarget(target);
    }

    public override void FinishAttack()
    {
        mesh.gameObject.SetActive(false);
        gameObject.SetActive(false);

        isAvaliable = true;
    }
}