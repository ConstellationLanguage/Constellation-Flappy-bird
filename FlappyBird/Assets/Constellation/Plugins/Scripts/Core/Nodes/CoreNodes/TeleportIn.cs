﻿namespace Constellation.CoreNodes {
    public class TeleportIn : INode, IReceiver, ITeleportIn {
        public const string NAME = "TeleportIn";
        private Attribute eventName;
        private ISender sender;

        public void Setup (INodeParameters _node, ILogger _logger) {
            _node.AddOutput (false, "Value received in the teleport");
            sender = _node.GetSender ();
            eventName = _node.AddAttribute (new Variable ("event name"), Attribute.AttributeType.Word, "The event name");
        }

        public void OnTeleport (Variable variable, string id) {
            if (id == eventName.Value.GetString () || eventName.Value.GetString () == "")
                sender.Send (variable, 0);
        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void Receive (Variable value, Input _input) {

        }
    }
}