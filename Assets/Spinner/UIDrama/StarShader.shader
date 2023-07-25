Shader "Unlit/StarShader"
{
  Properties {
    
      [Header(Shape Proportions)]
       
      [Space] 
      _MainTex ("Texture", 2D) = "white" {}
      _GlobalRotation ("GlobalRotation", Range(0,1)) = 0
      
      [Space(25)]
      _Tiling ("Tiling", Range(0.5, 50)) = 1
      _LocalRotation ("Local Rotation", Range(-.5,.5)) = 0
      [Space]
      _LocalScaleX ("Local ScaleX", Range(0, 2)) = 0
      _LocalScaleY ("Local ScaleY", Range(0, 2)) = 0
      [Space]
      _LocalOffsetX ("Local Offset X", Range(-2,2)) = 0
      _LocalOffsetY ("Local Offset Y", Range(-2,2)) = 0
      [Space]
      _RotationStep ("Pattern Rotation Matrix", Vector) = (2,1,1,1)
      
      [Header(Shape Form)]
      [Space]    _Size  ("Size", Range(0, 4)) = .7
      [IntRange] _NumVertices  ("Segments", Range(1, 180)) = 5
      
       
       [Space(25)]
       _BeamA ("Beam A", Range(0, 1)) = .5
       _BeamB ("Beam B", Range(0, 1)) = .5
       _BeamLength ("Beam Power", Range(0, 2)) = .2
       
       [Space(50)]
       _VertShift ("Shift Vertices", Range(-1, 1)) = 0
       _Roundness ("Roundness", Range(0, 1)) = 0
       
       [Header(Shape Colors)][Space]
       
       [Enum(Linear Horizontal, 0, Linear Vertical, 1, Radial (for stroked shapes), 2)] _GradientMode ("Gradient Mode", Float) = 0
       _Smoothness ("Smoothstep", Range(0.001, .77)) = 0
       _ColorStart ("Color Start", Range(0, 1)) = 0
       _ColorEnd ("Color End", Range(0, 1)) = 0
       
       
       
       [Space(25)]
        
        [Enum(Straight, 0, CustomOrdered, 1, Chess, 2, Monochrome, 3)] _Order("Color Ordering", Float) = 0
        [IntRange] _NumColors ("Number of colors", Range(2, 8)) = 2
        _ColorStep  ("ColorStep", Range(0, 1)) = 0
        
        [Space(25)]
        _Background ("Background", Color) = (0,0,0,1)
        [Space]
        _Color2 ("Color 2", Color) = (1,1,1,1)
        _Color3 ("Color 3", Color) = (1,0,1,1)
        _Color4 ("Color 4", Color) = (0,0,1,1)
        _Color5 ("Color 5", Color) = (0,1,1,1)
        _Color6 ("Color 6", Color) = (1,1,0,1)
        _Color7 ("Color 7", Color) = (0,1,0,1)
        _Color8 ("Color 8", Color) = (0,1,0,1)
        
        [Header(Lucky Strike Mode)][Space]
        
        _Tiling2    ("Number of strokes", Range(0, 20)) = 1
        _InnerRadius ("Inner Radius", Range(0, 2)) = 0
        _ColorShift ("Color Shift", Range(0, 10)) = 10
        _ExplosionSpeed ("Explosion Speed", Range(0,1)) = 0
        
        
        [Header(Distortion)][Space(25)]
        
        
        _WarpScale1 ("Warp Scale 1", Range(0, 1)) = 0
        _WarpTiling1 ("Warp Tiling 1", Range(1, 50)) = 1
        
        _WarpScale2 ("Warp Scale2", Range(0, 1)) = 0
        _WarpTiling2 ("Warp Tiling2", Range(1, 50)) = 1
        
        _VertexDisplacementPower ("Vertex Displacement Power", Range(0, 1)) = 0
        _VertexDisplacementSpeed ("Vertex Displacement Speed", Range(1, 50)) = 0
        
         
       
        
    }

    SubShader
    {
    
    
            Tags {
            "RenderType"="Opaque"
            "Queue" = "Transparent"
            }
        
             ZWrite Off
                // Lighting Off
                // Fog { Mode Off }

                  Blend SrcAlpha OneMinusSrcAlpha 
             //   Blend One One         // additive
             //   Blend DstColor Zero   // multiply

    
        Pass
        {
            Tags { "LightMode" = "ForwardBase" }
            Cull Off
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
          
            #include "UnityCG.cginc"
            #define PI 3.14159
            
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            int   _NumVertices, _NumColors;
            fixed4 _RotationStep;
            
            float _Tiling, _Tiling2, _ColorShift, _InnerRadius, _ColorStart, _ColorEnd, _Size, _SmoothStart, _SmoothEnd, _ColorStep;
            float _BeamA, _BeamB, _BeamLength;
            float _VertShift;
            float _LocalRotation, _GlobalRotation, _LocalOffsetX, _LocalOffsetY, _LocalScaleX, _LocalScaleY;
            
            float _Smoothness, _Roundness;
            fixed4 _Background, _Color2, _Color3, _Color4, _Color5, _Color6, _Color7, _Color8;
            float  _Order, _GradientMode;
            
            float _WarpScale1, _WarpTiling1, _WarpTiling2, _WarpScale2, _ExplosionSpeed, _VertexDisplacementPower, _VertexDisplacementSpeed;
     
     
     
            float2 localOffset;
            float startVerticesCount;
            float numVertices;
            
            
            
            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            
            
              
            float2 rotatePoint(float2 pt, float2 center, float angle) {
                float sinAngle = sin(angle);
                float cosAngle = cos(angle);
                pt -= center;
                float2 r = float2( pt.x * cosAngle - pt.y * sinAngle,
                                   pt.x * sinAngle + pt.y * cosAngle);
                r += center;
                return r;
            }
            
            
            float InverseLerp(float a, float b, float v){
                return (v-a)/(b-a);
            } 
            
            
           fixed4 GradientColor(fixed4 colorA, fixed4 colorB, float uv){
            
                float t = smoothstep(_ColorStart,_ColorEnd, uv);
                return lerp(colorA, colorB, t); 
            
            }
            
           
           fixed4 background(float2 uv){
           
            float color = 0;
            color = uv * 2;
            color += frac(uv);
            
            int2 value = floor((frac(uv.y)) * 2);
            value = clamp(value, 0, 2);
           
            switch (value.y) {
                
                default: return _Background;
                case 0:  return  _Background;
                case 1:  return  _Color2; 
                case 2:  return  _Color3; 
                case 3:  return  _Color4;  
                case 4:  return  _Color5;  
                case 5:  return  _Color6;
                case 6:  return  _Color7;
        }}
            
            
            
            
            
           fixed4 getColor(int index, int numColors){
           
               if(index == numColors)  return _Color2; 
               else     
               switch(index - 2) {
                        
                        default: return _Color2;
                        
                        case 0:  return _Color3;
                        case 1:  return _Color4;
                        case 2:  return _Color5;
                        case 3:  return _Color6;
                        case 4:  return _Color7;
                        case 5:  return _Color8;
                        case 6:  return _Color2;
                    }
                } 
            
       
 
           
           fixed4 setColor(float2 pos, float2 uv){
                
                    float color = 0;
                    int numColors = _NumColors;
                    float colorStep = floor((numColors-1) * _ColorStep) + 1;  
                    

                    if (_Order == 0) { color = uv * 2; color += floor(pos.y * 2); colorStep = 1; } 
                    else { color = pos * 2; }
                 
                    if(_Order == 1)  color += (floor(uv.y * 2) * colorStep) + (floor(uv.x * 2) * colorStep * 2); 
                    if(_Order == 2)  color += ((floor(uv.y * 2) + floor(uv.x * 2)) * colorStep);
                    if(_Order == 3)  color += floor(uv.y / _Tiling);
                    
                    color = fmod(color, numColors-1);
                    // radial = 2 // vertical = 1 // horizontal = 0 
                    float uvDirection = _GradientMode == 2 ? pos.x * 2  : _GradientMode == 1 ? uv.y * 2 : uv.x * 2;
                    float index = color + 2;
                    
                    
                    float modIndex = _Order == 1 ? index + 2 * (numColors-1) : index + numColors-1;
                    float modStep =  _Order == 1 ? colorStep * 2 : colorStep; 
                    if(_Order == 1 && _GradientMode == 1) modStep = modStep - 1 * colorStep;
                    
                    fixed4 from = getColor(fmod(modIndex - modStep - 2, numColors-1) + 2, numColors); 
                    fixed4 to =   getColor(index, numColors);
                    
                   
                
                    return  GradientColor(from, to, frac(uvDirection));
              
                }
                 
            
            
            
            float2 applyDistortion(float2 uvCenter, float2 uvResolution, float2 localOffset){
              
                uvCenter.x += sin(uvCenter.y        * _WarpTiling1 * PI * 2) * _WarpScale1;
                uvCenter.y += cos(uvCenter.x        * _WarpTiling1 * PI * 2) * _WarpScale1;
                uvCenter.x += cos(uvResolution.y    * _WarpTiling2 - 1  * PI * 2 + _Time.y) * _WarpScale2;
                uvCenter.y += sin(uvResolution.x    * _WarpTiling2 - 1  * PI * 2 + _Time.y) * _WarpScale2;
                
                return uvCenter;
            }
            
            float2 applyPatternRotationMatrix(float2 st, float2 res){
                
                float index = 1.0;
                index += step(1., fmod(res.x * 2,2.0));
                index += step(1., fmod(res.y * 2,2.0))*2.0;
                
                startVerticesCount = float(_NumVertices);
                numVertices = startVerticesCount;
                
                       if(index == 1.0){
                       st = rotatePoint(st, localOffset, PI * 0.5 * floor(_RotationStep.x));
                       numVertices = startVerticesCount;
                       } else if(index == 2.0){
                       st = rotatePoint(st, localOffset, PI * -0.5 * floor(_RotationStep.y));
                       numVertices = startVerticesCount * _RotationStep.w; 
                       } else if(index == 3.0){
                       st = rotatePoint(st, localOffset, PI * 0.5 * floor(_RotationStep.z)); 
                       numVertices = startVerticesCount * _RotationStep.w; 
                       }
                       
                 return st;      
            }
                        
            float flower(float angle, float2 uv, float numVertices){
                
                float segment = angle * numVertices;
                float border = length(uv);
                float shape = sin((segment - PI * .5) + border * _VertShift * 300);
                border +=  _BeamLength * shape;
                
                return border + (1 - _Size);
            
            }
            
            float star(float angle, float2 uv, float numVertices) { 
                
                angle /= (PI * 2);
                float segment = angle * numVertices;
                angle = ((floor(segment) + 0.5 + _VertShift) / numVertices + lerp(_BeamA * _BeamLength, - _BeamB * _BeamLength, step(0.5 + _VertShift, frac(segment)))) * (PI * 2);
             
                return abs(dot(float2(cos(angle), sin(angle)), uv)) + (1 -_Size);
            
            }
            
 
              
            float4 draw(float2 uv) {
                
                uv = rotatePoint(uv, float2(0.5,0.5), _GlobalRotation * 2 * PI) * _MainTex_ST.xy + _MainTex_ST.zw;
                localOffset = float2(_LocalOffsetX, _LocalOffsetY);
                
                float2 uvResolution = uv * _Tiling;
                float2 uvCenter = frac(uvResolution * 2 - 1) - .5 + localOffset;
              
                uvCenter = rotatePoint(uvCenter, localOffset, _LocalRotation * 2 * PI) * (1 + float2(_LocalScaleX, _LocalScaleY));
                uvCenter = applyDistortion(uvCenter, uvResolution, localOffset);  
                uvCenter = applyPatternRotationMatrix(uvCenter, uvResolution);
              
                float rounded = 0;
                float sharped = 0;
                float angle = atan2(uvCenter.y, uvCenter.x);
                
                if(_Roundness >= 0.02) rounded = flower(angle, uvCenter, numVertices);
                if(_Roundness <= 0.98) sharped = star  (angle, uvCenter, numVertices);
                
                float  shape = lerp(sharped, rounded, _Roundness);
                float2 shapeBorder = length((shape + _ColorShift - (_Time.y * _ExplosionSpeed) + _InnerRadius) * _Tiling2 ); // stroke
                
                int2 value = floor((frac(shapeBorder)) * 2) + (shapeBorder.x * shapeBorder.y);
                value = clamp(value, 1, 2);
                
                
                fixed4 shapeColor = setColor(shapeBorder, uvResolution); 
                fixed4 backgroundColor = _Background; //background(uvResolution);
                
                fixed4 color = lerp(shapeColor, backgroundColor,  1.0 - smoothstep(.45, .45 - _Smoothness, shape));
             
                return color;
            
            }
            
             
            
            Interpolators vert (MeshData v)
            {
                Interpolators o;
                
                v.vertex.y = draw(v.uv).rgb * _VertexDisplacementPower * 10;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
               
                return o;
            }
            
            
       
            
            fixed4 frag (Interpolators i) : SV_Target
            {
               fixed4 procedural = draw(i.uv); 
               return procedural * tex2D(_MainTex, i.uv);
                //color.rgb = noiseTexture;
                
               ;
            }
            
            ENDCG
        }
    }
}


