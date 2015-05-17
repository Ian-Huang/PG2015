using UnityEngine;
using System.Collections;

//[ExecuteInEditMode] 
public class DialogName : MonoBehaviour
{
    public GameDefinition.DialogName dialogName;

    public float DelayTime;

    void OnEnable()
    {
        //StartCoroutine(NameShow());
        this.NameShow();
    }

    // Use this for initialization
    void NameShow()
    {
        //this.GetComponent<TextMesh>().text = "";
        //yield return new WaitForSeconds(this.DelayTime);

        switch (this.dialogName)
        {
            case GameDefinition.DialogName.None:
                this.GetComponent<TextMesh>().text = "";
                break;
            case GameDefinition.DialogName.被選角色名:
                this.GetComponent<TextMesh>().text = GameDefinition.PlayerNameData[GameDefinition.CurrentChoosePlayerName];
                break;
            case GameDefinition.DialogName.翠絲:
                this.GetComponent<TextMesh>().text = GameDefinition.PlayerNameData[GameDefinition.SystemPlayerName.翠絲];
                break;
            case GameDefinition.DialogName.巴洛:
                this.GetComponent<TextMesh>().text = GameDefinition.PlayerNameData[GameDefinition.SystemPlayerName.巴洛];
                break;
            case GameDefinition.DialogName.卡勒b:
                this.GetComponent<TextMesh>().text = GameDefinition.PlayerNameData[GameDefinition.SystemPlayerName.卡勒b];
                break;
            case GameDefinition.DialogName.里昂:
                this.GetComponent<TextMesh>().text = GameDefinition.PlayerNameData[GameDefinition.SystemPlayerName.里昂];
                break;
            case GameDefinition.DialogName.莉莉卡:
                this.GetComponent<TextMesh>().text = GameDefinition.PlayerNameData[GameDefinition.SystemPlayerName.莉莉卡];
                break;
            case GameDefinition.DialogName.葛蘭蒂:
                this.GetComponent<TextMesh>().text = GameDefinition.PlayerNameData[GameDefinition.SystemPlayerName.葛蘭蒂];
                break;
            default:
                this.GetComponent<TextMesh>().text = this.dialogName.ToString();
                break;
        }

    }
}