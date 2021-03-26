using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animator : MonoBehaviour
{
    [SerializeField]
    private Enemy_Master _enemyMaster;
    [SerializeField]
    private Animator _animator;


    private void OnEnable()
    {
        SetInitialReferences();

        _enemyMaster.EventEnemyAttack += SetAnimationToAttack;
        _enemyMaster.EventEnemyDie += DisableAnimator;
        _enemyMaster.EventEnemyDie += SetAnimationToDeath;       
        _enemyMaster.EventEnemyWalking += SetAnimationToWalk;
        _enemyMaster.EventEnemyReachedNavTarget += SetAnimationToIdle;
    }

    private void OnDisable()
    {
        _enemyMaster.EventEnemyAttack -= SetAnimationToAttack;
        _enemyMaster.EventEnemyDie -= DisableAnimator;
        _enemyMaster.EventEnemyDie -= SetAnimationToDeath;
        _enemyMaster.EventEnemyWalking -= SetAnimationToWalk;
        _enemyMaster.EventEnemyReachedNavTarget -= SetAnimationToIdle;
    }

    private void SetInitialReferences()
    {
        _enemyMaster = GetComponent<Enemy_Master>();
        _animator = GetComponent<Animator>();
    }

    private void SetAnimationToIdle()
    {
        if (_animator.enabled)
        {
            _animator.SetBool("isPursuing", false);
        }
    }

    private void SetAnimationToWalk()
    {
        if (_animator.enabled)
        {
            _animator.SetBool("isPursuing",true);
        }
    }

    private void SetAnimationToAttack()
    {
        if (_animator.enabled)
        {
            _animator.SetTrigger("Attack");
        }
    }

    private void SetAnimationToDeath()
    {
        if (_animator.enabled)
        {
            _animator.SetTrigger("Death");
        }
    }

    private void DisableAnimator()//TODO !??!?? opoznic Ienumeratorem albo wywalic w calosci?
    {
        if (_animator != null) 
        {
            //_animator.enabled = false;
        }
    }
}
