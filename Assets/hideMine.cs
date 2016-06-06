using UnityEngine;
using System.Collections;

public class hideMine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.tag == "mine")
            other.transform.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
