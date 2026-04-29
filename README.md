# VistaForge Widgets

VistaForge Widgets is a modern desktop widget platform for Windows 11, inspired by the classic Windows Vista Sidebar gadgets. It provides a flexible, extensible, and visually rich environment for displaying real-time information directly on the desktop.

The project is designed as both a functional productivity tool and a developer platform for building custom widgets.

## Overview

VistaForge Widgets reimagines desktop widgets using modern Windows technologies. It enables users to place interactive, customizable widgets directly on their desktop, while providing developers with a structured SDK to create new widgets.

This project emphasizes:

*   Native Windows 11 UI/UX
*   Modular architecture
*   Extensibility via plugins
*   Real-time data integration
*   High-performance rendering

## Key Features

### Desktop Widget System

*   Widgets render directly on the desktop layer
*   Optional "Always on Top" mode
*   Click-through transparency mode (optional)
*   Multi-monitor support
*   Z-order control (background, normal, overlay)

### Widget Management

*   Drag-and-drop positioning
*   Resizable widgets with constraints
*   Snap-to-grid alignment (optional)
*   Widget locking/unlocking
*   Show/Hide toggle per widget
*   Grouping (future phase)

### Layout Persistence

*   Automatic save/restore of widget positions
*   Multiple layout profiles
*   Import/export layouts via JSON

### Theming and Appearance

*   Light/Dark mode support
*   Windows accent color integration
*   Acrylic / Mica effects
*   Rounded corners and fluent design system
*   Opacity and blur customization per widget

### Performance

*   Low memory footprint
*   Efficient UI virtualization
*   Background update throttling
*   Configurable refresh rates

## Built-in Widgets (MVP + Extended)

### Core Widgets (MVP)

*   **Clock Widget**
    *   Digital and analog modes
    *   Timezone support
*   **Sticky Notes**
    *   Rich text support
    *   Persistent storage
*   **System Monitor**
    *   CPU usage
    *   RAM usage
    *   Disk activity

### Extended Widgets

*   **Weather Widget**
    *   Location-based forecasts
    *   API integration (OpenWeather or similar)
*   **Calendar Widget**
    *   Agenda view
    *   Integration-ready (Google/Outlook future)
*   **Music Widget**
    *   Displays currently playing media
    *   Integration with system media session
*   **RSS/News Feed**
    *   Custom feed URLs
    *   Auto-refresh
*   **Countdown Timer**
    *   Event-based countdowns
*   **Quick Launcher**
    *   Custom app shortcuts
*   **Network Monitor**
    *   Upload/download speeds
*   **Battery & Power Widget**
    *   Battery percentage
    *   Power mode status

## UI/UX Design System

### Global UI Elements

*   **Widget Container (Root Window)**
    *   Transparent background
    *   Desktop-layer anchored window
    *   Input handling for widget interaction
*   **Widget Card**
    *   Rounded corners (8–16px radius)
    *   Acrylic/Mica background
    *   Drop shadow
    *   Header bar (optional)
*   **Widget Header**
    *   Title label
    *   Drag handle
    *   Action buttons:
        *   Settings
        *   Refresh
        *   Close/Hide
*   **Resize Handles**
    *   Corner-based resizing
    *   Snap constraints
*   **Context Menu (Right Click)**
    *   Add widget
    *   Remove widget
    *   Lock position
    *   Settings
    *   Theme toggle
*   **Global Toolbar (Optional Floating UI)**
    *   Add Widget
    *   Layout Manager
    *   Settings
    *   Toggle visibility

### Widget-Level UI Components

Each widget can include:

*   Title/Header Bar
*   Content Area
*   Footer (optional)
*   Settings Panel (flyout or modal)

**Common UI Controls**

*   TextBlocks
*   Icons (Fluent Icons)
*   Progress Bars
*   Charts/Graphs
*   Input fields (notes, settings)
*   Toggle switches
*   Dropdown selectors
*   Buttons

### Settings UI

*   **Global Settings Panel**
    *   Theme (Light/Dark/System)
    *   Transparency level
    *   Blur intensity
    *   Update frequency
    *   Startup behavior
    *   Multi-monitor preferences
*   **Widget Settings Panel**
    *   Widget-specific configuration
    *   API keys (for weather/news)
    *   Layout customization
    *   Display options

## Architecture

### High-Level Structure

```
VistaForge/
│
├── Core/
│   ├── WidgetHost
│   ├── LayoutManager
│   ├── ThemeManager
│   ├── SettingsManager
│   └── PluginLoader
│
├── Widgets/
│   ├── ClockWidget
│   ├── NotesWidget
│   ├── SystemMonitorWidget
│   └── ...
│
├── UI/
│   ├── MainWindow.xaml
│   ├── WidgetContainer.xaml
│   └── SharedControls/
│
├── Plugins/
│   └── (External Widgets)
│
└── Assets/
```

### Core Components

*   **WidgetHost:** Manages widget lifecycle, handles rendering and placement, coordinates input events.
*   **LayoutManager:** Saves/loads widget positions, handles layout profiles, serializes to JSON.
*   **ThemeManager:** Applies system or custom themes, controls accent colors and effects.
*   **SettingsManager:** Global configuration persistence, user preferences.
*   **PluginLoader:** Dynamically loads widget assemblies, validates widget manifests, sandboxing (future phase).

### Plugin System (Widget SDK)

**Goals**

*   Allow third-party widget development
*   Maintain consistent UI/UX
*   Ensure stability and security

**Widget Structure**

Each widget includes:

*   Assembly (.dll)
*   Manifest (JSON)
*   UI Component (XAML)

**Example Manifest**

```json
{
  "name": "WeatherWidget",
  "version": "1.0.0",
  "author": "Developer",
  "entryPoint": "WeatherWidget.Main",
  "minWidth": 200,
  "minHeight": 150
}
```

**Widget Interface (Concept)**

```csharp
public interface IWidget
{
    string Name { get; }
    UIElement Render();
    void Initialize();
    void Update();
    void Dispose();
}
```

## Technology Stack

*   **Language:** C#
*   **Framework:** .NET 10
*   **UI Framework:** WinUI 3 (Windows App SDK)
*   **Architecture:** MVVM
*   **Data Storage:** JSON (local)
*   **Optional APIs:** Weather API, RSS feeds, System diagnostics

## Development Roadmap

*   **Phase 1 – Foundation:** Desktop host window, Basic widget container, Drag and resize support
*   **Phase 2 – Core Widgets:** Clock, Notes, System monitor
*   **Phase 3 – Persistence:** Layout saving/loading, Settings system
*   **Phase 4 – UI Polish:** Acrylic effects, Animations, Theme system
*   **Phase 5 – Plugin System:** Widget SDK, Dynamic loading
*   **Phase 6 – Advanced Features:** Multi-monitor support, Widget grouping, Marketplace concept

## Installation (Planned)

1.  Download latest release
2.  Run installer
3.  Launch VistaForge Widgets
4.  Add widgets via context menu

## Future Enhancements

*   HTML/JavaScript widget support
*   Cloud sync of layouts
*   Widget marketplace
*   AI-powered widgets (summaries, assistants)
*   Voice interaction
*   Mobile companion app

## Contribution

Contributions are welcome. Planned areas for contribution include:

*   New widgets
*   UI enhancements
*   Performance optimization
*   Plugin SDK expansion

## License

MIT License (or your preferred license)

## Vision

VistaForge Widgets aims to bring back the charm and utility of desktop widgets while modernizing them for today’s systems. It serves both as a productivity tool and a platform for innovation in desktop UI development.
