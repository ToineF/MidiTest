using UnityEngine;

public class HitzoneActivator : MonoBehaviour
{
    [SerializeField] private GameObject _collider;
    [SerializeField] private KeyboardKey _key;

    private void Start()
    {
        _key.OnOn += () => _collider.SetActive(true);
        _key.OnOff += () => _collider.SetActive(false);
    }
}
