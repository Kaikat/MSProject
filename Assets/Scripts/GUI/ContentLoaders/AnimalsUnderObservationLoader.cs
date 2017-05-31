using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsUnderObservationLoader : MonoBehaviour {

    public GameObject ContentPanel;
    private AnimalSpecies animalSpecies;
    private const string ANIMAL_UNDER_OBS_PREFAB = "AnimalUnderObs";

    void Awake()
    {
        EventManager.RegisterEvent<AnimalSpecies>(GameEvent.ViewingAnimalsUnderObservation, SetAnimalSpecies);
    }

    void Destroy()
    {
        EventManager.UnregisterEvent<AnimalSpecies>(GameEvent.ViewingAnimalsUnderObservation, SetAnimalSpecies);
    }

    private void SetAnimalSpecies(AnimalSpecies species)
    {
        animalSpecies = species;
    }

	public void OnShow()
    {
        List<Animal> animals = Service.Request.Player().GetAnimals()[animalSpecies];
        foreach (Animal animal in animals)
        {
            GameObject newPrefab = Instantiate(Resources.Load(ANIMAL_UNDER_OBS_PREFAB)) as GameObject;
            newPrefab.transform.parent = ContentPanel.transform;
            
        }
    }
}
