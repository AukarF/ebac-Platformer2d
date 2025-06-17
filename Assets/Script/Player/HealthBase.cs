using UnityEngine;
using System;
using System.Collections.Generic;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;

    public float startLife = 10f;
    public bool destroyOnKill = false;
    public float delayToKill = 0f;

    private float _currentLife;
    private bool _isDead = false;

    public FlashColor _flashColor;

    public float CurrentHealth => _currentLife; // ✅ Expor vida atual para leitura

    private void Awake()
    {
        Init();

        if (_flashColor == null)
            _flashColor = GetComponentInChildren<FlashColor>();
    }

    public void Init()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    public void ResetHealth() // ✅ Método chamado pelo Player.cs se vida estiver zerada
    {
        Init();
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }

        if (_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;

        if (destroyOnKill)
            Destroy(gameObject, delayToKill);

        OnKill?.Invoke();
    }
}
