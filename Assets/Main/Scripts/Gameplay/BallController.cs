using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Beebyte.Obfuscator;
using UnityEngine.Events;

public class BallController : Singleton<BallController>
{
    public Transform ballSpawnParent;
    public GameObject ballPrefab;

    public Animator cameraAnimator;
    public Rope rope;

    private GameObject currentBall;
    private Animator currentBallAnimator;
    private MeshRenderer currentBallLayer1;
    private MeshRenderer currentBallLayer2;
    private MeshRenderer currentBallLayer3;

    private int currentNumberState = 0;

    public Slider openBallSlider;
    private bool isCheckSlider = false;

    private bool isRemoveLayerProcess = false;
    private float timer = 0;

    public RightShopController shopController;
    public GameObject iconOpenItem;
    public GameObject iconOpenCharacter;

    public Text textNumberRemainingBalls;
    [HideInInspector]
    public int curNumberRemainingBalls = 0;

    public bool isBallActive { get; private set; }

    public Image imageBallBackground;

    public GameObject rays;

    public List<ParticleSystem> confetti;

    [Header("Tutorial")]
    public GameObject textPull;
    public GameObject hand;

    public UnityEvent BallComplete { get; set; } = new UnityEvent();

    public static BallController singleton;

    private void Awake()
    {
        singleton = this;
    }
	
	public void SpawnBall()
    {
        if (isBallActive)
            return;

        openBallSlider.gameObject.SetActive(false);

        ShowNumberRemainingBalls();

        if (curNumberRemainingBalls == 0)
        {
            GetComponent<ShopMenu>().ShowPanelWarning();
            return;
        }

        isBallActive = true;

        ShowNumberRemainingBalls();

        currentBall = Instantiate(ballPrefab, new Vector3(100, 100, 0), Quaternion.identity, ballSpawnParent);

        InitializeBallAnimator();

        currentNumberState = 0;

        NextState();
    }

    private void InitializeBallAnimator()
    {
        currentBallAnimator = currentBall.GetComponent<Animator>();
        currentBallLayer1 = currentBall.transform.Find("Layer1").GetComponent<MeshRenderer>();
        currentBallLayer2 = currentBall.transform.Find("Layer2").GetComponent<MeshRenderer>();
        currentBallLayer3 = currentBall.transform.Find("Layer3").GetComponent<MeshRenderer>();
    }

    public void ShowNumberRemainingBalls()
    {
        if (curNumberRemainingBalls == 0)
        {
            textNumberRemainingBalls.text = "+";
            imageBallBackground.color = Color.white;
        }
        else
        {
            textNumberRemainingBalls.text = curNumberRemainingBalls.ToString();
            imageBallBackground.color = new Color(1, 1, 1, 0);
        }
    }

    [Skip]
    public void NextState()
    {
        switch(currentNumberState)
        {
            case 0: // появление шара
                currentBallAnimator.SetTrigger("start");
                break;

            case 1: // появление слайдера
                openBallSlider.gameObject.SetActive(true);
                openBallSlider.value = 0;
                isCheckSlider = true;
                if (SaveManager.GetKey("TutorialHand", 0) == 0)
                    hand.SetActive(true);
                break;

            case 2: // открыть слой
                currentBallLayer1.materials[1].SetFloat("_Cutoff", 1);
                openBallSlider.gameObject.SetActive(false);
                isCheckSlider = false;
                isRemoveLayerProcess = true;
                timer = 0;
                curNumberRemainingBalls--;
                SaveManager.SetKey("NumberRemainingBalls", curNumberRemainingBalls);
                ShowNumberRemainingBalls();
                break;

            case 3: // анимация прокрутки шара
                hand.SetActive(false);
                SaveManager.SetKey("TutorialHand", 1);
                currentBallLayer1.gameObject.SetActive(false);
                isRemoveLayerProcess = false;
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 4: // появление слайдера
                openBallSlider.gameObject.SetActive(true);
                openBallSlider.value = 0;
                isCheckSlider = true;
                break;

            case 5: // открыть слой
                currentBallLayer2.materials[1].SetFloat("_Cutoff", 1);
                openBallSlider.gameObject.SetActive(false);
                isCheckSlider = false;
                isRemoveLayerProcess = true;
                timer = 0;
                break;

            case 6: // анимация прокрутки шара
                currentBallLayer2.gameObject.SetActive(false);
                isRemoveLayerProcess = false;
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 7: // появление слайдера
                openBallSlider.gameObject.SetActive(true);
                openBallSlider.value = 0;
                isCheckSlider = true;
                break;

            case 8: // открыть слой
                currentBallLayer3.materials[1].SetFloat("_Cutoff", 1);
                openBallSlider.gameObject.SetActive(false);
                isCheckSlider = false;
                isRemoveLayerProcess = true;
                timer = 0;
                break;

            case 9: // анимация прокрутки в положение forward
                currentBallLayer3.gameObject.SetActive(false);
                isRemoveLayerProcess = false;
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 10:    // открыть первые двери
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 11:    // появляется случайный головной убор
                rays.SetActive(true);
                foreach (ParticleSystem i in confetti) i.Play();
                iconOpenItem.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                iconOpenItem.SetActive(true);
                iconOpenItem.GetComponent<Image>().sprite = OpenRandomItem(ItemController.enTypeItem.HAT);
                iconOpenItem.GetComponent<Animator>().SetBool("isShow", true);
                SoundController.Instance.getItem.Play();
                break;

            case 12:
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 13:
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 14:
                rays.SetActive(true);
                foreach (ParticleSystem i in confetti) i.Play();
                iconOpenItem.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                iconOpenItem.SetActive(true);
                iconOpenItem.GetComponent<Image>().sprite = OpenRandomItem(ItemController.enTypeItem.DRESS);
                iconOpenItem.GetComponent<Animator>().SetBool("isShow", true);
                SoundController.Instance.getItem.Play();
                break;

            case 15:
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 16:
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 17:
                rays.SetActive(true);
                foreach (ParticleSystem i in confetti) i.Play();
                iconOpenItem.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                iconOpenItem.SetActive(true);
                iconOpenItem.GetComponent<Image>().sprite = OpenRandomItem(ItemController.enTypeItem.BOTTLE);
                iconOpenItem.GetComponent<Animator>().SetBool("isShow", true);
                SoundController.Instance.getItem.Play();
                break;

            case 18:
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 19:
                currentBallAnimator.SetTrigger("nextState");
                break;

            case 20:
                rays.SetActive(true);
                foreach (ParticleSystem i in confetti) i.Play();
                iconOpenItem.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                iconOpenItem.SetActive(true);
                iconOpenItem.GetComponent<Image>().sprite = OpenRandomItem(ItemController.enTypeItem.BOOTS);
                iconOpenItem.GetComponent<Animator>().SetBool("isShow", true);
                SoundController.Instance.getItem.Play();
                break;

            case 21:
                cameraAnimator.SetTrigger("down");
                rope.gameObject.SetActive(true);
                rope.GenerateRope();
                textPull.SetActive(true);
                break;

            case 22:
                cameraAnimator.SetTrigger("up");
                rope.gameObject.SetActive(false);
                currentBallAnimator.SetTrigger("nextState");
                textPull.SetActive(false);
                break;

            case 23:
                rays.SetActive(true);
                foreach (ParticleSystem i in confetti) i.Play();
                iconOpenCharacter.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 50);
                iconOpenCharacter.SetActive(true);
                iconOpenCharacter.GetComponent<Image>().sprite = OpenRandomDoll();
                iconOpenCharacter.GetComponent<Animator>().SetBool("isShow", true);
                SoundController.Instance.getItem.Play();
                break;

            case 24:
                Finish();
                currentNumberState--;
                break;
        }

        currentNumberState++;
    }

    public Sprite OpenRandomItem(ItemController.enTypeItem _typeItem)
    {
        int randomNumber = 0;

        if (_typeItem == ItemController.enTypeItem.HAT)
        {
            randomNumber = Random.Range(0, shopController.hatIconsBig.Count);
            SaveManager.SetKey("HatItem" + randomNumber, 1);
            return shopController.hatIconsBig[randomNumber];
        }
        else if (_typeItem == ItemController.enTypeItem.DRESS)
        {
            randomNumber = Random.Range(0, shopController.dressesIconsBig.Count);
            SaveManager.SetKey("DressItem" + randomNumber, 1);
            return shopController.dressesIconsBig[randomNumber];
        }
        else if (_typeItem == ItemController.enTypeItem.BOTTLE)
        {
            randomNumber = Random.Range(0, shopController.bottleIconsBig.Count);
            SaveManager.SetKey("BottleItem" + randomNumber, 1);
            return shopController.bottleIconsBig[randomNumber];
        }
        else if (_typeItem == ItemController.enTypeItem.BOOTS)
        {
            randomNumber = Random.Range(0, shopController.bootsIconsBig.Count);
            SaveManager.SetKey("BootsItem" + randomNumber, 1);
            return shopController.bootsIconsBig[randomNumber];
        }

        return null;
    }

    public Sprite OpenRandomDoll()
    {
        int randomNumber = 0;

        randomNumber = Random.Range(0, shopController.dollIcon.Count);
        SaveManager.SetKey("Doll" + randomNumber, 1);
        return shopController.dollIcon[randomNumber];
    }

    [Skip]
    public void ClickOnNewItem()
    {
        rays.SetActive(false);
        iconOpenItem.SetActive(false);
        iconOpenCharacter.SetActive(false);
        currentBallAnimator.SetTrigger("nextState");
    }

    private void Finish()
    {
        isBallActive = false;
        Destroy(currentBall);
        SpawnBall();
        BallComplete.Invoke();
    }

    private void Update()
    {
        if (isRemoveLayerProcess && (currentNumberState == 3 || currentNumberState == 6 || currentNumberState == 9))
        {
            timer += Time.deltaTime;
            if (currentNumberState == 3)
                currentBallLayer1.materials[0].SetFloat("_Cutoff", timer);
            else if (currentNumberState == 6)
                currentBallLayer2.materials[0].SetFloat("_Cutoff", timer);
            else if (currentNumberState == 9)
                currentBallLayer3.materials[0].SetFloat("_Cutoff", timer);

            if (timer >= 0.6f)
            {
                NextState();
            }
        }

        if (isCheckSlider)
        { 
            if (openBallSlider.value >= 0.8f)
            {
                NextState();
            }

            if (currentNumberState == 2)
            {
                currentBallLayer1.materials[1].SetFloat("_Cutoff", openBallSlider.value);
            }
            else if (currentNumberState == 5)
            {
                currentBallLayer2.materials[1].SetFloat("_Cutoff", openBallSlider.value);
            }
            else if (currentNumberState == 8)
            {
                currentBallLayer3.materials[1].SetFloat("_Cutoff", openBallSlider.value);
            }
        }
    }

    public void AddBall(int _countBalls = 1)
    {
        if (curNumberRemainingBalls == 0)
            ShopMenu.singleton.HidePanelWarning();

        curNumberRemainingBalls += _countBalls;
        SaveManager.SetKey("NumberRemainingBalls", curNumberRemainingBalls);
        ShowNumberRemainingBalls();

        if (!isBallActive)
            SpawnBall();
    }
}
