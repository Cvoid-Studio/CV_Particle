using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// using OpenGL ES 2.0

public class TestMain : MonoBehaviour {
    public Material Mat_Target;
    private Shader glsl_Script;
    

    // Mesh -> Material : pointer
    float start_T;

    private int Precise_map = 0;
    private float Gap = 0F;
    bool T;
    void Start () {
        Precise_map = Screen.width / 6;

        // Bind Shader
        //glsl_Script = Shader.Find("Elec_Wave");

        //Mat_Target = new Material(glsl_Script);
        //Mat_Target = new Material(glsl_Script);
        //Mat_Target.shader = Resources.Load<Shader>("Elec_Wave");
        //Mat_Target.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
        //Mat_Target.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        Mat_Target.SetFloat("Win_width", (float)Screen.width);
        Mat_Target.SetFloat("Win_height", (float)Screen.height);

        start_T = Time.time;

        Sin_Array = new float[Precise_map+1];
        Elec_Array = new float[Precise_map + 1];

        Gap = 1F / (float)(Precise_map);

        Debug.Log("HHEUIHFosdfoierngi!!!");
    }

    

    // Update is called once per frame
    private float Amp = 0.15F;
    private float[] Sin_Array;
    private float[] Elec_Array;
    void Update () {
        float Now = Time.time - start_T;
        float Center = 0.5F;

        for (int i = 0; i < Precise_map; ++i)
        {
            float X = i * Gap;
            Sin_Array[i] = (1F - 1.25f*Mathf.Abs((X - Center)))
                * (Amp * Mathf.Sin((X - Center) *40.0f + Now * 20.0f))
                + 0.5f;
            Elec_Array[i] = (1F - 1.25f * Mathf.Abs((X - Center)))
                *(Amp/2.0f * Mathf.Sin((X - Center) * 20.0f + Now * 40.0f))
                + 0.5f;
            Elec_Array[i] *= Random.Range(0.90f, 1.1f);
        }

        Sin_Array[Precise_map] = Sin_Array[Precise_map-1];
        Elec_Array[Precise_map] = Elec_Array[Precise_map - 1];

    }

    void OnGUI()
    {
        GL.Clear(true, true, Color.black);
        float X = 0F;

        GL.PushMatrix();
        GL.LoadOrtho();
        X = 0F;
        for (int i = 0; i < Precise_map/2; ++i)
        {

            GL.Begin(GL.QUADS);
            Mat_Target.SetVector("Elec_Frag",
                new Vector4(X, Elec_Array[i], X + Gap, Elec_Array[i + 1]));
            Mat_Target.SetPass(0);
            GL.Color(new Color(0F, 1.0F, 0.3F, 0.5f));
            GL.Vertex3(X + Gap, 1F, 1F);
            GL.Color(new Color(0F, 1.0F, 0.3F, 0.5f));
            GL.Vertex3(X + Gap, 0F, 1F);
            GL.Color(new Color(0F, 1.0F, 0.3F, 1.0F));
            GL.Vertex3(X, 0F, 1F);
            GL.Color(new Color(0F, 1.0F, 0.3F, 1.0F));
            GL.Vertex3(X, 1F, 1F);

            GL.End();
            X += Gap;
        }
        GL.PopMatrix();



        GL.PushMatrix();
        GL.LoadOrtho();

        X = 0F;
        for (int i = 0; i < Precise_map / 2; ++i)
        {
            
            GL.Begin(GL.QUADS);
            Mat_Target.SetVector("Elec_Frag",
                new Vector4(X, Sin_Array[i], X + Gap, Sin_Array[i+1]));
            Mat_Target.SetPass(0);
            GL.Color(new Color(0.2F, 0.9F, 1.0F, 1F));
            GL.Vertex3(X + Gap, 1F, 1F);
            GL.Color(new Color(0.4F, 0.2F, 1.0F, 1F));
            GL.Vertex3(X + Gap, 0F, 1F);
            GL.Color(new Color(0.4F, 0.2F, 1.0F, 1F));
            GL.Vertex3(X, 0F, 1F);
            GL.Color(new Color(0.2F, 0.9F, 1.0F, 1F));
            GL.Vertex3(X, 1F, 1F);

            GL.End();
            X += Gap;
        }
        GL.PopMatrix();

        GL.PushMatrix();
        GL.LoadOrtho();
        X = 0F;
        for (int i = 0; i < Precise_map / 2; ++i)
        {

            GL.Begin(GL.QUADS);
            Mat_Target.SetVector("Elec_Frag",
                new Vector4(X, 1F - Sin_Array[i], X + Gap, 1F - Sin_Array[i + 1]));
            Mat_Target.SetPass(0);
            GL.Color(new Color(1.0F, 0.9F, 0.2F, 1F));
            GL.Vertex3(X + Gap, 1F, 1F);
            GL.Color(new Color(1.0F, 0.2F, 0.2F, 1F));
            GL.Vertex3(X + Gap, 0F, 1F);
            GL.Color(new Color(1.0F, 0.2F, 0.2F, 1F));
            GL.Vertex3(X, 0F, 1F);
            GL.Color(new Color(1.0F, 0.9F, 0.2F, 1F));
            GL.Vertex3(X, 1F, 1F);

            GL.End();
            X += Gap;
        }
        GL.PopMatrix();


       
        
    }
}
