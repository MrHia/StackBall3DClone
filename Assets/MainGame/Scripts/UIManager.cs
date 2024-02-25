using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject homeUI, inGameUI, finishGameUI, gameOverUI;
    [SerializeField] private GameObject allBtn;
    private bool _buttons;

    [Header("PreGame")] public Button soundBtn;
    public Sprite soundOn;
    public Sprite soundOff;

    [Header("InGame")] [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image levelSlider;
    [SerializeField] private Image currentLevelImg;
    [SerializeField] private Image nextLevelImage;
    [SerializeField] private TMP_Text currentLevText;
    [SerializeField] private TMP_Text nextLevelText;
    private Material _ballMaterial;
    private BallController _ball;
    [Header("GameOver")]
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text bestScoreText;
    [Header("Game Finish")]
    [SerializeField] private TMP_Text finishLevelText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _ballMaterial = FindObjectOfType<BallController>().transform.GetChild(0).GetComponent<MeshRenderer>().material;
        _ball = FindObjectOfType<BallController>();
        levelSlider.transform.parent.GetComponent<Image>().color = _ballMaterial.color + Color.gray;
        levelSlider.color = _ballMaterial.color;
        currentLevelImg.color = _ballMaterial.color;
        nextLevelImage.color = _ballMaterial.color;
        
        
    }
    
    public void SetTextScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void Start()
    {
        scoreText.text = "0";
        currentLevText.text = PlayerPrefsManager.Instance.GetLevel().ToString();
        nextLevelText.text = (PlayerPrefsManager.Instance.GetLevel()+1).ToString();
        soundBtn.onClick.AddListener(SoundManager.Instance.SoundOff);
    }

    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void Setting()
    {
        _buttons = !_buttons;
        allBtn.SetActive(_buttons);
    }

    private void Update()
    {
        if (_ball.ballState == BallState.Prepare)
        {
            if (SoundManager.Instance.sound && soundBtn.GetComponent<Image>().sprite != soundOn)
            {
                soundBtn.GetComponent<Image>().sprite = soundOn;
            }
            else if (!SoundManager.Instance.sound && soundBtn.GetComponent<Image>().sprite != soundOff)
            {
                soundBtn.GetComponent<Image>().sprite = soundOff;
            }
        }

        if (_ball.ballState == BallState.Died  && Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.ResetLevel();
        }
        if (_ball.ballState == BallState.Finish  && Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.NextLevel();
            
        }
        if (_ball.ballState == BallState.Prepare && !IgnoreUI() && Input.GetMouseButtonDown(0))
        {
            _ball.ballState = BallState.Playing;
            homeUI.SetActive(false);
            inGameUI.SetActive(true);
            finishGameUI.SetActive(false);
            gameOverUI.SetActive(false);
        }
    }

    public void ShowGameOverUI()
    {
        homeUI.SetActive(false);
        inGameUI.SetActive(false);
        finishGameUI.SetActive(false);
        gameOverUI.SetActive(true);
        gameOverScoreText.text = Score.Instance.score.ToString();
        bestScoreText.text = PlayerPrefsManager.Instance.GetScore().ToString();
    }
    public void ShowFinishGameUI()
    {
        homeUI.SetActive(false);
        inGameUI.SetActive(false);
        finishGameUI.SetActive(true);
        gameOverUI.SetActive(false);
        finishLevelText.text = $"LEVEL {PlayerPrefsManager.Instance.GetLevel()}";
    }
    public bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if (raycastResultsList[i].gameObject.GetComponent<Ignore>() != null)
            {
                //Debug.Log($"Hit before remove {raycastResultsList.Count > 0}");
                raycastResultsList.RemoveAt(i);
                i--;
                //Debug.Log($"Hit after remove {raycastResultsList.Count > 0} && i: {i}");
            }
        }

        return raycastResultsList.Count > 0;
    }
}