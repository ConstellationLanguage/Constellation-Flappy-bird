﻿using UnityEngine;

namespace Constellation.Math {
public class PingPong: INode, IReceiver
    {
		private ISender sender;
		private Variable VarT;
		private Variable VarLenght;
        public const string NAME = "PingPong";
        public void Setup(INodeParameters _node)
        {
			_node.AddInput(this, false, "T");
			_node.AddInput(this, true, "Lenght");
			VarT = new Variable(0);
			VarLenght = new Variable(0);
            sender = _node.GetSender();
            _node.AddOutput(false, "Ping Pong T value");
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
				VarT.Set(_value.GetFloat());
			
			if(_input.InputId == 1)
				VarLenght.Set(_value.GetFloat());

            if (_input.isWarm)
                sender.Send(new Variable().Set(Mathf.PingPong(VarT.GetFloat(), VarLenght.GetFloat())), 0);
        }
    }
}
