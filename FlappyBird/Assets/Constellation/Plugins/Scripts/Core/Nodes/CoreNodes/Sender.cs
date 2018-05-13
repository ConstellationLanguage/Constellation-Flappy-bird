namespace Constellation.CoreNodes {
    public class Sender : INode, IReceiver {
        public const string NAME = "Sender";
        private Attribute eventName;

        public void Setup (INodeParameters _node) {
            _node.AddInput (this, true, "value to send");
            eventName = _node.AddAttribute (new Variable ("event name"), Attribute.AttributeType.Word, "The event name");

        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void Receive (Variable value, Input _input) {
            if (_input.isWarm)
                ConstellationComponent.eventSystem.SendEvent (eventName.Value.GetString(), value);
        }
    }
}