namespace Constellation.Tags {
    public class Tutorial : INode, IReceiver{
        public const string NAME = "Tutorial";

        public void Setup (INodeParameters _node, ILogger _logger) {

        }

        //return the node name (used in the factory).
        public string NodeName () {
            return NAME;
        }

        //return the node namespace (used for the factory)
        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        //Receive from inputs.
        public void Receive (Variable _value, Input _input) {
        }
    }
}