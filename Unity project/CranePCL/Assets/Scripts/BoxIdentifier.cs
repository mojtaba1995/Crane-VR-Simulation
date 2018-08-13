using UnityEngine;
using System.Collections;

public class BoxIdentifier : MonoBehaviour {

    public GameObject MainMesh;

	// Use this for initialization
	void Start () {
	



	}
	
	// Update is called once per frame
	void Update () {


        Renderer rend = (Renderer)gameObject.GetComponent(typeof(Renderer));
        

        foreach (Transform child in MainMesh.transform) {
            Renderer meshRend = (Renderer)child.GetComponent(typeof(Renderer));
            meshRend.material.color = rend.material.color;
        }

        
        

    }
}
