#include "stdafx.h"
#include "NisMacro.Net.Core.h"


//	NisMacro.net全てのメイン関数
//	本関数をNisElementsから起動することで実行される
extern "C" int StartHttpConnect(){
	HttpConnect::Program::Main();
	return 0;
};

