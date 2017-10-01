Shader "Elec_Wave" {
    SubShader{
        Pass{
        

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
		GLSLPROGRAM

        uniform float Win_width;
        uniform float Win_height;
        #ifdef VERTEX
        out vec4 View_Port;
        out vec4 Ver_Color;

        void main()
        {
            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
            Ver_Color = gl_Color;
        }

        #endif


        #ifdef FRAGMENT
        in vec4 View_Port;
        in vec4 Ver_Color;

        uniform vec4 Elec_Frag;
        void main()
        {
            gl_FragColor = Ver_Color;

            // Linear Interpolation
            float X = (gl_FragCoord.x - 0.5) / Win_width;
            float Y = (Elec_Frag[3] - Elec_Frag[1])*(X - Elec_Frag[0]) / (Elec_Frag[2] - Elec_Frag[0]);
            Y += Elec_Frag[1];
            float Diff = 1.0 - abs(((gl_FragCoord.y - 0.5) / Win_height) - Y);
            gl_FragColor.a *= smoothstep(0.95 + (1.0 - Diff) / 20.0, 1.0, Diff) * Diff * 0.3;
            gl_FragColor.a += smoothstep(0.992, 1.0, Diff) * Diff;
        }

        #endif

        ENDGLSL
        }
    }
}