using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetGetter
{
    public static Dictionary<string, Dictionary<string, string>> data;

    public static event Action Finished;

    private static string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vT5HdaSuzUGAe6OYHMpWJMFH2qcTxgWE2UdLlE53NxoXsfJE64S9TONz3fJo3iR95Pd4OincJKEnhPA/pub?output=csv";

    private static UnityWebRequestAsyncOperation asyncOperation;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnApplicationStart()
    {
        asyncOperation = UnityWebRequest.Get(url).SendWebRequest();
        asyncOperation.completed += AsyncOperation_completed;
    }

    private static void AsyncOperation_completed(AsyncOperation obj)
    {
        if(asyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("ERROR");
            Debug.Log(asyncOperation.webRequest.error);
        }
        else
        {
            Parse(asyncOperation.webRequest.downloadHandler.text);
            Finished?.Invoke();
        }
    }

    private static void Parse(string input)
    {
        data = new Dictionary<string, Dictionary<string, string>>();
        string[,] cash = ParseCSVString(input);

        for (int i = 0; i < cash.GetLength(0); i++)
        {
            if (i <= 0) continue;
            data.Add(cash[i, 0], new Dictionary<string, string>());
            for (int j = 0; j < cash.GetLength(1); j++)
            {
                data[cash[i, 0]].Add(cash[0, j], cash[i, j]);
            }
        }
    }

    public static string[,] ParseCSVString(string csvString)
    {
        List<string[]> rows = new List<string[]>();
        string[] lines = csvString.Split('\n');

        foreach (string line in lines)
        {
            string[] columns = line.Trim().Split(',');
            rows.Add(columns);
        }

        int numRows = rows.Count;
        int numCols = rows[0].Length;

        string[,] result = new string[numRows, numCols];
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                result[i, j] = rows[i][j];
            }
        }

        return result;
    }
}
