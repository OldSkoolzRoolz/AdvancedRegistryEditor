# Advanced RegistryEditor (Adapted from RegistryEditor by giladreich https://github.com/giladreich/RegistryEditor))

## Overview
**RegistryEditor** is a Windows Forms application for advanced management and monitoring of the Windows Registry. It provides a user-friendly interface to search, view, export, delete, and monitor registry keys and values, both locally and remotely.

## Features

- **Registry Search**  
  - Search for registry keys, values, and data using customizable filters.
  - Supports case-sensitive and regular expression searches.
  - Find results across multiple hives, including remote computers.

- **Registry Management**  
  - Export and delete registry hives with automatic emergency backup.
  - Open registry locations directly from search results.
  - Clipboard integration for copying selected registry paths.

- **Monitoring (ETW)**  
  - Real-time monitoring of registry changes using Event Tracing for Windows (ETW).
  - Filter monitoring by process name, user, key name, and event type.
  - Log registry events to a file for auditing and analysis.

- **User Interface**  
  - Modern Windows Forms UI with details and list views.
  - Progress indicators and robust error handling.
  - Tree view for navigating registry hives and subkeys.

- **Remote Registry Support**  
  - Search and manage registry keys on remote machines (with limitations on certain hives).

## Technologies Used

- **C# 13.0**  
  - Utilizes the latest language features for robust and maintainable code.

- **.NET 9**  
  - Targets the newest .NET runtime for performance and compatibility.

- **Windows Forms**  
  - Provides a rich desktop UI experience.

- **Microsoft.Win32**  
  - Direct interaction with the Windows Registry.

- **Event Tracing for Windows (ETW)**  
  - Monitors registry changes in real time.

## Getting Started

1. **Requirements**
   - Windows 10/11
   - .NET 9 Runtime
2. **Build & Run**
   - Open the solution in Visual Studio 2022.
   - Build and run the project.
3. **Usage**
   - Use the search and management tools to interact with the registry.
   - Start monitoring to track registry changes.

## License

Distributed under an Open Source License. See file headers for details.

---

*Author: Kyle Crowder*  
*GitHub: [OldSkoolzRoolz](https://github.com/OldSkoolzRoolz)*
