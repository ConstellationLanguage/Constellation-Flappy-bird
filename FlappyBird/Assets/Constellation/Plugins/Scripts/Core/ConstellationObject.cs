﻿namespace Constellation
{
    [System.Serializable]
    public class ConstellationObject
    {
        public string Name;
        public string Namespace;
        public string Guid;

        public virtual void Initialize(string _guid, string _name)
        {
            Guid = _guid;
            Name = _name;
        }


        public string GetGuid()
        {
            return Guid;
        }

        public virtual void OnDestroy()
        {

        }
    }
}