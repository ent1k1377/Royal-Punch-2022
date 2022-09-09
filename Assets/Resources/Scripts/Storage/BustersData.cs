using System;

namespace Resources.Scripts.Storage
{
    [Serializable]
    public class BustersData : Data
    {
        public int LevelHealth;
        public int CostHealth;
        public float CostIncreaseMultiplierHealth;
        
        public int LevelStrength;
        public int CostStrength;
        public float CostIncreaseMultiplierStrength;

        public BustersData() : base()
        {
            LevelHealth = 1;
            CostHealth = 50;
            CostIncreaseMultiplierHealth = 1.2f;
            LevelStrength = 1;
            CostStrength = 50;
            CostIncreaseMultiplierStrength = 1.25f;
        }
    }
}