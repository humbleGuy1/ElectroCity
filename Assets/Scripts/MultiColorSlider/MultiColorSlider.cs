using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiColorSlider : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _sprite;

    private Sprite _tempSprite;
    private Color32[] _colors;

    public void CreateBlank()
    {
        Texture2D texture = new Texture2D((int)_sprite.rect.width, (int)_sprite.rect.height);
        _tempSprite = Sprite.Create(texture, _sprite.rect, _sprite.pivot);

        _colors = _sprite.texture.GetPixels32();
        _tempSprite.texture.SetPixels32(_colors, 0);
        _image.sprite = _tempSprite;
    }

    public void Colorize(Team[] teams)
    {
        int lastStop = 0;
        int totalPercent = 0;

        foreach (var team in teams)
        {
            totalPercent += team.Percent;
            int teamPercent = team.Percent;

            if (totalPercent > 100)
                teamPercent -= totalPercent - 100;

            ColorRect(team.Color, _colors, teamPercent, GetXPixelIndex(lastStop));
            lastStop += team.Percent;
        }
    }

    private void ColorRect(Color color, Color32[] colors, int percent, int startPoint)
    {
        int length = GetXPixelIndex(percent) + startPoint;
        int index;

        for (int j = startPoint; j < length; j++)
        {
            for (int i = 0; i < colors.Length; i += (int)_image.sprite.rect.width)
            {
                index = i + j;

                colors[index] = color;
            }
        }

        _image.sprite.texture.SetPixels32(0, 0, (int)_image.sprite.rect.width, (int)_image.sprite.rect.height, colors);

        _image.sprite.texture.Apply();
    }

    private int GetXPixelIndex(int percent)
    {
        return ((int)_image.sprite.rect.width * percent) / 100;
    }
}