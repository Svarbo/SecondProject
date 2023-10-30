using UnityEngine;

public class TrapGun : MonoBehaviour
{
    [SerializeField] private ObjectPool _shells;
    [SerializeField] private Transform _startShellPosition;
    [SerializeField] private float _shootDelay;

    private float _currentDelay;
    private GameObject _currentShell;

    private void Update()
    {
        _currentDelay += Time.deltaTime;

        if (_currentDelay >= _shootDelay)
            TrySetShell();
    }

    private void TrySetShell()
    {
        if (_shells.TryGetObject(out _currentShell))
        {
            _currentShell.transform.position = _startShellPosition.position;
            _currentShell.gameObject.SetActive(true);

            _currentDelay = 0;
        }
    }
}