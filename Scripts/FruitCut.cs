using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCut : MonoBehaviour {

    public Material material;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;
        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, material);
        if (!pieces[1].GetComponent<Rigidbody>())
            pieces[1].AddComponent<Rigidbody>();
        Destroy(pieces[1], 1);

        if (victim.tag == "GO")
        {
            
            FruitSpawn.Instance.RestartGame();
            Rigidbody rigibody = pieces[0].GetComponent<Rigidbody>();
            rigibody.useGravity = false;
            Destroy(pieces[0], 1);

        }
        else
        {

            FruitSpawn.Instance.AddScore();
            Hand.Instance.GetHandShaking();

        }
        Hand.Instance.StopCoroutine();


        
    }
}
