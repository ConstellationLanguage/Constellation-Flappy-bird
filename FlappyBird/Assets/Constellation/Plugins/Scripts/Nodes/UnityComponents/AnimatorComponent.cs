﻿using UnityEngine;

namespace Constellation.Components {
    public class AnimatorComponent : INode, IReceiver, IGameObject {
        UnityEngine.Animator animator;
        private Variable varName;
        private Variable varValue;

        public const string NAME = "AnimatorComponent";

        public void Setup (INodeParameters _nodeParameters, ILogger _logger) {
            _nodeParameters.AddInput (this, false, "Object", "Animator object");
            _nodeParameters.AddInput (this, false, "Var name");
            _nodeParameters.AddInput (this, false, "Var value");
            varName = new Variable ().Set ("");
            varValue = new Variable ().Set (0);
        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void Set (GameObject _gameObject) {
            var anim = _gameObject.GetComponent<UnityEngine.Animator> ();
            if (anim != null)
                animator = anim;
        }

        public void Receive (Variable value, Input _input) {
            if (_input.InputId == 0)
                Set (UnityObjectsConvertions.ConvertToGameObject (value.GetObject ()));

            if (_input.InputId == 1) {
                varName.Set (value.GetString ());
            }

            if (_input.InputId == 2) {
                if (value.IsFloat ()) {
                    varValue.Set (value.GetFloat ());
                    animator.SetFloat (varName.GetString (), varValue.GetFloat ());
                } else {
                    Debug.LogWarning("Animator node only supports numbers");
                }
            }
        }
    }
}