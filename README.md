# SoulverTools

**SoulverTools** is a collection of tools, extensions, attributes, and helper classes for *Unity*.

***All components are focused on reducing boilerplate code and simplifying daily development workflows.***

> [!NOTE]
> This repository contains runtime utilities, editor helpers, and prepared systems.  
> Some features are still under development.

---

## Features Overview

- Tilemap tools
- Custom Inspector attributes
- Runtime extensions
- Settings systems
- Utility data classes
- Gizmos helpers

---

## Tilemap Tools

### Tilemap to PNG Export

Export Unity Tilemaps to PNG images.

**Use cases:**
- debugging
- minimap generation
- level previews
- external processing


> [!TIP]
> Useful when a static Tilemap representation is required outside of Unity.

---

## Custom Inspector Attributes

Attributes used to control Inspector visibility and behavior.

- **ReadOnlyAttribute**
  - Displays a value in the Inspector but prevents editing.
- **ShowIfAttribute**
  - Base conditional attribute.
- **ShowIfBoolAttribute**
  - Shows fields based on a boolean value.
- **ShowIfEnumAttribute**
  - Shows fields depending on enum state.
- **ToHashAttribute**
  - Converts string values into hashes (Animator parameters, IDs).

> [!IMPORTANT]
> These attributes help keep the Inspector clean and prevent invalid configurations.

---

## Extensions

Runtime extensions simplifying gameplay, UI, and animation logic.

- FlyToTarget
- IntValueAnimator
- AnimationEndEvent
- MultiAnimationQueue
- ViewContent
  - EventTrigger based
- SpriteResolverAnimation
- ToggleSwapImage

---

## Settings Systems

- **Audio Settings**
  - Ready-to-use scripts for volume control and persistence.
- **Language Settings**
  - *Work in progress.*


---

## Utility Classes

<a name="unitydata"></a>
### UnityData

Static helper class for common Unity operations.

- Child GameObject collection
- Image component caching
- Alpha control for UI elements
- Button and EventTrigger callbacks
- Animation clip length lookup


---

### SwitcherLink

Utility class for controlled event subscriptions.

- prevents duplicate subscriptions
- centralized enable / disable logic
- explicit subscription state

> [!TIP]
> Useful for UI lifecycle and state-based systems.

---

### ValueData

Helpers for numeric and range-based logic.

- inclusive range checks
- exclusive range checks
- Vector2Int support
- value normalization


---

### ConverterData

Conversion helper utilities.

- World position → Tile position


---

## Gizmos Utilities

### GizmosPrefab

Contains predefined standard shapes for Gizmos generation.

- debugging
- level design
- logic visualization in Scene View

> [!WARNING]
> Gizmos are editor-only and should not be used for gameplay logic.

---

## Project Status

> [!CAUTION]
> The project is under active development.  
> APIs and structure may change.

---

## Navigation

- [UnityData](#unitydata)
- Extensions
- Attributes
- Gizmos

<!-- This content will not appear in the rendered Markdown -->

