/**-----------------------------------------------------------------
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
                '<tr ><th >头像</td></tr>',
                '<tr><td>{LoginName}</td></tr>',
                '<tr ><th >昵称</td></tr>',
                '<tr><td>{LoginName}</td></tr>',
             '</table>',
          '</div>',
          '<div  class="table-d">',
              '<table>',
                '<tr ><th >性别</td></tr>',
                //'<tr><td>{[SHUtil.GetDiction(values.Gender)]}</td></tr>',
                '<tr><td>{LoginName}</td></tr>',
                '<tr ><th >手机号</td></tr>',
                '<tr><td>{Mobile}</td></tr>',
                '<tr ><th >出身年月</td></tr>',
                //'<tr><td>{[SHUtil.InterceptTime(values.Birth)]}</td></tr>',
                 '<tr><td>{LoginName}</td></tr>',
                '<tr ><th>账户安全</th></tr>',
                '<tr><td><span class="AS" style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: #ACCC6B;font-weight:bold">账户安全</span></td></tr>',
                '<tr ><th>地址管理</th></tr>',
                //'<tr><td>{[SHUtil.OpenList("点击进入地址管理","AM")]}</td></tr>',
                 '<tr><td><span class="AM" style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: #ACCC6B;font-weight:bold">地址管理</span></td></tr>',
             '</table>',
             '</div>'
        ),
        listeners: {
            // 查看更多事件
            More: function (but, view) {
                this.setStructureMenu(this.config.listMenu, this.data);
            }
        }
    },
    //主界面到此界面时加载
    rendering: function (params) {
        var me = this;
        //if (params.parentUrl) {
        //    me.backUrl = params.parentUrl;
        //}
        //this.Id = Ext.JSON.decode(util.storeGet("logininfor")).Id;
        //this.Name = Ext.JSON.decode(util.storeGet("logininfor")).Name;
        me.data();
    },
    //加载数据
    data: function () {
        var me = this;
        this.Id = Ext.JSON.decode(util.storeGet("logininfor")).Id;
        this.Name = Ext.JSON.decode(util.storeGet("logininfor")).Name;
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
            delegate: 'span.AS'
        });
        // 地址管理
        me.addListener('tap', function (but, view, record) {
            util.redirectTo("QST.Users.Address.List", "",
                        {
                            parentUrl: "QST.Users.Address.MyAccount",
                            data: { userId: this.Id, userName: this.Name }
                        });
        }, me, {
            element: 'innerElement',
            delegate: 'span.AM'
        });
    }
})