using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Unit unit;

    private bool canAttack = true;

    private void Start()
    {
        unit = GetComponent<Unit>();
    }
    private void Update()
    {
        if (unit.GetState() == UnitState.MoveToEnemy)
        {
            animator.SetBool("Move", true);
            Debug.Log("Move");
        }
        else
        {
            animator.SetBool("Move", false);
        }

        if (unit.GetState() == UnitState.WaitForAttack || unit.GetState() == UnitState.Attack)
        {
            canAttack = true;
            Debug.Log("Battle");
            animator.SetBool("Battle", true);
        }
        else
        {
            animator.SetBool("Battle", false);
        }

        if (unit.GetState() == UnitState.Attack && canAttack)
        {
            canAttack = false;
            int rand = Random.Range(1, 3);
            animator.SetTrigger("Attack" + rand);
        }
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }
}
