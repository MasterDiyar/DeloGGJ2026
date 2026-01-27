shader_type canvas_item;

// Ссылка на то, что уже нарисовано на экране под объектом
uniform sampler2D screen_texture : hint_screen_texture;

void fragment() {
    vec4 cloud_color = texture(TEXTURE, UV);
    
    vec4 screen_all = texture(screen_texture, SCREEN_UV);
    
    if (screen_all.a > 0.0) {
        discard;
    }
    
    COLOR = cloud_color;
}