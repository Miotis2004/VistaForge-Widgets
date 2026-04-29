using System;
using System.Reflection;

Console.WriteLine("Loading tests...");

var asm = Assembly.LoadFrom("tests/VistaForge.Widgets.Tests/bin/Debug/net10.0-windows10.0.19041.0/VistaForge.Widgets.Tests.dll");

Console.WriteLine("Tests Loaded");
