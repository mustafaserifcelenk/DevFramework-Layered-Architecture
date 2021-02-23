using DevFramework.Core.Aspects.Postsharp.ExceptionAspects;
using DevFramework.Core.Aspects.Postsharp.PerformanceAspects;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DevFramework.Northwind.Mvc.WebUI")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("DevFramework.Northwind.Mvc.WebUI")]
[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
//[assembly: PerformanceCounterAspect(2,AttributeTypes = "DevFramework.Northwind.Business.Concrete.Managers.*")] 2 burada 5 olan default yerine

//[assembly: LogAspect(typeof(FileLogger), AttributeTargetTypes = "DevFramework.Northwind.Business.Concrete.Managers.*")] Buraya log yazarsan tüm managerlar etkilenir, veya attributeu kullanarak belli bir dosyadaki tüm managerları loglarsın ve ya get ile başlayanları (.Get*), veya içinde Add geçenleri(.*Add*)
//[assembly: ExceptionLogAspect("")] yukardakiyle aynı

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("6d56b563-4600-41fe-93ce-bca5f7afdd0c")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
