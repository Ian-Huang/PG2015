using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IslandBackgroundSelect : MonoBehaviour
{
    public List<IslandBackgroundData> IslandBackgroundList;

    // Use this for initialization
    void Start()
    {
        //找出目前正在進行的島嶼的背景 (Sprite)
        this.GetComponent<SpriteRenderer>().sprite = this.IslandBackgroundList.Find((IslandBackgroundData data) =>
        {
            return (data.Island == GameDefinition.CurrentIsland);
        }).Sprite;
    }

    [System.Serializable]
    public class IslandBackgroundData
    {
        public GameDefinition.Island Island;
        public Sprite Sprite;
    }
}
