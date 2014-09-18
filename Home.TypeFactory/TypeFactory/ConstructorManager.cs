using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Home.TypeFactory
{
    internal class ConstructorManager
    {
        public ConstructorBuilder GenerateParameterlessConstructor(TypeBuilder builder)
        {
            return this.GenerateConstructor(builder, Type.EmptyTypes);
        }

        public ConstructorBuilder GenerateConstructor(TypeBuilder builder, Type[] parameters)
        {
            return builder.DefineConstructor(
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName,
                CallingConventions.Standard,
                parameters);
        }
    }
}
