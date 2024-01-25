using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] Color candleColor;
    [SerializeField] float lightSize = 1f;

    private void Update()
    {
        Shader.SetGlobalVector("_CandlePos" ,transform.position);
        Shader.SetGlobalColor("_CandleColor" , candleColor);
        Shader.SetGlobalFloat("_CandleSize" , lightSize);
    }
}
