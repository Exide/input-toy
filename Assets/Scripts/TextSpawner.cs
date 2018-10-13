using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextSpawner : MonoBehaviour {
    public GameObject textPrefab;

    private readonly List<string> ignoredKeyCodes = new List<string> {
        "AltGr"
    };

    private readonly List<string> suffixTrimmedKeyCodes = new List<string> {
        "Alpha",
        "Keypad"
    };

    private readonly List<string> prefixTrimmedKeyCodes = new List<string> {
        "Control",
        "Shift",
        "Alt",
        "Command",
        "Apple",
        "Windows"
    };

    private readonly Dictionary<string, string> replacedKeyCodes = new Dictionary<string, string> {
        {"Plus", "+"},
        {"Minus", "-"},
        {"Multiply", "*"},
        {"Divide", "/"},
        {"Period", "."},
        {"Equals", "="},
        {"BackQuote", "`"},
        {"Exclaim", "!"},
        {"At", "@"},
        {"Hash", "#"},
        {"Dollar", "$"},
        {"Caret", "^"},
        {"Ampersand", "&"},
        {"LeftParen", "("},
        {"RightParen", ")"},
        {"Underscore", "_"},
        {"LeftBracket", "["},
        {"RightBracket", "]"},
        {"Backslash", "\\"},
        {"Semicolon", ";"},
        {"Quote", "'"},
        {"Comma", ","},
        {"Slash", "/"}
    };

    private void Update() {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
            if (!Input.GetKeyDown(keyCode)) continue;
            if (ignoredKeyCodes.Contains(keyCode.ToString())) continue;
            var text = buildTextFromKeyCode(keyCode);
            instantiateText(text);
        }
    }

    private string buildTextFromKeyCode(KeyCode keyCode) {
        var text = keyCode.ToString();

        foreach (var code in prefixTrimmedKeyCodes) {
            if (text.Contains(code)) {
                text = code;
            }
        }

        foreach (var code in suffixTrimmedKeyCodes) {
            if (text.StartsWith(code)) {
                text = text.Substring(code.Length);
            }
        }

        if (replacedKeyCodes.ContainsKey(text)) {
            text = replacedKeyCodes[text];
        }

        return text;
    }

    private void instantiateText(string text) {
        var position = getRandomVisiblePosition();
        var rotation = Quaternion.identity;
        var obj = Instantiate(textPrefab, position, rotation);
        var mesh = (TextMeshPro) obj.GetComponent(typeof(TextMeshPro));
        mesh.text = text;
    }

    private Vector3 getRandomVisiblePosition() {
        if (Camera.main == null) throw new NoCameraException();
        var x = Random.Range(100, Screen.width - 100);
        var y = Random.Range(100, Screen.height - 100);
        var z = Camera.main.farClipPlane - 1;
        var screenPosition = new Vector3(x, y, z);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
}