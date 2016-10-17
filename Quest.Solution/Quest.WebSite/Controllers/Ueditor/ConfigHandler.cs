using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// Config 的摘要说明
/// </summary>
public class ConfigHandler : Handler
{
    public ConfigHandler(Controller context) : base(context) { }

    public override void Process()
    {
        WriteJson(Config.Items);
    }
}