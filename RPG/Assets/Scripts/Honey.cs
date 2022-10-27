using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Honey : MonoBehaviourPun
{
    [Header("Info")]
    public int curHp;
    public int maxHp;
    //public string objectToSpawnOnDeath;

    [Header("Components")]
    public SpriteRenderer sr;
    public GameObject honeyBlock;
    public Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D> ();
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        curHp -= damage;

        // update health bar
        //healthBar.photonView.RPC("UpdateHealthBar", RpcTarget.All, curHp);

        if(curHp <= 0)
            Die();
        else
        {
            photonView.RPC("FlashDamage", RpcTarget.All);
        }
    }

    [PunRPC]
    void FlashDamage()
    {
        StartCoroutine(DamageFlash());

            IEnumerator DamageFlash()
            {
                sr.color = Color.red;
                yield return new WaitForSeconds(0.05f);
                sr.color = Color.white;
            }
    }

    void Die()
    {
        //if(objectToSpawnOnDeath != string.Empty)
            //PhotonNetwork.Instantiate(objectToSpawnOnDeath, transform.position, Quaternion.identity);

        // destroy object accross network
        PhotonNetwork.Destroy(gameObject);
    }
}
