# VistaForge Widgets - Development Plan

This document outlines the detailed development phases, project setup scaffolding, and comprehensive testing strategy for the VistaForge Widgets platform.

## 0. Project Setup & Scaffolding

### Objective
Establish the foundational solution structure using C#, .NET 10, and WinUI 3 (Windows App SDK). Set up continuous integration and automated testing environments.

### Actions
1. **Initialize Solution:**
   - Create a blank Visual Studio Solution named `VistaForge`.
2. **Project Scaffolding:**
   - Create a WinUI 3 App (Packaged) project named `VistaForge.App` (maps to `UI/MainWindow.xaml` and application entry).
   - Create class library projects for modularity:
     - `VistaForge.Core` (WidgetHost, LayoutManager, ThemeManager, SettingsManager, PluginLoader).
     - `VistaForge.Widgets` (Built-in MVP widgets: Clock, Notes, System Monitor).
     - `VistaForge.SDK` (Public interfaces `IWidget` and base classes for third-party developers).
3. **Dependency Mapping:**
   - Link `VistaForge.Core` and `VistaForge.Widgets` to `VistaForge.App`.
   - Both `Core` and `Widgets` will reference `VistaForge.SDK`.
4. **Directory Structure:**
   - Map physical folders to the architecture outlined in the README (Core, Widgets, UI, Plugins, Assets).
5. **Testing Framework Setup:**
   - Set up `xUnit` testing projects for `Core` and `Widgets` (`VistaForge.Core.Tests`, `VistaForge.Widgets.Tests`).
   - Configure Moq or NSubstitute for dependency mocking.

### Testing Strategy - Setup
*   **Unit Tests:** Verify that all projects compile and project references are correctly established. Write a basic dummy test in each test project to ensure the test runner executes successfully.
*   **Integration Tests:** Verify that the main `VistaForge.App` launches to a blank WinUI 3 window without crashing.
*   **CI/CD Validation:** Setup a GitHub Action (or similar) to ensure the solution builds on PRs.

---

## Phase 1: Foundation

### Objective
Create the desktop host window that sits on the desktop layer, implement the basic widget container, and enable drag and resize interactions.

### Actions
1. **Desktop Layer Anchoring:**
   - Implement Win32 interop to position the `MainWindow` at the desktop level (behind icons, or just above wallpaper).
   - Make the `MainWindow` transparent and frameless.
2. **Widget Container (`WidgetHost`):**
   - Create a generic container XAML control that hosts `IWidget.Render()`.
   - Implement z-order management.
3. **Interactions:**
   - Implement drag-and-drop logic for positioning widgets within the bounds of the `MainWindow`.
   - Add resize handles to the Widget Container with constraint enforcement (min/max size).

### Testing Strategy - Phase 1
*   **Unit Tests:** Test the math and constraints logic for resizing and dragging. Ensure out-of-bounds positioning corrects itself.
*   **UI/Integration Tests:** Use WinAppDriver or Appium to automate dragging a mock widget from point A to point B and verify the updated coordinates. Verify that resizing a widget respects minimum bounds.
*   **Manual Testing:** Visually confirm the main window sits correctly on the desktop layer and ignores standard window manager behaviors like minimize all.

---

## Phase 2: Core Widgets (MVP)

### Objective
Implement the first set of functional widgets: Clock, Sticky Notes, and System Monitor.

### Actions
1. **SDK Solidification:**
   - Finalize the `IWidget` interface (`Name`, `Render()`, `Initialize()`, `Update()`, `Dispose()`).
2. **Clock Widget:**
   - Implement UI (XAML) for Digital and Analog views.
   - Implement tick updates using `DispatcherTimer` or background threading.
3. **Sticky Notes Widget:**
   - Implement rich text input control.
   - Setup basic local state retention (in memory first, tied to Phase 3 for disk).
4. **System Monitor Widget:**
   - Utilize `System.Diagnostics` or Win32 Performance Counters to fetch CPU, RAM, and Disk metrics.
   - Build a visual gauge/chart component to display data.

### Testing Strategy - Phase 2
*   **Unit Tests:**
    - Clock: Mock the system time and verify that the widget's internal time string updates correctly.
    - Notes: Test character limits and basic text formatting logic.
    - System Monitor: Mock performance counter outputs and verify the percentage calculations.
*   **UI/Integration Tests:** Verify that widgets render their specific UIs without throwing XAML binding errors.
*   **Performance Testing:** Ensure the System Monitor polling does not cause UI thread spikes (validate asynchronous updating).

---

## Phase 3: Persistence

### Objective
Save and load layout arrangements and user settings using local JSON storage.

### Actions
1. **SettingsManager:**
   - Implement a singleton or DI service to manage global configurations (Theme, Update Frequency).
   - Serialize/Deserialize to `%AppData%\VistaForge\settings.json`.
2. **LayoutManager:**
   - Track active widgets, their X/Y coordinates, Z-index, and width/height.
   - Serialize layout state to `%AppData%\VistaForge\layouts\default.json`.
3. **Lifecycle Hooks:**
   - Tie the `LayoutManager` to the application startup (restore) and exit/idle events (save).

### Testing Strategy - Phase 3
*   **Unit Tests:** Test serialization and deserialization of layout objects and setting models to/from JSON strings. Test edge cases (corrupted JSON, missing file, permission errors) to ensure graceful fallbacks.
*   **Integration Tests:** Write tests that spin up the `LayoutManager`, add a widget, trigger a save, instantiate a *new* `LayoutManager`, load the file, and verify the widget's state matches.
*   **E2E Tests:** Boot the app, move a widget, kill the app process, restart the app, and verify the widget spawns at the updated location.

---

## Phase 4: UI Polish

### Objective
Integrate fluent design principles, system themes, and animations.

### Actions
1. **ThemeManager:**
   - Hook into Windows 11 system theme changes (Light/Dark).
   - Apply Windows Accent colors to widget UI elements (e.g., toggles, progress bars).
2. **Materials & Effects:**
   - Apply WinUI 3 Backdrop materials (Mica for the app window if applicable, Acrylic for widget cards).
   - Implement rounded corners (`CornerRadius`) matching Windows 11 guidelines.
3. **Animations:**
   - Add transition animations for widget opening, closing, and resizing.

### Testing Strategy - Phase 4
*   **Unit Tests:** Test the `ThemeManager`'s ability to detect and parse system color registry keys or API responses.
*   **UI Tests:** Verify that changing the internal theme state triggers a visual state change in the main XAML resource dictionaries.
*   **Manual/Visual Testing:** Ensure Mica and Acrylic effects render correctly (these are notoriously difficult to test purely via automation and require human visual confirmation on a target Windows 11 device).

---

## Phase 5: Plugin System

### Objective
Enable dynamic loading of external `.dll` files as widgets to allow third-party development.

### Actions
1. **PluginLoader:**
   - Implement `AssemblyLoadContext` to isolate loaded plugins.
   - Scan a dedicated `Plugins/` folder for assemblies containing classes that implement `IWidget`.
2. **Manifest Validation:**
   - Read and validate the accompanying `manifest.json` for each plugin before loading the DLL to ensure basic metadata requirements are met.
3. **Sandboxing & Security (Basic):**
   - Implement exception handling boundaries so a crashing plugin does not crash the `WidgetHost`.

### Testing Strategy - Phase 5
*   **Unit Tests:**
    - Create a dummy valid DLL and an invalid DLL. Verify the `PluginLoader` successfully loads the valid one and rejects the invalid one.
    - Test manifest validation logic (missing fields, wrong types).
*   **Integration Tests:** Load a plugin that intentionally throws an exception in its `Update()` method and verify the `WidgetHost` catches it, disables the widget, and continues running.
*   **Security Tests:** Ensure plugins cannot access isolated internal memory spaces of other plugins.

---

## Phase 6: Advanced Features

### Objective
Implement multi-monitor support, widget grouping, and the marketplace concept.

### Actions
1. **Multi-Monitor Support:**
   - Detect screen boundaries, DPI scaling differences, and handle moving widgets across different displays.
   - Maintain separate layouts per monitor.
2. **Widget Grouping:**
   - Allow snapping widgets together into a single movable block or tabbed interface.
3. **Marketplace (Conceptual Phase):**
   - Build a rudimentary UI within settings to browse a remote JSON feed of available plugins and download them to the `Plugins/` directory.

### Testing Strategy - Phase 6
*   **Unit Tests:** Test the screen bounding box math for multi-monitor setups (e.g., negative X coordinates for monitors on the left).
*   **UI/Integration Tests:** Mock the multi-monitor API responses and ensure the `LayoutManager` saves widget coordinates relative to the correct virtual screen.
*   **E2E Tests:** For the marketplace, mock a local HTTP server serving a plugin list and test the download, extraction, and loading lifecycle.