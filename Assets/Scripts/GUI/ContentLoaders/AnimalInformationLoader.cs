using UnityEngine;
using UnityEngine.UI;

public class AnimalInformationLoader : MonoBehaviour
{
    public RawImage AnimalImage;
    public Text AnimalNameTitle;
    public Text AnimalDescription;

    public Text HealthFactor1;
    public Text HealthFactor2;
    public Text HealthFactor3;

    void Awake()
    {
    }

    /// <summary>
    /// Fills in fields for animal information, relying on GameStateManager.
    /// </summary>
    void SetTextFields()
    {
        Animal animal = GameStateManager.CurrentAnimal;
        // Populate fields
        AnimalImage.texture = Resources.Load<Texture>(animal.Species.ToString());
        AnimalNameTitle.text = Service.Request.AnimalName(animal.Species);
        AnimalDescription.text = Service.Request.AnimalDescription(animal.Species);
        // Populate health factors
        HealthFactor1.text = animal.Stats.Health1.ToString();
        HealthFactor2.text = animal.Stats.Health2.ToString();
        HealthFactor3.text = animal.Stats.Health3.ToString();
    }

    void Destroy()
    {
    }
}
