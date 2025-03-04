using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
    public abstract class UnitStateMachine : MonoBehaviour
    {
        protected State _currentState;

        protected BaseMovement baseMovement;
        protected Unit unit;
        
        
        
        public Animator animator;
        
        public BaseMovement BaseMovement => baseMovement;
        public Unit Unit => unit;

        protected void Awake()
        {
            baseMovement = GetComponent<BaseMovement>();
            unit = GetComponent<Unit>();
            animator = GetComponent<Animator>();
        }

        protected void FixedUpdate()
        {
            StateReaction();
        }

        protected void StateReaction()
        {
            _currentState?.DoState();
            ChangeState();
        }
        
        protected abstract void ChangeState();

        public void OnDeath()
        {
            animator.SetTrigger("Death");
            unit.ResourceDropSo.GetResource(transform.position);
        }

    }