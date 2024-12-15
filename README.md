# Модульная архитектура

```mermaid
flowchart BT
    classDef bar stroke:#0f0
    classDef red stroke:#cc0000
    classDef yellow stroke:#ffd966
    classDef main fill:#006195
    Bootstrap:::main-->Init(Initialization):::yellow;
    Bootstrap-->Dep(Dependencies):::yellow;
    
    Init-->Mods@{ shape: procs, label: "Modules"}
    
    Dep-->SceneContext;
    Zenject:::bar@{ shape: hex, label: "Zenject" }-->Dep;
    EntryPoint:::bar@{ shape: hex, label: "Entry Point" }-->Init;
    Addressables:::bar@{ shape: hex, label: "Addressables" }-->LocalContentLoader;
    SceneContext(Scene Context)<-.->Mods;
    Mods-->ModuleGraph;
    
    subgraph ModuleGraph [Module Instance]
    direction BT
    IModule:::red-->MonoInstaller(Mono Installer);
    IModule-->Prefab(Prefab);
    end

    subgraph Modules
    direction RL
    Player-->Level;
    Level-->Enemies;
    Enemies-->Etc[...];
    end

    Mods-->Modules;
    Init<-->LocalContentLoader@{ shape: cyl, label: "Local Content Loader" };
```

# Файловая архитектура

```mermaid
flowchart TD
    Pr(Project Files)-->FF
    Pr(Project Files)-->Misc3

    subgraph Misc3 [Misc]
    Misc(Misc)-->Scenes
    Misc-->URP
    Misc-->Misc1(...)
    end
    
    subgraph FF [Features]
    direction RL
    Features@{ shape: docs, label: "Features" }-->Feature1(Feature)

    Feature1-.-Scripts(Scripts)
    Feature1-.-Animations(Animations)
    Feature1-.-Sprites(Sprites)
    Feature1-.-VFX(VFX)
    Feature1-.-Misc5(Misc)
    end
```
