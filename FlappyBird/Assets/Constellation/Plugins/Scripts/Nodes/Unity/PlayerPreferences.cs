﻿using UnityEngine;

namespace Constellation.Unity {
	public class PlayerPreferences : INode, IReceiver {
		public const string NAME = "PlayerPreferences";
		public Variable keyName;
		public Variable savedData;
		public Variable keyValue;
		private ISender sender;
		public void Setup (INodeParameters _node) {
			keyName = new Variable ("");
			keyValue = new Variable ("");
			savedData = new Variable ("");
			_node.AddInput (this, false, "key Name");
			_node.AddInput (this, false, "key Data");
			_node.AddInput (this, false, "Save data");
			_node.AddInput (this, false, "Delete key");
			_node.AddInput (this, true, "Get data");

			sender = _node.GetSender();
			_node.AddOutput (false, "Key name");
			_node.AddOutput (false, "Key data");
		}
		public string NodeName () {
			return NAME;
		}
		public string NodeNamespace () {
			return NameSpace.NAME;
		}

		public void Receive (Variable value, Input _input) {
			if (_input.InputId == 0) {
				keyName.Set (value.GetString ());
			}

			if (_input.InputId == 1) {
				keyValue.Set (value.GetString ());
			}

			if (_input.InputId == 2) {
				PlayerPrefs.SetString (keyName.GetString (), keyValue.GetString ());
			}

			if (_input.InputId == 3) {
				PlayerPrefs.DeleteKey (value.GetString ());
			}

			if (_input.InputId == 4) {
				savedData.Set (PlayerPrefs.GetString (value.GetString ()));
			}

			if (_input.isWarm) {
				sender.Send (value, 0);
				sender.Send (savedData, 1);
			}
		}
	}
}