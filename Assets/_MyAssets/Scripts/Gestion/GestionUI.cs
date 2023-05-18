using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GestionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private GameObject _pausePanel = default;

    [SerializeField] private float _vitesseEnnemi = 6.0f;
    [SerializeField] private float _vitessePommeBleue = 2.0f;
    [SerializeField] private float _augVitesseParNiveau = 1.5f;
    [SerializeField] private int _pointageAugmentation = 500;
    [SerializeField] private int _viesJoueur = 4;
    [SerializeField] private GameObject _bigExplosionPrefab = default;
    

    private int _score = 0;
    private bool _estChanger = false;
    private bool _pauseOn = false;
    
    private GestionSpawn _spawnManager;
    private void Start()
    {
        _score = 0;
        _pauseOn = false;
        Time.timeScale = 1;
        UpdateScore();
    }

    private void Update()
    {

        // Permet la gestion du panneau de pause (marche/arrêt)
        if ((Input.GetKeyDown(KeyCode.Escape) && !_pauseOn))
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && _pauseOn))
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }

        if (_score % _pointageAugmentation == 0 && _score != 0 && _estChanger == false)
        {
            AugmentVitesseEnnemi();
            _estChanger = true;
        }
        else if (_score % _pointageAugmentation != 0)
        {
            _estChanger = false;
        }
    }

    // Méthode qui change le pointage sur le UI
    private void UpdateScore()
    {
        _txtScore.text =  _score.ToString() + " pts ";
    }

    private void AugmentVitesseEnnemi()
    {
        _vitesseEnnemi += _augVitesseParNiveau;
    }

    // Méthodes publiques ==================================================

    public int getScore()
    {
        return _score;
    }
    // Méthode qui permet l'augmentation du score
    public void AjouterScore(int points)
    {
        _score += points;
        UpdateScore();
    }

   

   

    // Méthode qui relance la partie après une pause
    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }

    public void ChargerDepart()
    {
        SceneManager.LoadScene(0);
    }

    public float getVitesseEnnemi()
    {
        return _vitesseEnnemi;
    }
    public float getVitessePommeBleue()
    {
        return _vitessePommeBleue;
    }
    //public void Degats()
    //{
    //    // Si le shield est actif on le désactive sinon on enlève une vie au joueur

    //    _viesJoueur--;


    //    // Si le joueur n'a plus de vie on arrête le spwan et détruit le joueur
    //    if (_viesJoueur < 1)
    //    {
    //        _spawnManager.mortJoueur();
    //        Instantiate(_bigExplosionPrefab, transform.position, Quaternion.identity);
    //        Destroy(this.gameObject);
    //        PlayerPrefs.SetInt("Score", _score);
    //        PlayerPrefs.Save();
    //        StartCoroutine("FinPartie");
    //    }
    //}
    //IEnumerator FinPartie()
    //{
    //    yield return new WaitForSeconds(2f);
    //    SceneManager.LoadScene(2);
    //}
    

    // Méthode pour récupérer la valeur de _score
    





}
