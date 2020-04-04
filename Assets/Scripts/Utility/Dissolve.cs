using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public SpriteRenderer sprite;
    Material material;
    bool isDissolving = false;
    float fade = 1f;
    void Start()
    {
        material = sprite.material;
    }

    void Update()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;
            if(fade <= 0f)
            {
                fade = 0f;
                gameObject.SetActive(false);
            }
            material.SetFloat("_Fade", fade);
        }
    }

    public void SetDissolving(bool state)
    {
        isDissolving = state;
    }
}
