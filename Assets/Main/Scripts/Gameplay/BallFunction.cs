using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFunction : MonoBehaviour
{
	public void NextState()
    {
        if (BallController.singleton != null)
            BallController.singleton.NextState();
        else
            Invoke("NextState", 0.1f);
    }
}