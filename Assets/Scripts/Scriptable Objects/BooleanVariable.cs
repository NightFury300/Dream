using UnityEngine;

[CreateAssetMenu(fileName = "New Boolean", menuName = "Boolean")]
public class BooleanVariable : ScriptableObject
{
    [SerializeField]
    private bool initialValue;
    [HideInInspector]
    public bool runtimeValue;

    public void OnEnable()
    {
        runtimeValue = initialValue;
    }
}
