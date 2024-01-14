using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private BulletPool _bulletPool = null;
    [SerializeField]
    private BulletSpawner _bulletSpawner = null;
    [SerializeField]
    private Player _player = null;
    [SerializeField]
    private Target _target = null;
    [SerializeField]
    private GameObject _endgameCanvas = null;
    [SerializeField]
    private EndgameScreenManager _endgameScreenManager = null;

    private string _victoryText = "You won!";
    private string _loseText = "You lost!";
    private bool _isPathNotClear = true;

    public event EventHandler PathClearEvent = null;

    private void Start()
    {
        _bulletPool.BulletReleasedEvent += HandleBulletRelease;
        _target.TargetReachedEvent += HandleTargetReached;
        _endgameScreenManager.ReplayEvent += HandleReplay;
        _player.CriticalScaleReachedEvent += HandleCriticalScaleReached;
    }

    private void HandleBulletRelease(object sender, EventArgs args)
    {
        if (!_isPathNotClear) return;

        Vector2 castOrigin = _player.transform.position;
        float radius = _player.transform.lossyScale.x * 0.5f;
        Vector2 direction = _target.transform.position - _player.transform.position;
        int layerMask = 1 << 6;

        _isPathNotClear = Physics2D.CircleCast(castOrigin, radius, direction, direction.magnitude, layerMask);

        if (!_isPathNotClear)
        {
            _bulletSpawner.IsShootingAllowed = false;
            PathClearEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleTargetReached(object sender, EventArgs args)
    {
        _player.Rigidbody2D.bodyType = RigidbodyType2D.Static;
        _player.CircleCollider2D.enabled = false;

        _endgameCanvas?.SetActive(true);
        _endgameScreenManager.Text = _victoryText;
        //Debug.Log("Target reached event handled!");
    }

    private void HandleReplay(object sender, EventArgs args)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    private void HandleCriticalScaleReached(object sender, EventArgs args)
    {
        _bulletSpawner.IsShootingAllowed = false;
        _endgameCanvas?.SetActive(true);
        _endgameScreenManager.Text = _loseText;
    }
}
