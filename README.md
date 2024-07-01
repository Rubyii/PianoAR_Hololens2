# PianoAR

# Beschreibung
PianoAR ist eine Anwendung auf der Hololens 2,
die Hilfslinien für Klaviertasten anzeigt.

## Benötige Programme
- Microsoft Visual Studio 2022
- Unity Editor version 2022.3.13f1
- Microsoft Mixed Reality Toolkit

## Benötige Pakete
- Microsoft Visual Studio 2022:
    - Spielentwicklung mit Unity
    - Entwicklung für die universelle Windows-Plattform
    - Desktopentwicklung mit C++
    - Spieleentwicklung mit C++
    - MSVC v143 - VS 2022 C++-Arm Buildtools(neueste Version)
    - MSVC v143 - VS 2022 C++-Arm64/ARM64EC Buildtools(neueste Version)
    - C++-Universelle Windows-Plattform-Unterstützung für v143-Buildtools
    ![Screenshot](Bilder/VisualStudio2022Pakete.png)

- Microsoft Mixed Reality Toolkit:
    - Alles von MRTK3
    - Platform Support: Mixed Reality OpenXR Plugin

## Unity Setup für die Hololens 2
- 3D Core nutzen
- Unity offen lassen und MRTK3 Tool starten <br>
[Pakete](#benötigte-pakete) auswählen<br>

![Anleitung](https://learn.microsoft.com/de-de/windows/mixed-reality/develop/unity/welcome-to-mr-feature-tool)
für die MRTK3 Paketinstallation

## Build Settings für Unity
![Screenshot](Bilder/UnityBuildSettings.png)

## Wie man auf die Hololens Deployed
1. Entwicklermodus auf der Hololens und auf einem Windows-PC aktivieren
2. Unity Builden 
3. .sln im Build Ordner mit MVS2022 starten
4. Projektname als Startprojekt festlegen<br>
5. Arm oder Arm64, Release oder Debug auswählen![Screenshot](Bilder/KonfigurationsOptionenMVS.png)
6. Auswählen ob man über [USB(Gerät)](#kabel-deploy) oder ![Remote(Remotecomputer)](#remote-deploy)  deployen will<br>
![Screenshot](Bilder/DeployAuswahl.png)<br>
<br>
![Microsoft Anleitung](https://learn.microsoft.com/de-de/windows/mixed-reality/develop/advanced-concepts/using-visual-studio?tabs=hl2) zum Deployen

### Kabel Deploy
Mit Kabel verbinden, Gerät auswählen und Erstellen

### Remote Deploy
1. Remotecomputer auswählen
2. Unter Debugeigenschaften->Debugging->Computernamen die IP der Hololens eintragen
3. Bei der ersten Verbindung mit der Hololens wird ein Kopplungscode erwartet. Den findet man auf der Hololens unter Entwicklermodus->Koppeln
4. Erstellen

## Assets
Alle genutzten Assets sind aus dem MRTK Standard Assets, was bei der installation von MRTK beinhaltet ist.

## Hilfreiche Videos
https://www.youtube.com/watch?v=dOsYerpKloY<br>
https://www.youtube.com/watch?v=ntaThZjom0o
