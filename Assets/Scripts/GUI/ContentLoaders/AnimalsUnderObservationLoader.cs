using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsUnderObservationLoader : MonoBehaviour, IShowHideListener {

    public GameObject ContentPanel;
    public GameObject UIObjectScreen;
    private AnimalSpecies animalSpecies;
    private List<GameObject> animalPrefabs = new List<GameObject>();
    private const string ANIMAL_UNDER_OBS_PREFAB = "AnimalUnderObs.prefab";

    void Awake()
    {
        EventManager.RegisterEvent<AnimalSpecies>(GameEvent.ViewingAnimalsUnderObservation, SetAnimalSpecies);
        UIObjectScreen.GetComponent<TaggedShowHide>().listener = this;
    }

    void Destroy()
    {
        EventManager.UnregisterEvent<AnimalSpecies>(GameEvent.ViewingAnimalsUnderObservation, SetAnimalSpecies);
    }

    private void SetAnimalSpecies(AnimalSpecies species)
    {
        animalSpecies = species;
    }

    void IShowHideListener.OnShow()
    {
        try
        {
            Dictionary<AnimalSpecies, List<Animal>> allAnimals = Service.Request.Player().GetAnimals();
            List<Animal> animals = Service.Request.Player().GetAnimals()[animalSpecies];
            foreach (Animal animal in animals)
            {
                GameObject newPrefab = Instantiate(Resources.Load(ANIMAL_UNDER_OBS_PREFAB)) as GameObject;
                newPrefab.transform.parent = ContentPanel.transform;
                animalPrefabs.Add(newPrefab);
            }
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogWarning("ANIMALSUNDEROBS: " + animalSpecies.ToString() + " not found: " + e.ToString());
        }
        
    }

    void IShowHideListener.OnHide()
    {
        foreach (GameObject animalPrefab in animalPrefabs)
        {
            GameObject.Destroy(animalPrefab.gameObject);
        }
        animalPrefabs.Clear();
    }
}
