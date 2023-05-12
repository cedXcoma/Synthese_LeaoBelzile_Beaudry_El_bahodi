using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = default;
    [SerializeField] private GameObject _container = default;
    [SerializeField] private GameObject[] _powerUpPrefab = default;

    private bool _stopSpawn = false;

    void Start()
    {
        StartSpawning();  //Déclenche les coroutine pour le spawn des ennemis et des améliorations
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnCoroutine());
        StartCoroutine(SpawnPUCoroutine());
    }

    // Coroutine pour l'apparition des PowerUps
    IEnumerator SpawnPUCoroutine()
    {
        yield return new WaitForSeconds(3f);
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            //Choisi au hasard un powerUp faisant partie du tableau et l'instancie
            int randomPU = Random.Range(0, _powerUpPrefab.Length);
            Instantiate(_powerUpPrefab[randomPU], positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6f, 10f));
        }
    }

    //Coroutine pour l'apparition des ennemis
    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(1f); // Délai initial
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, positionSpawn, Quaternion.identity);
            newEnemy.transform.parent = _container.transform;
            yield return new WaitForSeconds(3f);
        }

    }

    // Méthodes publiques ========================================================

    // Arrête le spawn à la mort du joueur (fin de partie)
    public void mortJoueur()
    {
        _stopSpawn = true;
    }
}
