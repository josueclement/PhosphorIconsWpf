using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace PhosphorIconsWpf.Markup;

/// <summary>
/// XAML markup extension that provides vector geometry data for Phosphor icons.
/// </summary>
/// <remarks>
/// This extension returns a WPF <see cref="Geometry"/> object that can be used
/// directly in Path elements or other controls that accept geometry data.
/// </remarks>
public class IconGeometryExtension : MarkupExtension
{
    /// <summary>
    /// Gets or sets the icon to be rendered.
    /// </summary>
    public Icon Icon { get; set; }
    
    /// <summary>
    /// Gets or sets the visual style of the icon.
    /// </summary>
    public IconType IconType { get; set; }

    /// <summary>
    /// Provides the geometry data for the specified icon.
    /// </summary>
    /// <param name="serviceProvider">The service provider from the XAML context.</param>
    /// <returns>A WPF <see cref="Geometry"/> object containing the icon's vector path data.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var service = new IconService();
        return service.CreateGeometry(Icon, IconType);
    }
}