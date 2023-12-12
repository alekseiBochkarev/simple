using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineIn3D : MonoBehaviour {

    public bool isGizmos = true;

    public bool isLineX;
    public bool isLineZ;

    public float lengthLine = 100;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        if (!isGizmos)
            return;

        Gizmos.color = Color.red;

        if (isLineX)
            Gizmos.DrawLine(transform.position + Vector3.left * lengthLine, transform.position + Vector3.right * lengthLine);

        if (isLineZ)
            Gizmos.DrawLine(transform.position + Vector3.forward * lengthLine, transform.position + Vector3.back * lengthLine);
    }
}
