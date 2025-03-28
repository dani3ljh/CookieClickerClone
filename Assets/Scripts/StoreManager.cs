using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manages the Store
/// </summary>
public class StoreManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject store;
    private Animator storeAnim;
    private GameManager gm;

    [Header("Prefabs")]
    [SerializeField] private GameObject storeItem;

    [Header("Data")]
    [SerializeField] private StoreItem[] items;
    private int[] itemsBought;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called.
    /// </summary>
    private void Start() {
        storeAnim = store.GetComponent<Animator>();
        gm = GetComponent<GameManager>();
        itemsBought = new int[items.Length];

        for (int i = 0; i < items.Length; i++) {
            StartCoroutine(ClickItems(i));
        }
    }

    /// <summary>
    /// Toggles the bool parameter in storeAnim
    /// </summary>
    public void ToggleStore() {
        storeAnim.SetBool("StoreIsClosed", !storeAnim.GetBool("StoreIsClosed"));
    }

    /// <summary>
    /// Uses index i to click at the items[i]'s cps and increment by itemsBought[i]
    /// </summary>
    /// <param name="i">Index of the item</param>
    /// <returns></returns>
    private IEnumerator ClickItems(int i) {
        StoreItem item = items[i];
        if (item.GetCPS() <= 0)
            yield break;

        while (true) {
            // print($"{item.GetName()} clicks");
            gm.IncrementCookies(itemsBought[i]);

            yield return new WaitForSeconds(1f / item.GetCPS());
        }
    }
}

/// <summary>
/// Stores infromation about an item in the store
/// </summary>
[Serializable]
public class StoreItem
{
    [SerializeField] private string name;
    [SerializeField] private BigInteger price;
    [SerializeField] private float cps;

    public StoreItem(string name, BigInteger price, float cps) {
        this.name = name;
        this.price = price;
        this.cps = cps;
    }

    public string GetName() {
        return name;
    }

    public BigInteger GetPrice() {
        return price;
    }

    public float GetCPS() {
        return cps;
    }
}
