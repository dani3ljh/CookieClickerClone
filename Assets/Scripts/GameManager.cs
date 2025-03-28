using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/// <summary>
/// Manages The Game
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private TextMesh cookiesText;

    [Header("Data")]
    private BigInteger cookies = 0;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called.
    /// </summary>
    private void Start() {
        UpdateCookieText();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            IncrementCookies(1);
        }
    }

    /// <summary>
    /// Increments Cookies by count then updates cookiesText
    /// </summary>
    /// <param name="count"></param>
    public void IncrementCookies(int count) {
        cookies += count;
        UpdateCookieText();
    }

    /// <summary>
    /// Updates cookiesText
    /// </summary>
    private void UpdateCookieText() {
        cookiesText.text = FormatBigInt(cookies);
    }

    /// <summary>
    /// Formats a BigInteger into a readable number ex: 3560240 => "3.56 Million"
    /// </summary>
    /// <param name="value">BigInteger To Convert</param>
    /// <returns>Value as a readable number</returns>
    private string FormatBigInt(BigInteger value)
    {
        if (value < 1000) return value.ToString();

        string[] suffixes = {"", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion", "Vigintillion"};
        int suffixIndex = 0;
        decimal decimalValue = (decimal)value;

        while (decimalValue >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            decimalValue /= 1000;
            suffixIndex++;
        }

        return $"{decimalValue:0.##} {suffixes[suffixIndex]}";
    }
}
