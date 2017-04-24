<<<<<<< HEAD
Shader "Custom/ColoredLines" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    
    SubShader {
        Pass { 
            Lighting Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha
            Color [_Color]
        }
    } 
}
=======
Shader "Custom/ColoredLines" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    
    SubShader {
        Pass { 
            Lighting Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha
            Color [_Color]
        }
    } 
}
>>>>>>> 2f58bfc643da8d811e07b94e1a353d25d22e7cd1
