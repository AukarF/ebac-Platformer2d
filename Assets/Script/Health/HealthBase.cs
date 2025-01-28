using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;
    
    public float startLife = 10f;

    public bool destroyOnKill = false;
    public float delayToKill = 0f;
    
    private float _currentLife;
    private bool _isDead = false;

    public FlashColor _flashColor;

    private void Awake()
    {
        init();
        if(_flashColor==null)
        {
            _flashColor.GetComponentInChildren<FlashColor>();
        }
    }

    private void init()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }

        if(_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;
        if(destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }

        OnKill?.Invoke();
    }


}
