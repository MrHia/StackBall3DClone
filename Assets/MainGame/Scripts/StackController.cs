using System.Collections;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private StackPartController[] _stackPartControllers = null;

    public void ShatterAllParts()
    {
        if (transform.parent!=null)
        {
            transform.parent = null;
            FindObjectOfType<BallController>().IncreaseBrokenStack();
        }

        foreach (var gO in _stackPartControllers)
        {
            gO.Shatter();
        }

        StartCoroutine(RemovePart());
    }

    IEnumerator RemovePart()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
