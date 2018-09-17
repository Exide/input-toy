using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private const string KEY_LIST = "abcdefghijklmnopqrstuvwxyz";
    public GameObject symbolPrefab;
    private List<char> knownKeys;

    private void Start() {
        knownKeys = new List<char>(KEY_LIST);
        Debug.Log(knownKeys);
    }

    private void Update() {
        foreach (var key in knownKeys) {
            var pressed = Input.GetKey(key.ToString());
            if (!pressed) continue;

            instantiateSymbol(key);
        }
    }

    private void instantiateSymbol(char symbol) {
        var position = new Vector3(0, 0, 0);
        var rotation = Quaternion.identity;
        var obj = Instantiate(symbolPrefab, position, rotation);
        var mesh = (TextMesh) obj.GetComponent(typeof(TextMesh));
        mesh.text = symbol.ToString();
    }
}