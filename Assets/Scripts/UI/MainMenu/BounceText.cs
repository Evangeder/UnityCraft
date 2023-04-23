using UnityEngine;
using UnityEngine.UI;

public class BounceText : MonoBehaviour
{
    private Text splashText;
    private float currentScale = 0f;

    private string[] splashes = new string[]
    {
        "Pre-beta!",
        "As seen on TV!",
        "Awesome!",
        "100% pure!",
        "May contain nuts!",
        "Better than Prey!",
        "More polygons!",
        "Sexy!",
        "Limited edition!",
        "Flashing letters!",
        "Made by Notch!",
        "Coming soon!",
        "Best in class!",
        "When it's finished!",
        "Absolutely dragon free!",
        "Excitement!",
        "More than 5000 sold!",
        "One of a kind!",
        "700+ hits on YouTube!",
        "Indev!",
        "Spiders everywhere!",
        "Check it out!",
        "Holy cow, man!",
        "It's a game!",
        "Made in Sweden!",
        "Uses LWJGL!",
        "Reticulating splines!",
        "Minecraft!",
        "Yaaay!",
        "Alpha version!",
        "Singleplayer!",
        "Keyboard compatible!",
        "Undocumented!",
        "Ingots!",
        "Exploding creepers!",
        "That's not a moon!",
        "l33t!",
        "Create!",
        "Survive!",
        "Dungeon!",
        "Exclusive!",
        "The bee's knees!",
        "Down with O.P.P.!",
        "Closed source!",
        "Classy!",
        "Wow!",
        "Not on steam!",
        "9.95 euro!",
        "Half price!",
        "Oh man!",
        "Check it out!",
        "Awesome community!",
        "Pixels!",
        "Teetsuuuuoooo!",
        "Kaaneeeedaaaa!",
        "Now with difficulty!",
        "Enhanced!",
        "90% bug free!",
        "Pretty!",
        "12 herbs and spices!",
        "Fat free!",
        "Absolutely no memes!",
        "Free dental!",
        "Ask your doctor!",
        "Minors welcome!",
        "Cloud computing!",
        "Legal in Finland!",
        "Hard to label!",
        "Technically good!",
        "Bringing home the bacon!",
        "Indie!",
        "GOTY!",
        "Ceci n'est pas une title screen!",
        "Euclidian!",
        "Now in 3D!",
        "Inspirational!",
        "Herregud!",
        "Complex cellular automata!",
        "Yes, sir!",
        "Played by cowboys!",
        "OpenGL 1.1!",
        "Thousands of colors!",
        "Try it!",
        "Age of Wonders is better!",
        "Try the mushroom stew!",
        "Sensational!",
        "Hot tamale, hot hot tamale!",
        "Play him off, keyboard cat!",
        "Guaranteed!",
        "Macroscopic!",
        "Bring it on!",
        "Random splash!",
        "Call your mother!",
        "Monster infighting!",
        "Loved by millions!",
        "Ultimate edition!",
        "Freaky!",
        "You've got a brand new key!",
        "Water proof!",
        "Uninflammable!",
        "Whoa, dude!",
        "All inclusive!",
        "Tell your friends!",
        "NP is not in P!",
        "Notch <3 Ez!",
        "Music by C418!",
        "Ported to Unity by Evangeder"
    };

    private void Awake() => splashText = GetComponent<Text>();
    void Start() => splashText.text = splashes[Random.Range(0, splashes.Length - 1)];

    [SerializeField]
    private float bounceSpeed = 6f;
    [SerializeField]
    private float textScale = 2f;

    void Update()
    {
        var scale = Mathf.Abs(Mathf.Sin(currentScale += Time.deltaTime * bounceSpeed)) / 15f + textScale;
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }
}
