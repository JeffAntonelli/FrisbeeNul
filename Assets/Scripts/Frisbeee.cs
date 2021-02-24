using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Frisbeee : MonoBehaviour
{
    public Transform player;
    [SerializeField] public float throwforce = 10; // Force de lancer
    
    private bool hasPlayer = false; // proximité du joueur pour saisir l objet
    private bool beingCarried = false; // est-ce que l objet est porté
    private bool touched = false; // detection si on touche un autre collider

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      // check distance entre objet et joueur
      float dist = Vector3.Distance(gameObject.transform.position, player.position);

      // si moins ou égale 1.9 unité de distance on peut ramasser
      if (dist <= 1f)
      {
          hasPlayer = true;
      }
      else
      {
          hasPlayer = false;
      }

      // si on peut ramasser et qu on appuie sur le bouton, on porte  lobjet
      if (hasPlayer && Input.GetButtonDown("Jump"))
      {
          GetComponent<Rigidbody2D>().isKinematic = true; // permet de porter l objet ( pas sur de l utillité)
          transform.parent = player; //
          beingCarried = true; // on porte l objet
      }

      // Si on porte l objet
      if (beingCarried)
      {
          // si on touche un autre collider en portant l objet
          if (touched)
          {
              GetComponent<Rigidbody2D>().isKinematic = false;
              transform.parent = null;
              beingCarried = false;
              touched = false;
          }

          // pour jeter l objet
          if (Input.GetButtonDown("Fire1"))
          {
              GetComponent<Rigidbody2D>().isKinematic = false;
              transform.parent = null;
              beingCarried = false;
              GetComponent<Rigidbody2D>().AddForce(player.forward * throwforce);
          }
      }
    }

    void onTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}
