using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLoader : MonoBehaviour {
    public static ImageLoader instance;

    Dictionary<string, Sprite> dic_Sprite = new Dictionary<string, Sprite>();

    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    private void Start() {
        LoadSprite();
    }

    void LoadSprite() {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Image/student");
        foreach(var s in sprites) {
            dic_Sprite.Add(s.name, s);
        }
    }

    public Sprite GetSprite(string code) {
        Sprite sprite = dic_Sprite[code];

        if(sprite == null) {
            Debug.LogWarning($"No sprite : {code}");
        }
        else {
            Debug.Log($"Get sprite : {code}");
        }

        return sprite;
    }
}
