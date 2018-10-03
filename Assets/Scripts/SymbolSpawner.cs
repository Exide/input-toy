using UnityEngine;

public class SymbolSpawner : MonoBehaviour {
    public GameObject symbolPrefab;

    private void Update() {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(keyCode)) {
                instantiateSymbol(keyCode.ToString());
            }
        }
    }

    private void instantiateSymbol(string symbol) {
        var position = new Vector3(0, 0, 0);
        var rotation = Quaternion.identity;
        var obj = Instantiate(symbolPrefab, position, rotation);
        var mesh = (TextMesh) obj.GetComponent(typeof(TextMesh));
        mesh.text = symbol;
    }
}