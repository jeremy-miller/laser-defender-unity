using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool isLooping = false;
    [SerializeField] private float timeBetweenWaves = 0f;
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    
    private WaveConfigSO _currentWave;

    private void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    private IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO waveConfig in waveConfigs)
            {
                _currentWave = waveConfig;
                for (int i = 0; i < _currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(_currentWave.GetEnemyPrefab(i),
                        _currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180),  // flip entire object, so enemy lasers fire correct direction
                        transform);
                    yield return new WaitForSeconds(_currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }

    public WaveConfigSO GetCurrentWave()
    {
        return _currentWave;
    }
}
