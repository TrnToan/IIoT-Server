namespace MagicMirrorIotServer.Api.Helpers;

public delegate Task AsyncEventHandler<TEventArgs>(object? sender, TEventArgs e);