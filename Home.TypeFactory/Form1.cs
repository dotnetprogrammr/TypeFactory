using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Home.TypeFactory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var assemblyManager = new AssemblyManager();
            var typeManager = new TypeManager();
            var constructorManager = new ConstructorManager();
            var ilManager = new ILManager();
            var propertyManager = new PropertyManager();

            var assembly = assemblyManager.InstantiateAssemblyBuilder();
            var module = assemblyManager.GenerateModuleBuilder(assembly);
            var type = typeManager.InstantiateTypeBuilder(module, "MyFirstType");
            var ctor = constructorManager.GenerateParameterlessConstructor(type);
            ilManager.EmitConstrucorIL(ctor);

            propertyManager.DefineProperty<System.Int32>(type, "MyFirstProperty");

            var instance = type.CreateType();

            //// assembly.Save(assembly.GetName().Name + ".dll");
        }
    }
}
