using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private GameObject poof;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Debug.Log("Coin get!");
            Destroy(gameObject);

            GameObject p = Instantiate(poof) as GameObject;
            p.transform.position = transform.position;
        }
    }
}
