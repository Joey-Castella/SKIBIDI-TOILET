
using TMPro;
using UnityEngine;


public class VaccineManager : MonoBehaviour
{
    public static VaccineManager instance;

    public int vaccineCount = 0;
    public int totalVaccine = 5;

    public TextMeshProUGUI vaccineText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddVaccine()
    {
        vaccineCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        vaccineText.text = vaccineCount + " / " + totalVaccine;
    }
}  

