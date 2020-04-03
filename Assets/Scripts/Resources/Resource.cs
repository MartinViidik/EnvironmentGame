using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceType type;
    public int amount;

    private int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    private ResourceType Type
    {
        get {return type;}
        set { type = value; }
    }

    public Resource(ResourceType _type)
    {
        type = _type;
    }

}


