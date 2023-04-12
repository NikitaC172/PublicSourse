using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CustomizatorSystem : MonoBehaviour
{
    [SerializeField] private List<Material> _helmets;
    [SerializeField] private List<Material> _costumes;
    [SerializeField] private List<Material> _boots;
    [SerializeField] private List<Material> _bikes;

    public Action<Material> HelmetMaterialChanged;
    public Action<Material> CostumeMaterialChanged;
    public Action<Material> BootMaterialChanged;
    public Action<Material> BikeMaterialChanged;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    public void ChangeHelmetMaterial(int numberMaterial)
    {
        HelmetMaterialChanged?.Invoke(_helmets[numberMaterial]);
    }

    public void ChangeCostumeMaterial(int numberMaterial)
    {
        CostumeMaterialChanged?.Invoke(_costumes[numberMaterial]);
    }

    public void ChangeBootMaterial(int numberMaterial)
    {
        BootMaterialChanged?.Invoke(_boots[numberMaterial]);
    }

    public void ChangeBikeMaterial(int numberMaterial)
    {
        BikeMaterialChanged?.Invoke(_bikes[numberMaterial]);
    }

    private void GetLoad()
    {
        ChangeBootMaterial(YandexGame.savesData.Boot);
        ChangeCostumeMaterial(YandexGame.savesData.Costume);
        ChangeHelmetMaterial(YandexGame.savesData.Helmet);
        ChangeBikeMaterial(YandexGame.savesData.Bike);
    }
}
