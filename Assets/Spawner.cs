using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private const string KEY_LIST = "abcdefghijklmnopqrstuvwxyz";
    private List<char> knownKeys;

    private void Start() {
        knownKeys = new List<char>(KEY_LIST);
        Debug.Log(knownKeys);
    }

    private void Update() {
        foreach (var key in knownKeys) {
            var pressed = Input.GetKey(key.ToString());
            if (!pressed) continue;

            var symbol = buildSymbol(key);
            Instantiate(symbol);
        }
    }

    private static GameObject buildSymbol(char symbol) {
        var gameObject = new GameObject();
        gameObject.AddComponent<TextMesh>().text = symbol.ToString();
        gameObject.AddComponent<MeshRenderer>();
        return gameObject;
    }
}