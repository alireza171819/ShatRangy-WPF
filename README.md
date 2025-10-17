# ShatRangy (WPF)

A small practice project built with **Windows Presentation Foundation (WPF)** and **Material Design in XAML**.

> This README was authored based on the repository structure and the original short description. It is ready to drop in as `README.md` at the repo root.

---

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Build & Run](#build--run)
- [FAQ](#faq)
- [Roadmap (Suggestions)](#roadmap-suggestions)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)

## Overview
ShatRangy demonstrates a layered WPF application styled with **Material Design**. The solution groups responsibilities into data access, business logic, view models (MVVM), and a WPF UI project.

## Features
- WPF desktop UI with **Material Design in XAML** components.
- Clean, layered structure: DataLayer, Business, ViewModels, and the main WPF app.
- MVVM-friendly organization to separate UI logic from presentation.
- Visual Studio–friendly solution for quick build and run.

## Project Structure
```
ShatRangy-WPF/
├─ Business/           # Business logic/services
├─ DataLayer/          # Models + data access
├─ VeiwModels/         # ViewModel classes (MVVM)  (folder name is intentionally kept as in the repo)
├─ ShatRangyy/         # WPF application (XAML views, App.xaml, resources)
├─ ShatRangy.sln       # Solution file
├─ .gitignore
└─ README.md           # (this file)
```

## Getting Started

### Prerequisites
- **Windows 10/11**
- **Visual Studio 2019 or 2022** with the **.NET desktop development** workload
- **.NET Framework 4.7.2 or 4.8** installed (you can retarget the project if needed)

### Clone
```bash
git clone https://github.com/alireza171819/ShatRangy-WPF.git
cd ShatRangy-WPF
```

### Open
- Open `ShatRangy.sln` in Visual Studio.
- Let NuGet restore packages automatically (or use **Restore NuGet Packages** from the Solution context menu).
- Ensure the startup project is **ShatRangyy**.

## Build & Run
- Press **F5** to build and run (Debug) from Visual Studio.
- If NuGet packages fail to restore, try:
  - **Tools → NuGet Package Manager → Package Manager Console** then:
    ```powershell
    Update-Package -Reinstall
    ```
  - Or right-click the solution → **Restore NuGet Packages**.

## FAQ

### What UI framework/theme is used?
This sample uses **Material Design in XAML** for theming and controls. If packages are missing, add/reinstall them via NuGet (e.g., `MaterialDesignThemes` / `MaterialDesignColors`).

### Can I run it on newer .NET versions?
The project targets .NET Framework. You can retarget to **.NET Framework 4.8** or migrate to **.NET (6/7/8) WPF** with some tweaks.

### Where do I add new screens?
Add new **Views** (XAML) under `ShatRangyy`, create corresponding **ViewModels** under `VeiwModels`, and bind via DataContext (MVVM).

## Roadmap (Suggestions)
- Add unit tests for **Business** and **DataLayer**.
- Document ViewModel responsibilities and bindings.
- Add screenshots of the UI under a `docs/` folder.
- Enable a simple GitHub Actions workflow to build the solution on push/PR.

## Contributing
PRs are welcome! For major changes, please open an issue to discuss what you would like to change.

## License
No license file was found in the repository. If you plan to reuse this code, please add a `LICENSE` file to clarify terms.

## Acknowledgments
- [Material Design in XAML Toolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
- WPF & MVVM community resources

---

_Last updated: 2025-10-17_
