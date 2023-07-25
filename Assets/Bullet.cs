using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody _body;
    private Mind _playerMind;

    private GameObject _death;



  
    public void Launch(Mind target, GameObject deathScreen)
    {
        _death = deathScreen;     
       _body = GetComponent<Rigidbody>();
       _playerMind = target;     
       _body.AddForce((target.transform.position - transform.position).normalized * 3000f);
    }


 
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Body player))
        {
            // player.DoSomething;
            _playerMind.SetProjectionSize(-0.4f);
            if(_playerMind.GetProjectionSize() < 0.1f)
                _death.SetActive(true);

            Destroy(gameObject);

        }

    }

}
