using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float Value", menuName = "Float Value")]
public class FloatVariable : ScriptableObject {
#if UNITY_EDITOR
    [Multiline] public string DeveloperDescription = "";
#endif
    public float Value = 0;

    public void SetValue(float value) {
        Value = value;
    }

    public void SetValue(FloatVariable value) {
        Value = value.Value;
    }

    public void ApplyChange(float amount) {
        Value += amount;
    }

    public void ApplyChange(FloatVariable amount) {
        Value += amount.Value;
    }
}