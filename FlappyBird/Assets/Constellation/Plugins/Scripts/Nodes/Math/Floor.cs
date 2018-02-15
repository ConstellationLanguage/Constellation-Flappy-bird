﻿using UnityEngine;

namespace Constellation.Math {
public class Floor: INode, IReceiver
    {
		private ISender sender;
        public const string NAME = "Floor";
        public void Setup(INodeParameters _node, ILogger _logger)
        {
			_node.AddInput(this, true, "A");
            sender = _node.GetSender();
            _node.AddOutput(false, "Largest integer smaller to or equal to a.");
        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void Receive(Variable _value, Input _input)
        {

            if (_input.isWarm)
                sender.Send(new Variable().Set(Mathf.Floor(_value.GetFloat())), 0);
        }
    }
}
