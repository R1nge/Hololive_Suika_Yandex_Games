using System;
using UnityEngine;
using YG;

namespace _Assets.Scripts.Services.Wallets
{
    public class Wallet
    {
        private int _coins;
        public int Coins
        {
            get { return _coins; }
            private set
            {
                _coins = value;
                OnCoinsChanged?.Invoke(_coins);
                Save();
            } 
        }

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

            if (Coins < coins)
            {
                Debug.LogWarning($"Wallet: SpendCoins: not enough coins. coins: {Coins}, spend: {coins}");
                return false;
            }

            Coins -= coins;
            OnCoinsChanged?.Invoke(Coins);
            return true;
        }

        private void Save()
        {
            YandexGame.savesData.coins = Coins;
            YandexGame.SaveProgress();
        }

        public void Load() => Coins = YandexGame.savesData.coins;
    }
}