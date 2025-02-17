using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyView : CharacterAnimator
{
    [SerializeField] private Color _newColor;
    [SerializeField] private float _blinkSpeed;

    private SpriteRenderer _spriteRenderer;
    private Color _customColor = Color.white;

    private Coroutine _blinkCoroutine;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Blink()
    {
        if (_blinkCoroutine == null)
        {
            _blinkCoroutine = StartCoroutine(ChangeColor(_newColor));
        }
    }

    private IEnumerator ChangeColor(Color color)
    {
        float time = 0f;

        Color startColor = _spriteRenderer.color;

        while (_spriteRenderer.color != color)
        {
            time += Time.deltaTime * _blinkSpeed;
            _spriteRenderer.color = Vector4.MoveTowards(startColor, color, time);

            yield return null;
        }

        if (_spriteRenderer.color != _customColor)
        {
            _blinkCoroutine = StartCoroutine(ChangeColor(_customColor));
        }
        else
        {
            _blinkCoroutine = null;
        }
    }
}