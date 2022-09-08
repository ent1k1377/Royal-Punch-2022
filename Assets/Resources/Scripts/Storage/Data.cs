using System;

namespace Resources.Scripts.Storage
{
    [Serializable]
    public abstract class Data
    {
        protected string NameFile;
        public virtual string GetNameFile() => NameFile;
    }
}