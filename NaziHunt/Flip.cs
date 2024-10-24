﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NaziHunt;

public class Flip
{
    public static Texture2D FlipImage(Texture2D source, bool vertical, bool horizontal)
    {
        Texture2D flipped = new(source.GraphicsDevice, source.Width, source.Height);
        Color[] data = new Color[source.Width * source.Height];
        Color[] flippedData = new Color[data.Length];

        source.GetData(data);
        for (int x = 0; x < source.Width; x++)
        {
            for (int y = 0; y < source.Height; y++)
            {
                int idx = (horizontal ? source.Width - 1 - x : x) + ((vertical ? source.Height - 1 - y : y) * source.Width);
                flippedData[x + y * source.Width] = data[idx];
            }
        }

        flipped.SetData(flippedData);
        return flipped;
    }
}
