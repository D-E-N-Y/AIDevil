using UnityEngine;

public class EM_Melee : E_Moving 
{
    protected override void Update()
    {
        base.Update();

        if (Vector3.Distance(playerTarget.transform.position, transform.position) <= _agent.stoppingDistance)
        {
            foreach (Spell spell in spells)
            {
                if(spell is SpellMelee) spell.Cast();
            }
        }
    }
}