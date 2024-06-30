using System;
using TECHCOOL.UI;

var containers = Podman.GetContainers();
Screen.Display(new ContainerListScreen(containers));

Console.Clear();

