// Author: Daniel Leal
// Why do programmers not like nature?
// Too many bugs and no documentation...

namespace PingPong;

internal static class Program
{
  /// <summary>
  ///  The main entry point for the application.
  /// </summary>
  [STAThread]
  static void Main()
  {
    // To customize application configuration such as set high DPI settings or default font,
    // see https://aka.ms/applicationconfiguration.
    ApplicationConfiguration.Initialize();
    Application.Run(new MainForm());
  }
}