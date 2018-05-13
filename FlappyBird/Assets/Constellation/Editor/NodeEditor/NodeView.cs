using Constellation;
using UnityEditor;
using UnityEngine;

namespace ConstellationEditor {
    public class NodeView {
        private const int ButtonSize = 14;
        private Rect Rect;
        public NodeData node;
        private NodeConfig nodeConfig;
        private ConstellationScript constellationScript;
        private bool isDestroyed = false;
        private bool selected = false;
        private bool nodeMoved = false;
        private Vector2 nodeMovement = Vector2.zero;
        private bool DrawDescription = false;
        private string Description = "";
        private bool CloseOnNextFrame = false;
        private bool isAttributeValueChanged = false;
        public delegate void HelpClicked (string _nodeName);
        private ILinkEditor linkEditor;
        private IVisibleObject visibleObject;
        
        public NodeView (NodeData _node, IVisibleObject _visibleObject, NodeConfig _nodeConfig, ConstellationScript _constellation, ILinkEditor _linkEditor) {
            nodeConfig = _nodeConfig;
            var nodeWidth = nodeConfig.NodeWidth;
            if (_node.GetAttributes().Length > 0) {
                nodeWidth = nodeConfig.NodeWidthAsAttributes;
            }
            Rect = new Rect(_node.XPosition, _node.YPosition, nodeWidth, (Mathf.Max(Mathf.Max(_node.Inputs.Count, _node.Outputs.Count), _node.AttributesData.Count) * nodeConfig.InputSize) + nodeConfig.TopMargin);
            node = _node;
            visibleObject = _visibleObject;
            constellationScript = _constellation;
            linkEditor = _linkEditor;

            
            foreach (var attribute in node.AttributesData) {
                attribute.Value = AttributeStyleFactory.Reset(attribute.Type, attribute.Value);
            }
        }

        public void DrawWindow (int id, GUI.WindowFunction DrawNodeWindow, bool isNote) {
            //Only draw visible nodes
            if (!visibleObject.InView(Rect))
                return;

            if (DrawDescription)
                DrawHelp(Description);
            
            if (node.Name != "Note") {
                var nodeStyle = selected ? nodeConfig.NodeHoverStyle : nodeConfig.NodeStyle;
                Rect = GUI.Window(id, Rect, DrawNodeWindow, "", nodeStyle);
            } else {
                var noteStyle = selected ? nodeConfig.NoteHoverStyle : nodeConfig.NoteStyle;
                Rect = GUI.Window(id, new Rect(Rect.x, Rect.y, 120, 120), DrawNodeWindow, "", noteStyle);
            }
            
            if (node.XPosition != Rect.x || node.YPosition != Rect.y) {
                nodeMovement = new Vector2(node.XPosition - Rect.x, node.YPosition - Rect.y);
                nodeMoved = true;
            } else {
                nodeMovement = Vector2.zero;
                nodeMoved = false;
            }
            node.XPosition = Rect.x;
            node.YPosition = Rect.y;
        }

        public bool IsDragged () {
            return nodeMoved;
        }

        public Vector2 DragVector () {
            return nodeMovement;
        }

        public void DragNode (Vector2 vector) {
            Rect = new Rect(Rect.x - vector.x, Rect.y - vector.y, Rect.width, Rect.height);
            node.XPosition = Rect.x;
            node.YPosition = Rect.y;
        }

        public void ClearDrag () {
            nodeMoved = false;
            nodeMovement = Vector2.zero;
        }

        public void SelectNode () {
            selected = true;
        }

        public void DeselectNode () {
            selected = false;
        }

        public void DestroyNode () {
            constellationScript.RemoveNode(node);
            isDestroyed = true;
        }

        public bool NodeExist () {
            return !isDestroyed;
        }

        private void DrawHelp (string text) {
            Event current = Event.current;
            GUI.Label(new Rect(current.mousePosition.x + 30, current.mousePosition.y + 20, 30 + Description.Length * 4, 30), text, GUI.skin.GetStyle("AnimationEventTooltip"));
            if (CloseOnNextFrame == true) {
                DrawDescription = false;
                CloseOnNextFrame = false;
            }
            if (current.isMouse) {
                CloseOnNextFrame = true;
            }
        }

        public bool IsAttributeValueChanged () {
            var changeState = isAttributeValueChanged;
            isAttributeValueChanged = false;
            return changeState;
        }

        private void AttributeValueChanged () {
            isAttributeValueChanged = true;
        }

        public void DrawContent (HelpClicked _onHelpClicked) {
            var current = Event.current;

            //Only draw node on Repaint if it's not selected
            if (current.IsRepaint())
                Draw(_onHelpClicked);

            //Draw on multiple events for buttons to work
            if (selected && !current.IsRepaint())
                Draw(_onHelpClicked);
        }

        private void Draw (HelpClicked _onHelpClicked) {
            DrawAttributes();
            DrawInputs();
            DrawOutputs();
            DrawHeader(_onHelpClicked);

            if (DrawDescription)
                DrawHelp(Description);
        }

        private void DrawAttributes () {
            if (node.GetAttributes() != null) {
                var i = 0;
                foreach (var attribute in node.AttributesData) {
                    EditorGUIUtility.labelWidth = 25;
                    EditorGUIUtility.fieldWidth = 10;
                    var attributeRect = new Rect(nodeConfig.AtrributeSize.x, nodeConfig.AtrributeSize.y + (nodeConfig.AtrributeSize.height * i), nodeConfig.AtrributeSize.width, nodeConfig.AtrributeSize.height);
                    if (attribute.Value != null) {
                        var currentAttributeValue = attribute.Value.GetString();
                        attribute.Value = AttributeStyleFactory.Draw(attribute.Type, attributeRect, attribute.Value);
                        if (attribute.Value != null) {
                            if (currentAttributeValue != attribute.Value.GetString())
                                AttributeValueChanged();
                            i++;
                        }
                    }
                }
            }
        }

        private void DrawInputs () {
            if (node.Inputs != null) {
                var i = 0;
                foreach (var input in node.Inputs) {
                    if (GUI.Button(new Rect(0, nodeConfig.TopMargin + (nodeConfig.InputSize * i), nodeConfig.InputSize, nodeConfig.InputSize), "",
                            GetConnectionStyle(input.IsWarm, input.Type))) {
                        Event current = Event.current;
                        if (current.button == 0)
                            linkEditor.AddLinkFromInput(input);
                        else {
                            DrawDescription = true;
                            Description = input.Description;
                        }
                    }
                    i++;
                }
            }
        }

        private void DrawOutputs () {
            if (node.Outputs != null) {
                var i = 0;
                foreach (var output in node.Outputs) {
                    if (GUI.Button(new Rect(Rect.width - nodeConfig.OutputSize, nodeConfig.TopMargin + ((nodeConfig.OutputSize) * i), nodeConfig.OutputSize, nodeConfig.OutputSize), "",
                            GetConnectionStyle(output.IsWarm, output.Type))) {
                        Event current = Event.current;
                        if (current.button == 0){
                            linkEditor.AddLinkFromOutput(output);
                        } else {
                            DrawDescription = true;
                            Description = output.Description;
                        }
                    }
                    i++;
                }
            }
        }

        public void DrawHeader (HelpClicked onHelpClicked) {
            var width = Rect.width - 10;

            if (selected) {
                var color = GUI.color;
                width -= ButtonSize * 2 + 7;

                //Light gray color for close button
                GUI.color = new Color(0.8f, 0.8f, 0.8f);
                UnityEngine.GUI.Box(new Rect(Rect.width - (ButtonSize + 2), 1, ButtonSize, ButtonSize), "", UnityEngine.GUI.skin.GetStyle("sv_label_0"));
                if (GUI.Button(new Rect(Rect.width - (ButtonSize + 1), 1, ButtonSize - 2, ButtonSize), "", UnityEngine.GUI.skin.GetStyle("WinBtnClose")) && Event.current.button == 0) {
                    DestroyNode();
                }

                GUI.color = color;
                if (GUI.Button(new Rect(Rect.width - (ButtonSize * 2 + 5), 1, ButtonSize, ButtonSize), "", nodeConfig.HelpStyle) && Event.current.button == 0) {
                    onHelpClicked(node.Name);
                }
            }

            GUI.Label(new Rect(10, 0, width, 16), node.Name, UnityEngine.GUI.skin.GetStyle("MiniLabel"));
        }

        private GUIStyle GetConnectionStyle (bool _isWarm, string _type) {
            if (_isWarm) {
                if (_type == "Object")
                    return nodeConfig.WarmInputObjectStyle;
                else
                    return nodeConfig.WarmInputStyle;
            } else {
                if (_type == "Object")
                    return nodeConfig.ColdInputObjectStyle;
                else
                    return nodeConfig.ColdInputStyle;
            }
        }

        public NodeData GetData () {
            return node;
        }

        public Rect GetRect () {
            return Rect;
        }

        public bool Selected {
            get {
                return selected;
            }
        }
    }
}