using System;
using System.Reflection;
using UnityEngine;

namespace Constellation.CoreNodes {
    public class CodeVar : INode, IReceiver {
        private ISender Sender;
        private Attribute VarName;
        public const string NAME = "CodeVar";
        private Variable currentVar;
        private Variable currentInstance;
        private object currentReflectedVar;
        private object currentReflectedObject;
        private PropertyInfo property;

        public void Setup (INodeParameters _node) {
            var newValue = new Variable ("VarName");
            Sender = _node.GetSender(); 
            _node.AddOutput (false, "The value");
            _node.AddInput (this, false, "Object", "Object which contains the var");
            _node.AddInput (this, false, "Set Var");
            _node.AddInput (this, true, "Push var");
            VarName = _node.AddAttribute (newValue, Attribute.AttributeType.Word, "VarName");
            currentVar = new Variable ();
            currentInstance = new Variable ();
        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void Receive (Variable _value, Input _input) {
            try {
                if (_input.InputId == 0) {
                    currentReflectedVar = _value.GetObject ().GetType ().GetProperty (VarName.Value.GetString ()).GetValue (_value.GetObject (), null);
                    currentReflectedObject = _value.GetObject ();
                    Type myType = currentReflectedObject.GetType ();
                    property = myType.GetProperty (VarName.Value.GetString ());
                    currentInstance.Set (currentReflectedVar);
                    GetVarInCurrentObject ();
                }

                if (_input.InputId == 1) {
                    SetVarInCurrentObject (_value);
                }

                if (_input.isWarm) {
                    Sender.Send (currentVar, 0);
                }
            } catch {
                Debug.LogWarning("Something went wrong while parsing your var: \n 1) make sure an object is setted in the first input \n 2) make sure the name match the variable name \n 3) The type you are trying to set is not handled by the node");
            };
        }

        private void SetVarInCurrentObject (Variable variable) {
            property.SetValue (currentReflectedObject, variable.GetObject (), null);
        }

        private void GetVarInCurrentObject () {
            try {
                if (currentReflectedVar is float)
                    currentVar.Set ((float) currentReflectedVar);
                else if (currentReflectedVar is int)
                    currentVar.Set ((float) currentReflectedVar);
                else if (currentReflectedVar is string)
                    currentVar.Set ((string) currentReflectedVar);
                else if (currentReflectedVar is bool) {
                    var boolean = (bool) currentReflectedVar;
                    if (boolean == true)
                        currentVar.Set (1);
                    else
                        currentVar.Set (0);
                } else if (currentReflectedVar is Vector3) {
                    var vec3 = (Vector3) currentReflectedVar;
                    Variable[] newVar = new Variable[3];
                    newVar[0] = new Variable ().Set (vec3.x);
                    newVar[1] = new Variable ().Set (vec3.y);
                    newVar[2] = new Variable ().Set (vec3.z);
                    currentVar.Set (newVar);
                } else
                    currentVar.Set (currentVar);

            } catch {
                Debug.LogWarning ("Constellation node: Get var has invalid attribute");
            }
        }
    }
}