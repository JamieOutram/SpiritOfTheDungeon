#ifndef CUSTOMFUNCTIONS_INCLUDED
#define CUSTOMFUNCTIONS_INCLUDED

void XorYConditional_float(float A, float B, float T, out float Out) 
{
	if (T == 0) 
	{
		Out = A;
	}
	else 
	{
		Out = B;
	}
}

#endif