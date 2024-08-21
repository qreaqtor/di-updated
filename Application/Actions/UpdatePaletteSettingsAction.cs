﻿using System.Net;
using System.Text.Json;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.Application.Actions;

public class UpdatePaletteSettingsAction : IApiAction, INeed<Palette>
{
    private Palette palette = null!;

    public void SetDependency(Palette dependency)
    {
        palette = dependency;
    }

    public string Endpoint => "/palette";

    public string HttpMethod => "PUT";

    public int Perform(Stream inputStream, Stream outputStream)
    {
        var updatedPalette = JsonSerializer.Deserialize<Palette>(inputStream);
        palette.BackgroundColor = updatedPalette?.BackgroundColor ?? palette.BackgroundColor;
        palette.PrimaryColor = updatedPalette?.PrimaryColor ?? palette.PrimaryColor;
        palette.SecondaryColor = updatedPalette?.SecondaryColor ?? palette.SecondaryColor;
        JsonSerializer.Serialize(outputStream, palette);

        return (int)HttpStatusCode.OK;
    }
}