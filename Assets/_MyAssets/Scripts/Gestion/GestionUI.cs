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
    [SerializeField] private TMP_Text _txtTemps = default;

    private AudioSource _audioSource;
    private int _score = 0;
    private bool _estChanger = false;
    private bool _pauseOn = false;
    private float _tempsDepart = 0;
    private GestionSpawn _spawnManager;
    float temps;
    private void Start()
    {
        _score = 0;
        _pauseOn = false;
        Time.timeScale = 1;
        UpdateScore();
        _tempsDepart = 0;
    }

    private void Update()
    {

        
        
        temps = Time.time;

        _txtTemps.text = "Temps : " + temps.ToString("f2");

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
        temps = temps-Time.time;
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
    public void Quitter()
    {
        Application.Quit();
    }
    






}
