using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class JokerManager : MonoBehaviour
{
    [Header("Scripts"), Space]
    public JokerDatabase jokerDatabase;
    public PlayerGameInfo PGI;
    public JokerSystem jokerSystem;

    [Header("Objects"), Space]
    public Transform jokerArea; // UI parent (like handArea)
    public GameObject jokerPrefab;

    [Header("Variables"), Space]
    public List<JokerData> ownedJokers = new List<JokerData>();

    public float jokerSpacing = 150f;
    
    public void GiveJoker(int index) // DEBUG: give joker by index
    {
        if (index < 0 || index >= jokerDatabase.jokers.Count)
        {
            Debug.LogError("Invalid joker index");
            return;
        }

        if (PGI.jokers >= PGI.jokerSlots)
        {
            Debug.Log("<color=red>Error: </color>Max Jokers reached!");
            return;
        }

        JokerData data = jokerDatabase.GetJoker(index);

        if (data == null)
        {
            Debug.LogError($"Joker is NULL at index {index}");
            return;
        }

        ownedJokers.Add(data);
        SpawnJoker(data);
    }

    public void SpawnJoker(JokerData data) // Spawns UI object
    {
        GameObject obj = Instantiate(jokerPrefab, jokerArea, false);
        PGI.jokers ++;

        JokerDisplay display = obj.GetComponent<JokerDisplay>();
        display.Setup(data);

        LayoutGroup lg = jokerArea.GetComponent<LayoutGroup>();
        if (lg != null) lg.enabled = false;

        ArrangeJokers();
    }

    public void ArrangeJokers()
    {
        int count = jokerArea.childCount;

        if (count == 0) return;

        float totalWidth = (count - 1) * jokerSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            RectTransform rt = jokerArea.GetChild(i).GetComponent<RectTransform>();

            if (rt == null) continue;

            float x = startX + (i * jokerSpacing);

            rt.anchoredPosition = new Vector2(x, 0);
            rt.localRotation = Quaternion.identity;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) GiveJoker(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) GiveJoker(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) GiveJoker(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) GiveJoker(3);
    }

    public int ApplyChipModifiers(int chips, CardData card)
    {
        foreach (var joker in ownedJokers)
        {
            chips = jokerSystem.ApplyChips(chips, card);
        }

        return chips;
    }

    public int ApplyMultModifiers(int mult, CardData card)
    {
        foreach (var joker in ownedJokers)
        {
            mult = jokerSystem.ApplyMult(mult, card);
        }

        return mult;
    }

    public double ApplyXMultModifiers(double xmult, CardData card)
    {
        foreach (var joker in ownedJokers)
        {
            xmult = jokerSystem.ApplyXMult(xmult, card);;
        }

        return xmult;
    }
}