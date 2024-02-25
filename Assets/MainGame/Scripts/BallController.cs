using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Rigidbody _rb;
    private float currentTime;

    private bool _smash, invincible;


    [HideInInspector] public BallState ballState = BallState.Prepare;
    private float _speed = 7;
    public AudioClip bounceOffClip, deadClip, winClip, destroyClip, iDestroyClip;
    private float _currentBrokenStack, _totalStack;

    public GameObject invincibleObj;
    public Image invincibleFill;
    public GameObject fireEffect, winEffect, splashEffect;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currentBrokenStack = 0;
    }

    private void Start()
    {
        _totalStack = FindObjectsOfType<StackController>().Length;
    }

    private void Update()
    {
        if (ballState == BallState.Playing)
        {
            InputBall();
        }


        /*if (ballState == BallState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        }*/
    }

    private void FixedUpdate()
    {
        if (ballState == BallState.Playing)
        {
            if (Input.GetMouseButton(0))
            {
                _smash = true;
                _rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * _speed, 0);
            }
        }

        if (_rb.velocity.y > 5)
        {
            var velocity = _rb.velocity;
            velocity = new Vector3(velocity.x, 5, velocity.z);
            _rb.velocity = velocity;
        }
    }

    public void IncreaseBrokenStack()
    {
        _currentBrokenStack++;
        if (!invincible)
        {
            Score.Instance.AddScore(1);
            SoundManager.Instance.PlaySoundFX(destroyClip, 0.5f);
        }
        else
        {
            Score.Instance.AddScore(2);
            SoundManager.Instance.PlaySoundFX(iDestroyClip, 0.5f);
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (!_smash)
        {
            _rb.velocity = new Vector3(0, 50 * Time.fixedDeltaTime * 5, 0);

            if (!target.gameObject.CompareTag("Finish"))
            {
                var splash = Instantiate(splashEffect, target.gameObject.transform, true);
                splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);
                var position = transform.position;
                splash.transform.localScale = transform.localScale * 0.5f;
                splash.transform.position = new Vector3(position.x, position.y - 0.22f, position.z);
                splash.GetComponent<SpriteRenderer>().color =
                    transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                Destroy(splash,3f);
            }

            SoundManager.Instance.PlaySoundFX(bounceOffClip, 0.5f);
        }
        else
        {
            if (invincible)
            {
                if (target.gameObject.CompareTag("enemy") || target.gameObject.CompareTag("plane"))
                {
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();
                    //Debug.Log("destroy when invincible");
                }
            }
            else
            {
                if (target.gameObject.CompareTag("enemy"))
                {
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();
                    //Debug.Log("enemy");
                }

                if (target.gameObject.CompareTag("plane"))
                {
                    //Debug.Log("Game Over");

                    _rb.isKinematic = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    ballState = BallState.Died;
                    SoundManager.Instance.PlaySoundFX(deadClip, 0.5f);
                    UIManager.Instance.ShowGameOverUI();
                }
            }
        }

        UIManager.Instance.LevelSliderFill(_currentBrokenStack / _totalStack);
        if (target.gameObject.CompareTag("Finish") && ballState == BallState.Playing)
        {
            ballState = BallState.Finish;
            SoundManager.Instance.PlaySoundFX(winClip, 0.7f);
            var winGo = Instantiate(winEffect, Camera.main.transform, true);
            winGo.transform.localPosition = Vector3.up * 1.5f;
            winGo.transform.eulerAngles = Vector3.zero;
            UIManager.Instance.ShowFinishGameUI();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!_smash || other.gameObject.CompareTag("Finish"))
        {
            _rb.velocity = new Vector3(0, 50 * Time.fixedDeltaTime * 5, 0);
        }
    }

    private void InputBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _smash = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _smash = false;
        }

        if (invincible)
        {
            currentTime -= Time.deltaTime * 0.35f;
            if (!fireEffect.activeInHierarchy)
                fireEffect.SetActive(true);
        }
        else
        {
            if (fireEffect.activeInHierarchy)
                fireEffect.SetActive(false);
            if (_smash)
            {
                currentTime += Time.deltaTime * 0.8f;
            }
            else
            {
                currentTime -= Time.deltaTime * 0.5f;
            }
        }

        if (currentTime >= 0.3f || invincibleFill.color == Color.red)
        {
            invincibleObj.SetActive(true);
        }
        else
        {
            invincibleObj.SetActive(false);
        }

        if (currentTime >= 1)
        {
            currentTime = 1;
            invincible = true;
            invincibleFill.color = Color.red;
        }
        else if (currentTime <= 0)
        {
            currentTime = 0;
            invincible = false;
            invincibleFill.color = Color.white;
        }

        if (invincibleObj.activeInHierarchy)
        {
            invincibleFill.fillAmount = currentTime / 1;
        }
    }
}

public enum BallState
{
    Prepare,
    Playing,
    Died,
    Finish
}