using System;

namespace Resources.Scripts.Storage
{
    [Serializable]
    public abstract class Data
    {
        public string NameFile => GetType().Name + ".save";
    }
}