using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TogglePseudoBody))]
public class PseudoBodyControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TogglePseudoBody controller = (TogglePseudoBody)target;

        GUILayout.Space(10);

        if (controller.IsPseudoBodyActive())
        {
            EditorGUILayout.LabelField("PseudoBody State: Enabled =)", EditorStyles.boldLabel);
        }
        else
        {
            EditorGUILayout.LabelField("PseudoBody State: Disabled or not found =(", EditorStyles.boldLabel);
        }
    }
}