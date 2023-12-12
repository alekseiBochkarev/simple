using UnityEngine;

public class Rope : MonoBehaviour {

    public Rigidbody2D hook;

    public GameObject linkPrefab;

    public Weight weight;

    public int links = 7;

	// Use this for initialization
	void Start () {
        //GenerateRope();
	}
	
	public void GenerateRope()
    {
        Rigidbody2D previousRB = hook;
        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform.position, Quaternion.identity, transform);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;

            if (i < links - 1)
                previousRB = link.GetComponent<Rigidbody2D>();
            else
                weight.ConnectedRopeEnd(link.GetComponent<Rigidbody2D>());
        }
    }

    public void DeleteRope()
    {
        foreach(Transform i in transform)
        {
            if (i.name != "Hook" && i.name != "Weight")
                Destroy(i.gameObject);
        }
    }
}
