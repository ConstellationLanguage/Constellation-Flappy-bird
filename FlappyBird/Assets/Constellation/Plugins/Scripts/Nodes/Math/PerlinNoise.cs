﻿using UnityEngine;

namespace Constellation.Math {
public class PerlinNoise: INode, IReceiver
    {
		private ISender sender;
		private Variable VarX;
		private Variable VarY;
        public const string NAME = "PerlinNoise";
        public void Setup(INodeParameters _node)
        {
			_node.AddInput(this, false, "X");
			_node.AddInput(this, true, "Y");
			VarX = new Variable(0);
			VarY = new Variable(0);
            sender = _node.GetSender();
            _node.AddOutput(false, "Next power of two");
        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void Receive(Variable _value, Input _input)
        {
			if(_input.InputId == 0)
				VarX.Set(_value.GetFloat());
			
			if(_input.InputId == 1)
				VarY.Set(_value.GetFloat());

            if (_input.isWarm)
                sender.Send(new Variable().Set(Mathf.PerlinNoise(VarX.GetFloat(), VarY.GetFloat())), 0);
        }
    }
}
