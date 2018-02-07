using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace Foliar.UU {
	/// <summary>
	/// Allows direct manipulation of a ScriptableObject field of type text
	/// </summary>
	[RequireComponent(typeof(InputField))]
	public class SOUI_InputField : SOUI {
		InputField _MyInputField;
		/// <summary>
		/// Unity InputField component
		/// </summary>
		private InputField MyInputField {
			get {
				if(_MyInputField == null) _MyInputField = GetComponent<InputField>();

				return _MyInputField;
			} set {
				_MyInputField = value;
			}
		}

		private void Awake() {
			MyInputField = GetComponent<InputField>();

			//Add listener
			MyInputField.onValueChanged.AddListener(delegate { InputFieldValueChanged(MyInputField.text); });
		}

		private void Update() {
			SetUnityInputField();
		}

		/// <summary>
		/// Sets the target field to the supplied value (This method is supplied to the Unity InputField component's OnValueChanged event)
		/// </summary>
		/// <param name="value"></param>
		void InputFieldValueChanged(string value) {
			if(TargetField != null) TargetField.SetValue(TargetObject, value);
		}

		/// <summary>
		/// Sets the text of the Unity InputField to the text held in the source ScriptableObject field
		/// </summary>
		public void SetUnityInputField() {
			if(TargetField != null) MyInputField.text = (string)TargetField.GetValue(TargetObject);
		}
	
	}


}