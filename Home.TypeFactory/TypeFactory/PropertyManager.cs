using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Home.TypeFactory
{
    internal class PropertyManager
    {
        public void DefineProperty(TypeBuilder builder, Type type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("propertyName");
            }

            string fieldName = Char.ToLowerInvariant(name[0]) + name.Substring(1);
            string propertyName = Char.ToUpperInvariant(name[0]) + name.Substring(1);

            FieldBuilder fieldBuilder = builder.DefineField(
                fieldName,
                type,
                FieldAttributes.Private);

            PropertyBuilder propertyBuilder = builder.DefineProperty(
                propertyName,
                PropertyAttributes.HasDefault,
                type, 
                null);

            //Getter
            MethodBuilder getMethodBuilder = builder.DefineMethod(
                "get_" + propertyName, 
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig,
                type, 
                Type.EmptyTypes);

            ILGenerator getILGenerator = getMethodBuilder.GetILGenerator();

            getILGenerator.Emit(OpCodes.Ldarg_0);
            getILGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            getILGenerator.Emit(OpCodes.Ret);

            //Setter
            MethodBuilder setMethodBuilder = builder.DefineMethod(
                "set_" + propertyName, 
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig, 
                null,
                new Type[] { type });

            ILGenerator setILGenerator = setMethodBuilder.GetILGenerator();

            setILGenerator.Emit(OpCodes.Ldarg_0);
            setILGenerator.Emit(OpCodes.Ldarg_1);
            setILGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            setILGenerator.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
        }
    }
}
