using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastMember;

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
            var exception = new HttpException("This is an exception", "This is why the exception happened");

            var assemblyManager = new AssemblyManager();
            var typeManager = new TypeManager();
            var constructorManager = new ConstructorManager();
            var ilManager = new ILManager();
            var propertyManager = new PropertyManager();

            var assembly = assemblyManager.InstantiateAssemblyBuilder();
            var module = assemblyManager.GenerateModuleBuilder(assembly);
            var builder = typeManager.InstantiateTypeBuilder(module, "MyFirstType");
            var ctor = constructorManager.GenerateParameterlessConstructor(builder);
            ilManager.EmitConstrucorIL(ctor);

            propertyManager.DefineProperty(builder, typeof(System.String), "Type");
            propertyManager.DefineProperty(builder, typeof(System.String), "Title");
            propertyManager.DefineProperty(builder, typeof(System.String), "Detail");

            // Some reflective shit here.
            // Find the properties unique to this exception
            var properties = exception.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (PropertyInfo property in properties)
            {
                propertyManager.DefineProperty(builder, property.PropertyType, property.Name);
            }

            var type = builder.CreateType();

            var iAccessor = TypeAccessor.Create(type);
            var eAccessor = TypeAccessor.Create(exception.GetType());

            ConstructorInfo ctorInfo = type.GetConstructor(new Type[] { });
            var instance = ctorInfo.Invoke(new object[] { });

            iAccessor[instance, "Type"] = "http://problems.rakuten.co.uk/internal-server-error";
            iAccessor[instance, "Title"] = "Internal Server Error";
            iAccessor[instance, "Detail"] = exception.Message;

            foreach (PropertyInfo property in properties)
            {
                iAccessor[instance, property.Name] = eAccessor[exception, property.Name];
            }

            //// assembly.Save(assembly.GetName().Name + ".dll");
        }
    }
}
