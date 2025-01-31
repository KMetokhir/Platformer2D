using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _gameObject.transform.position);
    }
}