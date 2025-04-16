using UnityEngine;


public class KeyboardKey : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;

    private Color _startColor;
    private void Start()
    {
        _startColor = _sprite.color;
    }

    public void On(float velocity)
    {
        _sprite.color = Color.yellow;
        _animator.SetBool("Sings", true);
    }

    public void Off()
    {
        _sprite.color = _startColor;
        _animator.SetBool("Sings", false);
    }
}