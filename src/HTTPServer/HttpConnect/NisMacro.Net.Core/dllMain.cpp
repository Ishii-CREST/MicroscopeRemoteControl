#include "stdafx.h"
#include "dllMain.h"

#pragma unmanaged
CComModule		_Module;	
HINSTANCE nisInstance;
BEGIN_OBJECT_MAP(ObjectMap)
END_OBJECT_MAP()

BOOL APIENTRY DllMain(HINSTANCE hinstDll, DWORD dwReason, LPVOID lpReserved)
{
	switch (dwReason) {
	
	case DLL_PROCESS_ATTACH: // DLLがプロセスのアドレス空間にマッピングされた。
		_Module.Init( ObjectMap, (HINSTANCE)hinstDll );
		nisInstance = _Module.GetResourceInstance();
		DisableThreadLibraryCalls( (HMODULE)hinstDll );
		break;
	
	case DLL_THREAD_ATTACH: // スレッドが作成されようとしている。
		break;
	
	case DLL_THREAD_DETACH: // スレッドが破棄されようとしている。
		break;
	
	case DLL_PROCESS_DETACH: // DLLのマッピングが解除されようとしている。
		break;

	}

	return TRUE;
}
