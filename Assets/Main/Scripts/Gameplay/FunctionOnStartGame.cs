using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionOnStartGame : MonoBehaviour {

    public RightShopController rightShopController;

	void Start ()
    {
        StartCoroutine(AllowLists());
	}

    private IEnumerator AllowLists()
    {
        /*rightShopController.hatIconsBig = rightShopController.CreateBigIcons(rightShopController.hatIcons, new Vector2(0, 192), new Vector2(256, 448));
        rightShopController.dressesIconsBig = rightShopController.CreateBigIcons(rightShopController.dressesIcons, new Vector2(0, 128), new Vector2(256, 384));
        rightShopController.bootsIconsBig = rightShopController.CreateBigIcons(rightShopController.bootsIcons, new Vector2(0, 0), new Vector2(256, 256));
        rightShopController.bottleIconsBig = rightShopController.CreateBigIcons(rightShopController.bottleIcons, new Vector2(0, 64), new Vector2(256, 320));*/

        yield return null;
    }
}
