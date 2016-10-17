using System;
using SuHui.Core.Data;
using System.ComponentModel.Composition;
using DynamicCodeGenerate.Model;
namespace DynamicCodeGenerate.Core
{
    [Export(typeof(IHelloWorldService))]
    internal partial class HelloWorldService : RepositoryBase<HelloWorld, Guid>, IHelloWorldService
    {
    }
    public partial interface IHelloWorldService : IRepository<HelloWorld, Guid>
    {
    }
}