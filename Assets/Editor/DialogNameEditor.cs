using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogName))]
public class DialogNameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        this.DrawDefaultInspector();

        DialogName myTarget = target as DialogName;
        myTarget.GetComponent<TextMesh>().text = myTarget.dialogName.ToString();
    }
}
