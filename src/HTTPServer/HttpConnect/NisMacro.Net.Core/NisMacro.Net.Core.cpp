#include "stdafx.h"
#include "NisMacro.Net.Core.h"


//	NisMacro.net�S�Ẵ��C���֐�
//	�{�֐���NisElements����N�����邱�ƂŎ��s�����
extern "C" int StartHttpConnect(){
	HttpConnect::Program::Main();
	return 0;
};

