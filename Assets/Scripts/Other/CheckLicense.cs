using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckLicense : MonoBehaviour
{
    public int LicenseYear;
    public int LicenseMonth;
    public int LicenseDay;

    // Use this for initialization
    void Start()
    {
        int[] lastOpenDay;
        if (PlayerPrefs.HasKey("OpenGameDay"))
        {
            lastOpenDay = PlayerPrefsX.GetIntArray("OpenGameDay");
            System.DateTime lastOpenDate = new System.DateTime(lastOpenDay[0], lastOpenDay[1], lastOpenDay[2]);
            if (lastOpenDate.CompareTo(System.DateTime.Now) >= 0)
            {
                print("開啟時間錯誤");
                return;
            }
        }

        System.DateTime LicenseTime = new System.DateTime(this.LicenseYear, this.LicenseMonth, this.LicenseDay);
        if (LicenseTime.CompareTo(System.DateTime.Now) >= 1)
            this.EnterGame();
        else
        {
            PlayerPrefsX.SetIntArray("OpenGameDay", new int[] { System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day });
            print("期限已過");
        }
    }

    void EnterGame()
    {
        PlayerPrefsX.SetIntArray("OpenGameDay", new int[] { System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day });

        EventCollection.script.NextEvent();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
