using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor
{
    public override void OnInspectorGUI()
    {
        this.DrawDefaultInspector();

        NPC myTarget = target as NPC;
        if (myTarget.gameObject.activeInHierarchy)
            myTarget.GetComponentInChildren<TextMesh>().text = myTarget.Mission.ToString();
    }
}
