﻿using UnityEngine;

namespace Constellation.Math {
    public class FloorToInt : INode, IReceiver {
        private ISender sender;
        public const string NAME = "FloorToInt";
        
        public void Setup (INodeParameters _node, ILogger _logger) {
            _node.AddInput (this, true, "A");
            sender = _node.GetSender();
            _node.AddOutput (false, "largest integer smaller to or equal to A");
        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void Receive (Variable _value, Input _input) {

            if (_input.isWarm)
                sender.Send (new Variable ().Set (Mathf.FloorToInt (_value.GetFloat ())), 0);
        }
    }
}