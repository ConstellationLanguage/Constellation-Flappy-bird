using System.Collections.Generic;
using Constellation;
using UnityEngine;

namespace ConstellationEditor {
    public class ConstellationCompiler {
        private NodesFactory NodesFactory;

        public void UpdateScriptsNodes(ConstellationScript [] scripts) {
            foreach(var script in scripts) {
                UpdateScriptNodes(script.script);
            }
        } 

        public void UpdateScriptNodes (ConstellationScriptData script) {
            List<NodeData> nodesToRemove = new List<NodeData> ();
            NodesFactory = new NodesFactory ();
            try {
            foreach (var node in script.Nodes) {
                if (NodesFactory.GetNodeSafeMode (node) == null) {
                    nodesToRemove.Add (node);
                } else if (node.Inputs.Count != NodesFactory.GetNode (node).Inputs.Count || node.Outputs.Count != NodesFactory.GetNode (node).Outputs.Count) {
                    nodesToRemove.Add (node);
                }
            }

            foreach (var node in nodesToRemove) {
                script.RemoveNode (node.Guid);
                var replacementNode = NodesFactory.GetNode (node.Name, node.Namespace);
                if(replacementNode == null)
                    return;

                replacementNode.XPosition = node.XPosition;
                replacementNode.YPosition = node.YPosition;

                if (node.Inputs.Count >= replacementNode.Inputs.Count) {
                    for (var i = 0; i < replacementNode.Inputs.Count; i++) {
                        replacementNode.Inputs[i].Guid = node.Inputs[i].Guid;
                    }
                } else {
                    for (var i = 0; i < node.Inputs.Count; i++) { 
                        replacementNode.Inputs[i].Guid = node.Inputs[i].Guid;
                    }
                }

                
                if (node.Outputs.Count >= replacementNode.Outputs.Count) {
                    for (var i = 0; i < replacementNode.Outputs.Count; i++) {
                        replacementNode.Outputs[i].Guid = node.Outputs[i].Guid;
                    }
                } else {
                    for (var i = 0; i < node.Outputs.Count; i++) { 
                        replacementNode.Outputs[i].Guid = node.Outputs[i].Guid;
                    }
                }
                script.AddNode (new NodeData (replacementNode));
            }
            } catch {
                Debug.LogError(script);
            }
        }
    }
}