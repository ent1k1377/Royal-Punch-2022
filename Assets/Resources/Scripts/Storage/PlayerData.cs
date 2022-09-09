using System;

namespace Resources.Scripts.Storage
{
    [Serializable]
    public class PlayerData : Data
    {
        public int NumberCoins;
        public int Level;
        public int Health;
        public int Strength;
        
        public PlayerData()
        {
            NumberCoins = 200;
            Level = 1;
            Health = 150;
            Strength = 25;
        }
    }
}