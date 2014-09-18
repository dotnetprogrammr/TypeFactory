using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Home.TypeFactory
{
    internal class AssemblyManager
    {
        public AssemblyBuilder InstantiateAssemblyBuilder()
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "Home.Web.Http.Problems";
            AppDomain thisDomain = Thread.GetDomain();

            AssemblyBuilder builder = thisDomain.DefineDynamicAssembly(
                assemblyName,
                AssemblyBuilderAccess.Run);

            return builder;
        }

        public ModuleBuilder GenerateModuleBuilder(AssemblyBuilder assemblyBuilder)
        {
            return assemblyBuilder.DefineDynamicModule(assemblyBuilder.GetName().Name, false);
        }
    }
}
