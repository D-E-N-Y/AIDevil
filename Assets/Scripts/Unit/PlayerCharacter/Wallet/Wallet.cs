using System;
using UnityEngine;

public class Wallet
{
    public event Action OnMoneyAmountChanged;

    private int _allCollectedMoney;
    public int AllCollectedMoney => _allCollectedMoney;

    private int _money;
    public int Money => _money;

    public Wallet()
    {
        _money = 0;
        _allCollectedMoney = 0;
    }

    public void AddMoney(int amount)
    {
        amount = Mathf.Max(0, amount);
        
        _money += amount;
        _allCollectedMoney += amount;

        OnMoneyAmountChanged?.Invoke();
    }

    public void RemoveMoney(int amount)
    {
        amount = Mathf.Max(0, amount);
        _money -= amount;

        OnMoneyAmountChanged?.Invoke();
    }

    public bool HasEnoughMoney(int amount)
    {
        amount = Mathf.Max(0, amount);
        return _money >= amount;
    }
}