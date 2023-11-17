using UnityEngine;
using UnityEngine.SceneManagement;

public class MediumLevelStrategy : LevelDifficultStrategy
{
    private readonly Player _player;
    private readonly Vector3 _startSpawnPosition;
    private LevelsInfo _levelsInfo;
    private GameBootstrap _gameBootstrap;
    private readonly PlayerCanvasDrawer _playerCanvasDrawer;

    public MediumLevelStrategy(Player player, Vector3 startSpawnPosition, LevelsInfo levelsInfo, GameBootstrap gameBootstrap)
    {
        _player = player;
        _startSpawnPosition = startSpawnPosition;
        _levelsInfo = levelsInfo;
        _gameBootstrap = gameBootstrap;

        _playerCanvasDrawer = _player.GetComponentInChildren<PlayerCanvasDrawer>();
        _playerCanvasDrawer.LosePanel.TryAgain += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _playerCanvasDrawer.LosePanel.gameObject.SetActive(false);
        _levelsInfo.SceneName = SceneManager.GetActiveScene().name;
        _gameBootstrap.AppCore.StateMachine.Enter(typeof(LoadLevelState), _levelsInfo);
    }

    public override void Execute()
    {
    }
}