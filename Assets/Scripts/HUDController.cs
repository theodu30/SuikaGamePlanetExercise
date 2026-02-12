using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private UIDocument uiDocument;

    private VisualElement root;

    private Label scoreLabel;
    private VisualElement nextFruitObject;

    private Label lostLabel;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        scoreLabel = root.Q<Label>("ScoreLabel");
        nextFruitObject = root.Q<VisualElement>("NextFruitObject");
        lostLabel = root.Q<Label>("LostLabel");
        lostLabel.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        GameEvents.OnScoreChange += UpdateScore;
        GameEvents.OnNextTypeChange += UpdateNextFruit;
        GameEvents.OnGameLost += UpdateGameLost;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreChange -= UpdateScore;
        GameEvents.OnNextTypeChange -= UpdateNextFruit;
        GameEvents.OnGameLost -= UpdateGameLost;
    }

    private void UpdateScore(object sender, int score)
    {
        scoreLabel.text = "Score: " + score.ToString();
    }

    private void UpdateNextFruit(object sender, Fruit.FruitType nextType)
    {
        Color fruitColor = Utils.ColorFromFruitType(nextType);
        nextFruitObject.style.unityBackgroundImageTintColor = new StyleColor(fruitColor);
    }

    private void UpdateGameLost(object sender, System.EventArgs args)
    {
        lostLabel.style.display = DisplayStyle.Flex;
    }

}
