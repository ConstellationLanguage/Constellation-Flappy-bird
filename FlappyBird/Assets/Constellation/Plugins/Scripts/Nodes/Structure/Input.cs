﻿

namespace Constellation
{
	[System.Serializable]
	public class Input: IReceiver
	{
		protected IReceiver receiver;
		public string Guid;
		public bool isWarm;
		public int InputId;
		public string Type;
		public bool isConnected;
		public string Description;

		public Input (string _guid, int _inputId, bool _isWarm, string _type, string _description)
		{
			Guid = _guid;
			isWarm = _isWarm;
			InputId = _inputId;
			Type = _type;
			Description = _description;
		}

		public void Register(IReceiver _receiver)
		{
			receiver = _receiver;
		}

		public void Unregister()
		{
			receiver = null;
		}

		public void Receive(Variable value, Input input)
		{	
			receiver.Receive (value, this);
		}
	}
}
