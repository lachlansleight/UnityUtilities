using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace Foliar.UU {
	/// <summary>
	/// Allows direct manipulation of a ScriptableObject field of bool (or of type that inherits from ScriptableObject and has one of two values)
	/// </summary>
	[RequireComponent(typeof(Toggle))]
	public class SOUI_Toggle : SOUI {
		Toggle _MyToggle;
		/// <summary>
		/// Unity Toggle component
		/// </summary>
		private Toggle MyToggle {
			get {
				if(_MyToggle == null) _MyToggle = GetComponent<Toggle>();

				return _MyToggle;
			} set {
				_MyToggle = value;
			}
		}

		/// <summary>
		/// The valuew to set the field when toggle is on
		/// </summary>
		public ScriptableObject OnValue;
		/// <summary>
		/// The value to set the field when toggle is off
		/// </summary>
		public ScriptableObject OffValue;

		/// <summary>
		/// Whether the target type is of type bool
		/// </summary>
		public bool isBoolType;


		private void Awake() {
			MyToggle = GetComponent<Toggle>();

			//Add listener
			MyToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(MyToggle.isOn); });
		}

		private void Update() {
			SetUnityToggle();
		}

		/// <summary>
		/// Sets the target field to the supplied value (This method is supplied to the Unity Toggle component's OnValueChanged event)
		/// </summary>
		/// <param name="value"></param>
		void ToggleValueChanged(bool value) {
			//Ensure value isn't null
			if(value && OnValue == null) {
				Debug.LogWarning("Warning: OnValue is null for SO_Toggle " + gameObject.name);
				return;
			}
			if(!value && OffValue == null) {
				Debug.LogWarning("Warning: OnValue is null for SO_Toggle " + gameObject.name);
				return;
			}

			if(isBoolType) {
				TargetField.SetValue(TargetObject, value);
			} else {
				TargetField.SetValue(TargetObject, value ? OnValue : OffValue);
			}
			
		}

		/// <summary>
		/// Sets the value of the Unity Toggle to the value held in the source ScriptableObject field
		/// </summary>
		public void SetUnityToggle() {
			Object FieldValue = (Object)TargetField.GetValue(TargetObject);
			if(isBoolType) {
				MyToggle.isOn = (bool)FieldValue;
			} else {
				MyToggle.isOn = (FieldValue == OnValue);
			}
		}
	
	}
}