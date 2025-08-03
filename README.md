
# RTSPDeck

**RTSPDeck** is a lightweight, open-source desktop application for Windows (10+) that lets you view and manage multiple RTSP security camera feeds. Built in C# using WPF and powered by LibVLCSharp.

> **Note**: Most of the architecture and functionality of this project was designed and implemented with the help of [ChatGPT (OpenAI)](https://openai.com/chatgpt).

---

## 🚀 Features

- 📷 View multiple RTSP streams in a grid layout
- 🔒 Save camera settings (IP, username, password, etc.) to a local config file
- ➕ Add/remove cameras dynamically via a GUI
- 🧩 Built with modern .NET and VLC libraries
- 🖥️ Compatible with most ONVIF / RTSP-capable NVRs and IP cameras
- 🌗 Attempts to follow system theme (light/dark), but full theme support is still being improved

---

## 📦 Tech Stack

- [.NET 8.0 (LTS)](https://dotnet.microsoft.com/)
- [WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
- [LibVLCSharp](https://code.videolan.org/videolan/LibVLCSharp)
- JSON-based local config file for camera persistence

---

## 🛠️ How to Build

### Prerequisites

- Visual Studio 2022 or newer
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Git (optional, for cloning)

> 🔧 **No standalone VLC installation is required at runtime.** The app uses `VideoLAN.LibVLC.Windows` NuGet package, which bundles the required native binaries (`libvlc.dll`, `libvlccore.dll`, `plugins/`). Just publish the full output folder.

> 🏗️ **Note**: Final build targets `x64` only.

### Steps

1. **Clone the repository:**

   ```bash
   git clone https://github.com/yourname/RTSPDeck.git
   cd RTSPDeck
   ```

2. **Open the solution:**

   Open `RTSPDeck.sln` in Visual Studio 2022.

3. **Install required NuGet packages (if not restored):**

   ```powershell
   Install-Package LibVLCSharp.WPF
   Install-Package LibVLCSharp
   Install-Package VideoLAN.LibVLC.Windows
   ```

4. **Build and run the project.**

---

## 📂 Configuration

RTSPDeck uses a JSON file to store camera information. This is created automatically on first run as `config.json` in the same directory as the executable.

Each camera feed entry includes:

- `Name`
- `IPAddress`
- `Port`
- `Username`
- `Password`
- `CameraNumber`

These values are used to generate a complete RTSP URL for each feed.

---

## 🎥 RTSP URL Format

RTSPDeck builds stream URLs like so:

```
rtsp://{username}:{password}@{ip}:{port}/Streaming/Channels/{cameraNumber}
```

Example:

```
rtsp://admin:secretpass@192.168.0.90:554/Streaming/Channels/101
```

This format is compatible with ANNKE, Hikvision, Dahua, Reolink, and most ONVIF-compliant systems.

---

## 🌓 Theme Support

RTSPDeck attempts to automatically follow your system theme (light or dark). However, full support (including runtime switching and modern accent color blending) is still in development. For now:

- Default styling matches light theme
- Some system brushes (`SystemColors.*BrushKey`) are used dynamically
- Dark theme styling may require explicit selection in a future release

---

## 🙏 Credits

- Core design and implementation assisted by [ChatGPT](https://chat.openai.com)
- RTSP video handling via [LibVLCSharp](https://code.videolan.org/videolan/LibVLCSharp)
- WPF UI based on native Windows controls
- Inspired by a need for a minimal, no-cloud, open RTSP viewer

---

## 📄 License

This project is licensed under the **MIT License**. You are free to use, modify, distribute, and share.
