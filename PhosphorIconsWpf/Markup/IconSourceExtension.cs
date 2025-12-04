using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace PhosphorIconsWpf.Markup;

/// <summary>
/// XAML markup extension that provides DrawingImage sources for Phosphor icons.
/// </summary>
/// <remarks>
/// This extension creates a complete <see cref="DrawingImage"/> with the specified icon and brush,
/// suitable for use as an image source in Image controls or other visual elements.
/// </remarks>
public class IconSourceExtension : MarkupExtension
{
    /// <summary>
    /// Gets or sets the brush used to paint the icon. Defaults to black.
    /// </summary>
    public Brush Brush { get; set; } = Brushes.Black;

    /// <summary>
    /// Gets or sets the icon to be rendered.
    /// </summary>
    public Icon Icon { get; set; }
    
    /// <summary>
    /// Gets or sets the visual style of the icon. Defaults to regular.
    /// </summary>
    public IconType IconType { get; set; } = IconType.regular;

    /// <summary>
    /// Provides a DrawingImage containing the rendered icon.
    /// </summary>
    /// <param name="serviceProvider">The service provider from the XAML context.</param>
    /// <returns>A <see cref="DrawingImage"/> that can be used as an image source.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var service = new IconService();
        return service.CreateDrawingImage(Icon, IconType, Brush);
    }
}