Shader "Portal/Stencil"
{
    Properties
    {
        [IntRange] _StencilID("Stencil ID", Range(0, 255)) = 0
        [KeywordEnum(None, Never, Less, Equal, LEqual, Greater, NotEqual, GEqual, Always)] _Comp("Computation", Float) = 0
        [KeywordEnum(Keep, Zero, Replace, IncrSat, DecrSat, Invert, IncrWrap, Decrwrap)] _Pass("Pass", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Geometry"
            "RenderPipeline" = "UniversalPipeline"
        }
        
        LOD 100
        
        Pass
        {
            
            Blend Zero One
            ZWrite Off

            Stencil
            {
                Ref[_StencilID]
                Comp [_Comp]
                Pass [_Pass]
                Fail Keep
            }
        }
    }
}
