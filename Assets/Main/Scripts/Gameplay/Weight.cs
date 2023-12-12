using UnityEngine;

public class Weight : MonoBehaviour {

    public float distanceFromChainEnd;

    public float distanceToOpen = 3;

    public Vector3 startPosition;

	public void ConnectedRopeEnd(Rigidbody2D endRB)
    {
        HingeJoint2D joint = gameObject.GetComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = endRB;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0, -distanceFromChainEnd);
    }

    public void OnMouseDown()
    {
        startPosition = transform.position;
    }

    public void OnMouseDrag()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 5;
        //Vector3 mousePos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 5);

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 5;
        transform.position = mousePos;

        float currentDistance = Vector3.Distance(startPosition, transform.position);
        if (currentDistance > distanceToOpen)
        {
            transform.parent.GetComponent<Rope>().DeleteRope();
            BallController.Instance.NextState();
        }
    }
}
