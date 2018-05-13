using System.Collections.Generic;
using UnityEngine;


namespace Constellation {
    [CreateAssetMenuAttribute(fileName = "NewConstellation", menuName = "Constellation", order = 3)]
    public class ConstellationScript : ScriptableObject {
        public ConstellationScriptData script;
        public bool IsInstance = false;
        public bool IsDifferentThanSource = false;

        public void InitializeData()
        {
            script = new ConstellationScriptData();
            Set(script);
        }

        public ConstellationScript Set (ConstellationScriptData _script) {
            script.Nodes = new List<NodeData>();
            script.Links = new List<LinkData>();

            if(_script.Nodes != null)
                foreach (var node in _script.Nodes) 
                    AddNode (node);

            if(_script.Links != null)
                foreach(var link in _script.Links) {
                    AddLink(link);
            }
            return this;
        }

        public NodeData AddNode (NodeData _node) {
            if (script.Nodes == null)
                script.Nodes = new List<NodeData> ();
            var newNode = new NodeData (_node);
            newNode.XPosition = _node.XPosition;
            newNode.YPosition = _node.YPosition;
            script.Nodes.Add (newNode);
            return newNode;
        }

        public NodeData AddNode (Node<INode> _node) {
            if (script.Nodes == null)
                script.Nodes = new List<NodeData> ();
            var newNode = new NodeData (_node);
            script.Nodes.Add (newNode);
            return newNode;
        }

        public void RemoveNode (NodeData _node) {
            script.Nodes.Remove (_node);
        }

        public void RemoveNode (Node<INode> _node) {
            foreach (NodeData node in script.Nodes) {
                if (_node.GetGuid () == node.Guid) {
                    script.Nodes.Remove (node);
                    return;
                }
            }
        }

        public LinkData[] GetLinks () {
            if (script.Links == null)
                script.Links = new List<LinkData> ();

            return script.Links.ToArray ();
        }

        public NodeData[] GetNodes () {
            if (script.Nodes == null)
                script.Nodes = new List<NodeData> ();

            return script.Nodes.ToArray ();
        }

        public bool IsLinkValid (LinkData _link) {
            foreach (LinkData link in script.Links) {
                if (_link.Input.Guid == link.Input.Guid && _link.Output.Guid == link.Output.Guid) {
                    Debug.LogError ("Cannot add a link that already exist");
                    return false;
                }
            }
            return true;
        }

        public void AddLink (LinkData _link) {
            if (script.Links == null)
                script.Links = new List<LinkData> ();
            script.Links.Add (_link);
        }

        public LinkData AddLink (Link _link) {
            if (script.Links == null)
                script.Links = new List<LinkData> ();
            var newLink = new LinkData (_link);
            script.Links.Add (newLink);
            return newLink;
        }

        public void RemoveLink (LinkData _link) {
            script.Links.Remove (_link);
        }
    }
}