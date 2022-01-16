using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VertexWobble : MonoBehaviour
{

    public TMP_Text text;
    public GameObject txt;

    private Mesh mesh;
    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        txt.SetActive(true);
        text.text = "Usa la Fiamma per passare oltre";
    }

    // Update is called once per frame
    void Update()
    {
        text.ForceMeshUpdate();
        mesh = text.mesh;
        vertices=mesh.vertices;

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time+i);
            vertices[i] += offset;
        }
        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time*3.3f),Mathf.Cos(time*2.5f));
    }
}
