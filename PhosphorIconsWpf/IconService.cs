using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Xml;
using System;
using ZiggyCreatures.Caching.Fusion;

namespace PhosphorIconsWpf;

/// <summary>
/// Provides static services for loading and rendering Phosphor icons from embedded SVG resources.
/// </summary>
/// <remarks>
/// This static service reads SVG icon files embedded in the assembly, extracts the vector path data,
/// and converts them into WPF geometry objects and drawing images. Icon data is cached using
/// FusionCache for optimal performance on repeated access.
/// </remarks>
public static class IconService
{
    private static readonly Assembly Assembly = typeof(IPhosphorIconsWpfMarker).Assembly; 
    private static readonly FusionCache Cache = new(new FusionCacheOptions());
    
    /// <summary>
    /// Converts an icon enum value to its corresponding file name format.
    /// </summary>
    /// <param name="icon">The icon to convert.</param>
    /// <returns>The icon name with underscores replaced by hyphens.</returns>
    private static string GetIconName(Icon icon)
        => $"{icon}".Replace("_", "-");

    /// <summary>
    /// Constructs the full resource stream name for a specific icon and type.
    /// </summary>
    /// <param name="icon">The icon to locate.</param>
    /// <param name="iconType">The visual style of the icon.</param>
    /// <returns>The fully qualified resource name.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the icon type is not supported.</exception>
    private static string GetIconStreamName(Icon icon, IconType iconType)
    {
        switch (iconType)
        {
            case IconType.bold:
            case IconType.fill:
            case IconType.light:
            case IconType.thin:
                return $"PhosphorIconsWpf.Icons.{iconType}.{GetIconName(icon)}-{iconType}.svg";
            case IconType.regular:
                return $"PhosphorIconsWpf.Icons.{iconType}.{GetIconName(icon)}.svg";
            default:
                throw new InvalidOperationException($"Icon type '{iconType}' not supported");
        }
    }

    /// <summary>
    /// Retrieves the embedded resource stream for the specified icon.
    /// </summary>
    /// <param name="icon">The icon to load.</param>
    /// <param name="iconType">The visual style of the icon.</param>
    /// <returns>A stream containing the SVG icon data, or null if not found.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static Stream? GetIconStream(Icon icon, IconType iconType)
        => Assembly.GetManifestResourceStream(GetIconStreamName(icon, iconType));

    /// <summary>
    /// Extracts the SVG path data from the icon's embedded SVG file.
    /// </summary>
    /// <param name="icon">The icon to load.</param>
    /// <param name="iconType">The visual style of the icon.</param>
    /// <returns>The SVG path data string from the 'd' attribute.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the icon is not found or the path data cannot be extracted.
    /// </exception>
    // ReSharper disable once MemberCanBePrivate.Global
    public static string GetIconData(Icon icon, IconType iconType)
    {
        // Load the SVG file from embedded resources
        using var stream = GetIconStream(icon, iconType)
                           ?? throw new InvalidOperationException($"Icon '{icon}' not found");
        using var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();

        // Parse the SVG XML content
        var xml = new XmlDocument();
        xml.LoadXml(content);

        // Set up namespace manager for SVG namespace
        var xnm = new XmlNamespaceManager(xml.NameTable);
        xnm.AddNamespace("std", "http://www.w3.org/2000/svg");

        // Extract the path element which contains the vector data
        var node = xml.SelectSingleNode("/std:svg/std:path", xnm);

        // Parse the 'd' attribute which contains the path data
        if (node?.Attributes?["d"]?.Value is { } val)
            return val;

        throw new InvalidOperationException($"Cannot read icon '{GetIconName(icon)}'");
    }

    /// <summary>
    /// Creates a WPF Geometry object from the specified icon.
    /// </summary>
    /// <param name="icon">The icon to render.</param>
    /// <param name="iconType">The visual style of the icon.</param>
    /// <returns>A <see cref="Geometry"/> object containing the icon's vector path.</returns>
    public static Geometry CreateGeometry(Icon icon, IconType iconType)
    {
        var cacheKey = $"{icon}_{iconType}";
        var data = Cache.GetOrSet<string>(cacheKey, _ => GetIconData(icon, iconType));
        
        // Parse the icon data into a Geometry object
        return Geometry.Parse(data);
    }

    /// <summary>
    /// Creates a DrawingImage with the specified icon and brush color.
    /// </summary>
    /// <param name="icon">The icon to render.</param>
    /// <param name="iconType">The visual style of the icon.</param>
    /// <param name="brush">The brush to use for filling the icon.</param>
    /// <returns>A <see cref="DrawingImage"/> that can be used as an image source.</returns>
    public static DrawingImage CreateDrawingImage(Icon icon, IconType iconType, Brush brush)
    {
        // Get the vector geometry for the icon
        var geometry = CreateGeometry(icon, iconType);

        // Create a drawing image with the geometry and specified brush
        var drawingImage = new DrawingImage(new GeometryDrawing
        {
            Geometry = geometry,
            Brush = brush
        });
        
        return drawingImage;
    }
}