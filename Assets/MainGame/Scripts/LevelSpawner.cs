using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] models;

    [HideInInspector] public GameObject[] modelPrefabs = new GameObject[4];

    public GameObject WinPrefab;

    private GameObject temp1, temp2;

    public int level = 1, addOn = 7;
    private float _index = 0;

    public Material plateMat, baseMat;

    public MeshRenderer ballMesh;

    // Start is called before the first frame update
    void Awake()
    {
        plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
        baseMat.color = plateMat.color + Color.gray;
        ballMesh.material.color = plateMat.color;
        
        
        level = PlayerPrefsManager.Instance.GetLevel();
        if (level > 9)
        {
            addOn = 0;
        }

        ModelSelecttion();
        var randomValue = Random.value;
        for (_index = 0; _index > -level - addOn; _index -= 0.5f)
        {
            if (level <= 20)
            {
                temp1 = Instantiate(modelPrefabs[Random.Range(0, 2)]);
            }

            if (level is > 20 and <= 50)
            {
                temp1 = Instantiate(modelPrefabs[Random.Range(1, 3)]);
            }

            if (level is > 50 and <= 100)
            {
                temp1 = Instantiate(modelPrefabs[Random.Range(2, 4)]);
            }

            if (level > 100)
            {
                temp1 = Instantiate(modelPrefabs[Random.Range(3, 4)]);
            }

            temp1.transform.position = new Vector3(0, _index - 0.01f, 0);
            temp1.transform.eulerAngles = new Vector3(0, _index * 8, 0);

            if (Mathf.Abs(_index) >= level * 0.3f && Mathf.Abs(_index) <= level * 0.6f)
            {
                temp1.transform.eulerAngles = new Vector3(0, _index * 8, 0);
                temp1.transform.eulerAngles = Vector3.up * 100;
            }
            else if (Mathf.Abs(_index) >= level * 0.8f)
            {
                temp1.transform.eulerAngles = new Vector3(0, _index * 8, 0);
                if (randomValue > 0.75f)
                {
                    temp1.transform.eulerAngles = Vector3.up * 180;
                }
            }

            temp1.transform.parent = FindObjectOfType<RotatorObject>().transform;
        }

        temp2 = Instantiate(WinPrefab);
        temp2.name = "WinPrefab";
        temp2.transform.position = new Vector3(0, _index - 0.01f, 0);
    }

    private void ModelSelecttion()
    {
        var randomModel = Random.Range(0, 5);
        switch (randomModel)
        {
            case 0:
                for (var i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = models[i];
                }

                break;
            case 1:
                for (var i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = models[i + 4];
                }

                break;
            case 2:
                for (var i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = models[i + 8];
                }

                break;
            case 3:
                for (var i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = models[i + 12];
                }

                break;
            case 4:
                for (var i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = models[i + 16];
                }

                break;
        }
    }
}