using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public LSystem lSystem;

	void Start () {
        lSystem.Run(7);
    }
	
	void Update () {
		
	}
}
