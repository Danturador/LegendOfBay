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
# Файл нейминг

## Категории файлов

1. Анимации
`Anim_[Объект]_[Действие]_[Состояние/Доп. атрибуты]`

* Пример:
    * `Anim_Player_Run_Loop`
    * `Anim_Enemy_Attack_Fast`
    * `Anim_UI_ButtonClick`
 
2. Материалы
`Mat_[Объект/Назначение]_[Характеристика]`

* Пример:
    * `Mat_Environment_Grass`
    * `Mat_Character_Skin`
    * `Mat_UI_Button`
 
3. Спрайты
`Sprite_[Категория]_[Название/Состояние]_[Размер/Версия]`

* Пример:
    * `Sprite_Icons_Coin_64x64`
    * `Sprite_Character_Jump_128x128`
    * `Sprite_UI_Menu_Highlight`
 
4. Префабы
`Prefab_[Объект]_[Назначение/Тип]`

* Пример:
    * `Prefab_Enemy_Spearman`
    * `Prefab_Player_MainCharacter`
    * `Prefab_UI_PauseMenu`
 
5. Аудио
`Audio_[Категория]_[Описание]_[Формат/Длина]`

* Пример:
    * `Audio_UI_Click_Short`
    * `Audio_Environment_Wind_Loop`
    * `Audio_Character_Footstep_Grass`

 6. Шрифты
`Font_[Название]_[Размер/Характеристики]`

* Пример:
    * `Font_Arial_Regular_14pt`
    * `Font_ComicSans_Bold`
 
7. Сцены
`Scene_[Категория]_[Название]`

* Пример:
    * `Scene_MainMenu`
    * `Scene_Level1_Forest`
 
8. Шейдеры
`Shader_[Категория]_[Тип/Особенности]`

* Пример:
    * `Shader_UI_Outline`
    * `Shader_Environment_Water`
 
