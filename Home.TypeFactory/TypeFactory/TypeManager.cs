using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Home.TypeFactory
{
    internal class TypeManager
    {
        public TypeBuilder InstantiateTypeBuilder(ModuleBuilder builder, string name)
        {
            return builder.DefineType(
                name,
                TypeAttributes.Public |
                TypeAttributes.Class |
                TypeAttributes.AutoClass |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit |
                TypeAttributes.AutoLayout,
                typeof(object),
                Type.EmptyTypes);
        }
    }
}
