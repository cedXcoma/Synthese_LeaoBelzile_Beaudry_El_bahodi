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

    // M�thode qui g�re le tir du joueur ainsi que le d�lai entre chaque tir
    private void Tir()
    {
        //Debug.Log(Input.GetAxis("Fire1"));
        if (Input.GetAxis("Fire1") == 1 && Time.time > _canFire)
        {
            _canFire = Time.time + _delai;
            // Si le bool�en du triplelaser est actif on instancie des triple laser � la place
            //Ne pas oublier de remettre le son
            
           Instantiate(_flechePrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity);          
        }
    }

    // D�placements et limitation des mouvements du joueur
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(direction * Time.deltaTime * _speed);
      
        //G�rer la zone verticale
        transform.position = new Vector3(transform.position.x,
        Mathf.Clamp(transform.position.y, -3.07f, 2.3f), 0f);

        //G�rer d�passement horizontaux
        if (transform.position.x >= 11.3)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -11.3)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0f);
        }
    }

    // M�thodes publiques ==================================================================

    // M�thode appell� quand le joueur subit du d�gat
    public void Degats()
    {
        // Si le shield est actif on le d�sactive sinon on enl�ve une vie au joueur
        
            _viesJoueur--;
            _slider.value = _viesJoueur;
         
        // Si le joueur n'a plus de vie on arr�te le spwan et d�truit le joueur
        if (_viesJoueur < 1)
        {
            _spawnManager.mortJoueur();
            Instantiate(_bigExplosionPrefab, transform.position, Quaternion.identity);         
            Destroy(this.gameObject);
        }
    }

    
    // M�thode et coroutine li� � l'augmentation de la vitesse du joueur
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

    // M�thode li� � l'activation du shield
    
}
