using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace Foliar.UU {
	/// <summary>
	/// Allows direct manipulation of a ScriptableObject field of type that inherits from ScriptableObject
	/// </summary>
	[RequireComponent(typeof(Dropdown))]
	public class SOUI_Dropdown : SOUI {

		Dropdown _MyDropdown;
		/// <summary>
		/// Unity Dropdown component
		/// </summary>
		private Dropdown MyDropdown {
			get {
				if(_MyDropdown == null) _MyDropdown = GetComponent<Dropdown>();

				return _MyDropdown;
			} set {
				_MyDropdown = value;
			}
		}
		
		/// <summary>
		/// List of available options
		/// </summary>
		public ScriptableObject[] Options;

		private void Awake() {
			MyDropdown = GetComponent<Dropdown>();

			SetUnityDropdown();

			//add listener
			MyDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(MyDropdown.value); });
		}

		private void Update() {
			SetUnityDropdown();
		}

		/// <summary>
		/// Sets the target field to the supplied value (This method is supplied to the Unity Dropdown component's OnValueChanged event)
		/// </summary>
		/// <param name="value"></param>
		void DropdownValueChanged(int value) {
			//ensure value is in range
			if(value >= 0 && value < Options.Length) {
				//ensure value isn't null
				if(Options[value] == null) {
					Debug.LogWarning("Warning: Options " + value + " is null on SO_Dropdown " + gameObject.name);
					return;
				}
				
				//actually set the field value
				TargetField.SetValue(TargetObject, Options[value]);

			} else {
				Debug.LogWarning("Warning: value supplied to DropdownValueChanged on object " + gameObject.name + " is out of bounds (supplied " + value + " where options list has length " + Options.Length + ")");
			}
		}

		/// <summary>
		/// Makes the Unity Dropdown component's options reflect this component's options
		/// </summary>
		public void UpdateUnityOptions() {
			MyDropdown.ClearOptions();
			List<Dropdown.OptionData> NewOptions = new List<Dropdown.OptionData>();
			for(int i = 0; i < Options.Length; i++) {
				if(Options[i] == null) NewOptions.Add(new Dropdown.OptionData("Empty Option"));
				else NewOptions.Add(new Dropdown.OptionData(Options[i].name));
			}
			MyDropdown.AddOptions(NewOptions);
			SetUnityDropdown();
		}

		/// <summary>
		/// Sets the value of the Unity dropdown to the value held in the source ScriptableObject field
		/// </summary>
		public void SetUnityDropdown() {
			Object FieldValue = (Object)TargetField.GetValue(TargetObject);
			for(int i = 0; i < Options.Length; i++) {
				if(Options[i] == FieldValue) {
					MyDropdown.value = i;
				}
			}
		}

		/// <summary>
		/// Adds an option to the Option List
		/// </summary>
		public void AddOption() {
			ScriptableObject[] NewOptions = new ScriptableObject[Options.Length + 1];
			for(int i = 0; i < Options.Length; i++) {
				NewOptions[i] = Options[i];
			}
			Options = NewOptions;
		}

		/// <summary>
		/// Removes the specified Option from the Options list
		/// </summary>
		/// <param name="index">The index of the option to remove</param>
		public void RemoveOption(int index) {
			if(index < 0 || index >= Options.Length) return;

			ScriptableObject[] NewOptions = new ScriptableObject[Options.Length - 1];
			int offset = 0;
			for(int i = 0; i < NewOptions.Length; i++) {
				if(i == index) {
					offset++;
				}
				NewOptions[i] = Options[i + offset];
			}
			Options = NewOptions;
		}

		/// <summary>
		/// Moves the specified Option up one place in the options list
		/// </summary>
		/// <param name="index"></param>
		public void MoveUp(int index) {
			if(index == 0 || Options.Length == 1) return;

			ScriptableObject TempOptionA = Options[index-1];
			ScriptableObject TempOptionB = Options[index];
			Options[index] = TempOptionA;
			Options[index-1] = TempOptionB;
		}

		/// <summary>
		/// Moves the specified Option down one place in the options list
		/// </summary>
		/// <param name="index"></param>
		public void MoveDown(int index) {
			if(index == Options.Length - 1 || Options.Length == 1) return;

			ScriptableObject TempOptionA = Options[index+1];
			ScriptableObject TempOptionB = Options[index];
			Options[index] = TempOptionA;
			Options[index+1] = TempOptionB;
		}
	
	}
}