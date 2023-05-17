using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 12f;
    [SerializeField] private GameObject _flechePrefab = default;
    [SerializeField] private float _delai = 0.5f;
    [SerializeField] private int _viesJoueur = 3;
    [SerializeField] private AudioClip _endSound = default;
    [SerializeField] private GameObject _bigExplosionPrefab = default;

    private GestionSpawn _spawnManager;
    private GestionUI _uiManager;
    private GameObject _shield;
    private Animator _anim;
    public Slider _slider;

    private float _vitesseInitiale;
    private float _canFire = -1;

    private void Awake()
    {
        _spawnManager = GameObject.Find("GestionSpawn").GetComponent<GestionSpawn>();
        _uiManager = FindObjectOfType<GestionUI>().GetComponent<GestionUI>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        transform.position = new Vector3(0f, -2.4f, 0f);  // position initiale du joueur
    }

    void Update()
    {
        Move();
        Tir();
    }

    // Méthode qui gère le tir du joueur ainsi que le délai entre chaque tir
    private void Tir()
    {
        //Debug.Log(Input.GetAxis("Fire1"));
        if (Input.GetAxis("Fire1") == 1 && Time.time > _canFire)
        {
            _canFire = Time.time + _delai;
            // Si le booléen du triplelaser est actif on instancie des triple laser à la place
            //Ne pas oublier de remettre le son
            
           Instantiate(_flechePrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity);          
        }
    }

    // Déplacements et limitation des mouvements du joueur
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(direction * Time.deltaTime * _speed);
      
        //Gérer la zone verticale
        transform.position = new Vector3(transform.position.x,
        Mathf.Clamp(transform.position.y, -3.07f, 2.3f), 0f);

        //Gérer dépassement horizontaux
        if (transform.position.x >= 11.3)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -11.3)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0f);
        }
    }

    // Méthodes publiques ==================================================================

    // Méthode appellé quand le joueur subit du dégat
    public void Degats()
    {
        // Si le shield est actif on le désactive sinon on enlève une vie au joueur
        
            _viesJoueur--;
            _slider.value = _viesJoueur;
         
        // Si le joueur n'a plus de vie on arrête le spwan et détruit le joueur
        if (_viesJoueur < 1)
        {
            _spawnManager.mortJoueur();
            Instantiate(_bigExplosionPrefab, transform.position, Quaternion.identity);         
            Destroy(this.gameObject);
        }
    }

    
    // Méthode et coroutine lié à l'augmentation de la vitesse du joueur
    public void SpeedPowerUp()
    {
        _vitesseInitiale = _speed;
        _speed = _vitesseInitiale + 5f;
        StartCoroutine(SpeedRoutine());
    }

    IEnumerator SpeedRoutine()
    {
        yield return new WaitForSeconds(5f);
        _speed = _vitesseInitiale;
    }

    // Méthode lié à l'activation du shield
    
}
