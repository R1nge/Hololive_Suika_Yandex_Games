using System;
using UnityEngine;

namespace _Assets.Scripts.Services.Wallets
{
    public class Wallet
    {
        private int _coins;
        public int Coins => _coins;

        public event Action<int> OnCoinsChanged;

        public void EarnCoins(int coins)
        {
            if (coins <= 0)
            {
                Debug.LogWarning($"Wallet: EarnCoins: coins must be greater than 0. coins: {coins}");
                return;
            }

            _coins += coins;
            OnCoinsChanged?.Invoke(_coins);
        }

        public bool SpendCoins(int coins)
        {
            if (coins <= 0)
            {
                Debug.LogWarning($"Wallet: SpendCoins: coins must be greater than 0. coins: {coins}");
                return false;
            }
            
            if (_coins < coins)
            {
                Debug.LogWarning($"Wallet: SpendCoins: not enough coins. coins: {_coins}, spend: {coins}");
                return false;
            }
            
            _coins -= coins;
            OnCoinsChanged?.Invoke(_coins);
            return true;
        }
    }
}