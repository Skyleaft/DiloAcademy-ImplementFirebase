using UnityEngine;
using UnityEngine.UI;


public class TapText : MonoBehaviour
{
    public float SpawnTime = 0.7f;
    public Text Text;
    public GameObject TextObj;

    private float _spawnTime;


    private void OnEnable()
    {
        _spawnTime = SpawnTime;
    }


    private void Update()
    {
        _spawnTime -= Time.unscaledDeltaTime;

        if (_spawnTime <= 0f)
        {
            gameObject.SetActive(false);

        }
        else
        {
            Text.CrossFadeAlpha(0f, 0.7f, false);
            //tambahan agar objek yang dispawn bergerak keatas
            TextObj.transform.position = Vector2.LerpUnclamped(TextObj.transform.position, TextObj.transform.position + Vector3.up, 0.5f);
            if (Text.color.a == 0f)
            {
                gameObject.SetActive(false);
            }
        }

    }

}