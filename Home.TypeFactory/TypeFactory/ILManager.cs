using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Home.TypeFactory
{
    internal class ILManager
    {
        public void EmitConstrucorIL(ConstructorBuilder builder)
        {
            ConstructorInfo conObj = typeof(object).GetConstructor(new Type[0]);

            //call constructor of base object
            ILGenerator il = builder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, conObj);
            il.Emit(OpCodes.Ret);
        }
    }
}
