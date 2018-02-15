﻿using UnityEngine;

namespace Constellation.Math {
    public class ClosestPowerOf2 : INode, IReceiver {
        private ISender sender;
        public const string NAME = "ClosestPowerOf2";
        public void Setup (INodeParameters _node, ILogger _logger) {
            _node.AddInput (this, true, "a");
            sender = _node.GetSender();
            _node.AddOutput (false, "Closest power of two");
        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void Receive (Variable _value, Input _input) {
            if (_input.isWarm)
                sender.Send (new Variable ().Set (Mathf.ClosestPowerOfTwo (Mathf.RoundToInt (_value.GetFloat ()))), 0);
        }
    }
}