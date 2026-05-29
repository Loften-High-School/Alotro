using System.Collections.Generic;
using NUnit.Framework.Internal;
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
    public List<GameObject> jokerObjects = new List<GameObject>();

    public float jokerSpacing = 150f;

    public bool test = false;
    
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

        SpawnJoker(data);
    }

    public void SpawnJoker(JokerData data) // Spawns UI object
    {
        GameObject obj = Instantiate(jokerPrefab, jokerArea, false);
        ownedJokers.Add(data);
        jokerObjects.Add(obj);
        PGI.jokers ++;

        JokerDisplay display = obj.GetComponent<JokerDisplay>();
        display.Setup(data);

        LayoutGroup lg = jokerArea.GetComponent<LayoutGroup>();
        if (lg != null) lg.enabled = false;

        ArrangeJokers();
    }

    public void RemoveJoker(int index)
    {
        if (index < 0 || index >= jokerObjects.Count) return;

        Destroy(jokerObjects[index]);

        jokerObjects.RemoveAt(index);
        ownedJokers.RemoveAt(index);
        PGI.jokers --;
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

        if (test == true) 
        {
            for (int i = ownedJokers.Count; i >= 0; i--) 
            {    
                RemoveJoker(i);
            }
            test = false;
        }
        
    }

    public float ApplyChipsOnScoredJokers(float chips, CardData card)
    {
        foreach (var joker in ownedJokers)
        {
            if (joker.activation != Activation.OnScored)
            continue;

            // IMPORTANT: check card condition
            if (!jokerSystem.DoesCardMatch(joker, card))
                continue;

            switch (joker.type)
            {
                case JokerType.AddChips:
                    chips += joker.value;
                    break;

                case JokerType.AddMult:
                    // handle later
                    break;
            }
        }

        return chips;
    }

    public int ApplyMultModifiers(int mult, CardData card)
    {
        foreach (var joker in ownedJokers)
        {
            //mult = jokerSystem.ApplyMult(mult, card);
        }

        return mult;
    }

    public double ApplyXMultModifiers(double xmult, CardData card)
    {
        foreach (var joker in ownedJokers)
        {
            //xmult = jokerSystem.ApplyXMult(xmult, card);;
        }

        return xmult;
    }
}