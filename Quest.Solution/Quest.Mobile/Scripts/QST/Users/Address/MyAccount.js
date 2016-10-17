/*-----------------------------------------------------------------
* @explanation:我的账户信息展示界面
* @created：XS
* @create time：2015/11/25
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Users.Address.MyAccount', {
    extend: 'app.user.NTemplate',
    xtype: 'user_address_myaccount',
    config: {
        moreBut: false,
        title: config.str.MyAccount,
        tpl: new Ext.XTemplate(
          '<div  class="table-d">',
             '<table>',
                 //头像
                '<tr><td style="height: 150px;">',
                '<div style="float:left;width:50%; margin:35px 0px 0 0"><span style="font-size: 130%">头像</span></div>',
                '<div style="float:right;width:50%;text-align:right;">' +
                    '<div style="float:left;width:50%"><img width="100" height="100" src="resources/images/Users/Address/touxiangda@2x.png" style="display: inline; float: right; margin:5px -60px 0 0;"/></div>',
                    '<div style="float:left;width:50%;text-align:right;">' +
                        '<img width="15" height="25" src="resources/images/Arrow.png" style="display: inline; float: right; margin:45px 10px 0 0;"/>',
                     '</div>',
                 '</div>',
                '</td></tr>',
                 //昵称
                '<tr><th style="padding: .1em 1em;"></th></tr>',
                '<tr><td style="height: 80px;">',
                '<div style="float:left;width:50%"><span style="font-size: 130%;">昵称</span></div>',
                '<div style="float:left;width:50%;text-align:right;">' +
                    '<span >{LoginName}</span>',
                 '</div>',
                '</td></tr>',
                //性别
                '<tr><th style="padding: 1em 1em;"></th></tr>',
                 '<tr><td style="height: 80px;">',
                '<div style="float:left;width:50%"><span style="font-size: 130%;">性别</span></div>',
                '<div style="float:left;width:50%;text-align:right;">' +
                    '<span >女</span>',
                 '</div>',
                '</td></tr>',
                //手机号
                '<tr><th style="padding: .1em 1em;"></th></tr>',
                '<tr><td style="height: 80px;">',
                '<div style="float:left;width:50%"><span style="font-size: 130%;">手机号</span></div>',
                '<div style="float:left;width:50%;text-align:right;">' +
                    '<span >{Mobile}</span>',
                 '</div>',
                '</td></tr>',
                //出生日期
               '<tr><th style="padding: .1em 1em;"></th></tr>',
                 '<tr><td style="height: 80px;">',
                '<div style="float:left;width:50%"><span style="font-size: 130%;">出生日期</span></div>',
                '<div style="float:left;width:50%;text-align:right;">' +
                    '<span >{[this.InterceptTime(values.Birth)]}</span>',
                 '</div>',
                '</td></tr>',
                 //账户安全
                '<tr><th style="padding: .1em 1em;"></th></tr>',
                '<tr><td class="AS" style="height: 80px;">',
                '<div style="float:left;width:50%"><span style="font-size: 130%;">账户安全</span></div>',
                '<div style="float:left;width:50%;text-align:right;">' +
                   '<img width="15" height="25" src="resources/images/Arrow.png" style="display: inline; float: right; margin:5px 10px 0 0;"/>',
                 '</div>',
                '</td></tr>',
                 //地址管理
                '<tr><th style="padding: .1em 1em;"></th></tr>',
                '<tr><td class="AM" style="height: 80px;">',
                '<div style="float:left;width:50%"><span style="font-size: 130%;">地址管理</span></div>',
                '<div style="float:left;width:50%;text-align:right;">' +
                   '<img width="15" height="25" src="resources/images/Arrow.png" style="display: inline; float: right; margin:5px 10px 0 0;"/>',
                 '</div>',
                '</td></tr>',
             '</table>',
             '</div>', {
                 InterceptTime: function (value) {
                     if (value)
                         return value.substr(0, 10);
                     return value;
                 },
                 GetDiction: function (key) {
                     var dictions = Ext.decode(config.idata.sysInfo.dictions);
                     var dName = '';
                     Ext.Array.each(dictions, function (item) {
                         if (item.DictionKey == key) {
                             dName = dName + item.DictionValue
                         }
                     });
                     return dName;
                 }
             }
        ),
        listeners: {
            //返回前一界面
            Back: function (list) {
                util.redirectTo("QST.Main.Layout", "back", {});
            }
        }
    },
    //主界面到此界面时加载
    rendering: function (params) {
        var me = this;
        if (params.parentUrl) {
            me.backUrl = params.parentUrl;
        }
        //this.Id = Ext.JSON.decode(util.storeGet("logininfor")).Id;
        //this.Name = Ext.JSON.decode(util.storeGet("logininfor")).Name;

    },
    //加载数据
    data: function () {
        var me = this;
        //this.Id = Ext.JSON.decode(util.storeGet("logininfor")).Id;
        //this.Name = Ext.JSON.decode(util.storeGet("logininfor")).Name;
        Ext.Ajax.request({
            //基本信息
            url: config.url + '/User/GetByLoginId',
            params: { dicKey: "00000000-0000-0000-0001-000000000001" },
            success: function (response) {
                var result = Ext.decode(response.responseText);
                if (result.success) {
                    Ext.Array.each(result.data, function (item) {
                        //if (item.Accessory && item.Accessory != "") {
                        //    item.Accessory = Ext.decode(item.Accessory);
                        //}
                        me.setTplData(item);
                    });
                }
            }
        });
    },
    //子界面到此界面时加载
    overViewResult: function (params) {
        this.data();
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);
        me.data();
        me.set_Listener();
    },
    // 设置事件
    set_Listener: function () {
        var me = this;
        // 账户安全
        me.addListener('tap', function (but, view, record) {
            util.redirectTo("SH.App.HRManagement.UserBank.List", "",
                         {
                             parentUrl: "QST.Users.Address.MyAccount",
                             data: { BankUserId: this.Id }
                         });
        }, me, {
            element: 'innerElement',
            delegate: 'td.AS'
        });
        // 地址管理
        me.addListener('tap', function (but, view, record) {
            util.redirectTo("QST.Users.Address.List", "",
                        {
                            parentUrl: "QST.Users.Address.MyAccount",
                            data: { userId: "00000000-0000-0000-0001-000000000001", userName: this.Name }
                        });
        }, me, {
            element: 'innerElement',
            delegate: 'td.AM'
        });
    }
})