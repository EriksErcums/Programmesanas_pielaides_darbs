using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPC))]
public class DialogueManagerEditor : Editor
{
    /*public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NPC npc = (NPC)target;
        string s = "z";
        GUILayout.Label("Contacts");
        if(GUILayout.Button("Add contact"))
        {
            GUILayout.TextArea(s);
        }
    }*/
}
