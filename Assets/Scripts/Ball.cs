using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] MeshRenderer _sphere;
    [SerializeField] SpriteRenderer _selector;

    public Color color {
        get => _sphere.materials[0].GetColor("_Color");
        set => _sphere.materials[0].SetColor("_Color", value);
    }

    public Node node { get; set; }


    public void GetPosition(out int x, out int y)
    {
        x = node?.x ?? -1;
        y = node?.y ?? -1;
    }

    public void Select()
    {
        _selector.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _selector.gameObject.SetActive(false);
    }

}
