using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    Stand,
    MoveToEnemy,
    WaitForAttack,
    Attack,
    Dead
}

[RequireComponent(typeof(UnitAlignment))]
[RequireComponent(typeof(UnitAnimator))]
public class Unit : MonoBehaviour
{
    public UnitAlignment alignment;
    private UnitState state;

    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private ParticleSystem blood;

    private float prevAttackTime;

    [SerializeField]
    private float health;

    [SerializeField]
    private float damage;

    private Unit target;

    private void Start()
    {
        state = UnitState.Stand;
        alignment = GetComponent<UnitAlignment>();
    }

    private void Update()
    {
        Debug.Log(gameObject.name + " state is " + state);
        if (health <= 0) Die();
        else if (state == UnitState.Stand)
        {
            CheckEnemyTargets();
        }
        else if (state == UnitState.MoveToEnemy)
        {
            AIPath setter = GetComponent<AIPath>();
            if (setter.reachedEndOfPath)
                state = UnitState.WaitForAttack;
        }
        else if (state == UnitState.WaitForAttack)
        {
            if (target != null && target.GetState() != UnitState.Dead)
            {
                if (CanAttack()) state = UnitState.Attack;
            }
            else if (target == null || target.GetState() == UnitState.Dead)
                state = UnitState.Stand;
        }
        
    }

    public void Die()
    {
        state = UnitState.Dead;
        GetComponent<UnitAnimator>().DisableAnimator();
        Destroy(gameObject, 2);
    }

    public UnitState GetState()
    {
        return state;
    }

    private void CheckEnemyTargets()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5, LayerMask.GetMask("Unit"));
         
        for (int i = 0; i < colliders.Length; i++)
        {
            Unit unit = colliders[i].GetComponentInParent<Unit>();
            if (unit.GetState() == UnitState.Dead) continue;

            if (alignment.IsUnitEnemy(unit))
            {
                SetEnemy(unit);
                return;
            }
        }
    }

    private void SetEnemy(Unit enemy)
    {
        transform.LookAt(enemy.transform);
        this.target = enemy;
        //AIPath setter = GetComponent<AIPath>();
        AIDestinationSetter dest = GetComponent<AIDestinationSetter>();
        dest.target = enemy.transform;
        //setter.target = enemy.transform; 
        state = UnitState.MoveToEnemy;
    }

    private bool CanAttack()
    {
        if (Time.time - prevAttackTime < attackCooldown)
        {
            return false;
        }
        else if (Time.time - prevAttackTime > attackCooldown)
        {   
            return true;
        }
        return false;
    }

    public void DoMeleeDamage()
    {
        target.ReceiveDamage(damage);
    }

    public void EndMeleeAttack()
    {
        prevAttackTime = Time.time;
        state = UnitState.WaitForAttack;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        Bleed();
    }

    private void Bleed()
    {
        blood.Stop();
        blood.Play();
    }


}
