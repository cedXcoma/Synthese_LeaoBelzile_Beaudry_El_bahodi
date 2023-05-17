using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PommeDoree : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] ParticleSystem doree = default;
    //[SerializeField] private AudioClip _powerUpSound = default;


    // D�place le powerUp vers le bas et le d�truit s'il n'est pas saisi
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // G�re la collision et le d�clenchement de am�liorations quand le powerUp touche le joueur
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            Destroy(this.gameObject);
            Instantiate(doree, transform.position, Quaternion.identity);
            // AudioSource.PlayClipAtPoint(_powerUpSound, Camera.main.transform.position, 0.6f);                     
            player.SpeedPowerUp();              
            
        }
    }


}
