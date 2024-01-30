using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetOnRelease: MonoBehaviour
{
    [SerializeField] Transform a;
    Vector3 scale;

    private void Start()
    {
        scale = transform.localScale;
        GameManager.Instance.goToMapButton.onClick.AddListener(ResetMap);
    }

    public void ResetMap()
    {
        transform.position = a.position;
        transform.localScale = scale;
    }

}
