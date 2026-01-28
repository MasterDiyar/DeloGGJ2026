shader_type canvas_item;

uniform sampler2D noise_texture : repeat_enable;
uniform float speed = 0.05;
uniform float zoom_speed = 0.02;

void fragment() {

    vec2 moving_uv = UV;
    moving_uv.x += TIME * speed;
    moving_uv.y += TIME * 0.01; 
    
    vec2 centered_uv = moving_uv - vec2(0.5);
    centered_uv *= sin(1.0 - TIME * zoom_speed); 
    moving_uv = centered_uv + vec2(0.5);

    float noise_val = texture(noise_texture, moving_uv).r;
    
    vec4 color = vec4(.3, .3, .3, 0.0); 

    if (noise_val > 0.7*((sin(TIME/10.0)/10.0)+1.0)) {
        color.a = 99.0 / 255.0; 
    } else if (noise_val > 0.5*((sin(TIME/10.0)/10.0)+1.0)) {
        color.a = 66.0 / 255.0;
    } else 
        color.a = 0.0; 
    

    COLOR = color;
}