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
	
	case DLL_PROCESS_ATTACH: // DLL���v���Z�X�̃A�h���X��ԂɃ}�b�s���O���ꂽ�B
		_Module.Init( ObjectMap, (HINSTANCE)hinstDll );
		nisInstance = _Module.GetResourceInstance();
		DisableThreadLibraryCalls( (HMODULE)hinstDll );
		break;
	
	case DLL_THREAD_ATTACH: // �X���b�h���쐬����悤�Ƃ��Ă���B
		break;
	
	case DLL_THREAD_DETACH: // �X���b�h���j������悤�Ƃ��Ă���B
		break;
	
	case DLL_PROCESS_DETACH: // DLL�̃}�b�s���O����������悤�Ƃ��Ă���B
		break;

	}

	return TRUE;
}
