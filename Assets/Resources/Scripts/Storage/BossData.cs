using System;

namespace Resources.Scripts.Storage
{
    [Serializable]
    public class BossData : Data
    {
        public int Health;
        public int Strength;

        public BossData()
        {
            Health = 380;
            Strength = 35;
        }
    }
}