/*

Copyright (c) 2018 Lachlan Sleight

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using UnityEditor;

/// <summary>
/// Utility class for quickly adding SerializedProperty fields to custom Inspectors
/// </summary>
public class InspectorProperty {

	enum PropertyType {
		Default,
		FloatSlider,
		IntSlider
	}

	/// <summary>
	/// The internal serialized property (useful for things like .floatValue)
	/// </summary>
	public SerializedProperty Property;

	string DisplayName;
	float MinFloat;
	float MaxFloat;
	int MinInt;
	int MaxInt;
	PropertyType Type;

	/// <summary>
	/// Initializes an InspectorProperty in the default way (i.e. not a slider)
	/// </summary>
	/// <param name="editor">The editor containing the serializedObject reference</param>
	/// <param name="Name">The name of the field on the serializedObject</param>
	public InspectorProperty(Editor editor, string Name)
	{
		Property = editor.serializedObject.FindProperty(Name);
		DisplayName = MakeReadableName(Name);
	}

	/// <summary>
	/// Initializes an InspectorProperty to be edited as a float slider
	/// </summary>
	/// <param name="editor">The editor containing the serializedObject reference</param>
	/// <param name="Name">The name of the field on the serializedObject</param>
	/// <param name="Min">The minimum value for the slider</param>
	/// <param name="Max">The maximum value for the slider</param>
	public InspectorProperty(Editor editor, string Name, float Min, float Max)
	{
		Property = editor.serializedObject.FindProperty(Name);
		DisplayName = MakeReadableName(Name);
		Type = PropertyType.FloatSlider;
		MinFloat = Min;
		MaxFloat = Max;
	}

	/// <summary>
	/// Initializes an InspectorProperty to be edited as an int slider
	/// </summary>
	/// <param name="editor">The editor containing the serializedObject reference</param>
	/// <param name="Name">The name of the field on the serializedObject</param>
	/// <param name="Min">The minimum value for the slider</param>
	/// <param name="Max">The maximum value for the slider</param>
	public InspectorProperty(Editor editor, string Name, int Min, int Max)
	{
		Property = editor.serializedObject.FindProperty(Name);
		DisplayName = MakeReadableName(Name);
		Type = PropertyType.IntSlider;
		MinInt = Min;
		MaxInt = Max;
	}

	/// <summary>
	/// Actually shows the SerializedProperty Field
	/// </summary>
	public void ShowField()
	{
		ShowField(new GUIContent(DisplayName));
	}

	/// <summary>
	/// Actually shows the SerializedProperty Field
	/// </summary>
	/// <param name="content">Custom GUIContent object to use rather than the default field name</param>
	public void ShowField(GUIContent content)
	{
		switch(Type) {
		case PropertyType.Default:
			EditorGUILayout.PropertyField(Property, content);
			break;
		case PropertyType.FloatSlider:
			EditorGUILayout.Slider(Property, MinFloat, MaxFloat, content);
			break;
		case PropertyType.IntSlider:
			EditorGUILayout.IntSlider(Property, MinInt, MaxInt, content);
			break;
		}
	}

	//just turns things like "displayName" into "Display Name" and "customHtml" to "Custom Html"
	//note - will turn "customHTMLField" into "Custom HTMLField"
	string MakeReadableName(string Source)
	{
		string output = "";
		for(int i = 0; i < Source.Length; i++) {
			//first character should always be upper case
			if(i == 0) {
				output += Source[0].ToString().ToUpper();
				continue;
			} else {
				if(char.IsUpper(Source[i])) {
					//if it's not the first capital of an acronym, no need for a space (e.g. H*TML*)
					if(char.IsUpper(Source[i-1])) {
						output += Source[i].ToString();
					//if it's the first capital after some lowercase, put a space
					} else {
						output += " ";
						output += Source[i].ToString();
					}
				} else {
					output += Source[i].ToString();
				}
			}
		}
		return output;
	}

}
