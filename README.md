# PhosphorIconsWpf

A comprehensive WPF UI library providing access to **1,000+ beautiful, open-source icons** from [Phosphor Icons](https://phosphoricons.com/). Easily integrate scalable vector icons into your WPF applications with simple XAML markup extensions.

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/phosphor-icons/homepage?tab=readme-ov-file#license)

## ✨ Features

- 🎨 **1,000+ high-quality icons** covering UI, arrows, shapes, brands, technology, and more
- 🎭 **5 visual styles**: Bold, Fill, Light, Regular, and Thin
- 🚀 **Easy XAML integration** with markup extensions
- 📦 **Embedded SVG resources** - no external dependencies
- 🎯 **Full control** over size, color, and styling
- ⚡ **Performance optimized** - icons parsed on-demand

## 📦 Installation

```
dotnet add package PhosphorIconsWpf
```

## 🚀 Quick Start

### 1. Import the Namespace

Add the namespace to your XAML file:

```xaml
xmlns:piw="clr-namespace:PhosphorIconsWpf.Markup;assembly=PhosphorIconsWpf"
```

### 2. Use Icons in Your UI

#### With Image Control (IconSource)

The `IconSource` extension creates a complete `DrawingImage` perfect for Image controls:

```xaml
<Image Source="{piw:IconSource Icon=airplane_landing, IconType=fill, Brush=Black}" />
```

#### With PathIcon Control (IconGeometry)

The `IconGeometry` extension provides raw geometry data for maximum flexibility:

```xaml
<Image Width="64" Height="64">
    <Image.Source>
        <DrawingImage>
            <DrawingImage.Drawing>
                <GeometryDrawing Geometry="{piw:IconGeometry Icon=file, IconType=bold}" Brush="Black" />
            </DrawingImage.Drawing>
        </DrawingImage>
    </Image.Source>
</Image>
```

## 🎨 Icon Styles

Each icon is available in **5 visual styles**:

| Style | Description | Use Case |
|-------|-------------|----------|
| `bold` | Thick, prominent strokes | Emphasis, primary actions |
| `fill` | Solid filled shapes | Active states, selections |
| `light` | Thin, delicate strokes | Subtle UI, large displays |
| `regular` | Standard stroke width | General purpose (default) |
| `thin` | Minimal stroke weight | Elegant, minimalist designs |

Copyright (c) 2025 Josué Clément

Made with ❤️ for the WPF community