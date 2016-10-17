/**-----------------------------------------------------------------
* @explanation:登陆
* @created：Raindya
* @create time：2015/11/24 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define("QST.Main.Login", {
    alternateClassName: 'userLogin',
    extend: 'Ext.form.Panel',
    xtype: 'userLogin',
    requires: ['Ext.form.FieldSet', 'Ext.field.Password', 'Ext.field.Select'],
    config: {
        scrollable: null,
        title: config.str.loginTitle,
        redirect: null,
        items: [
            {
                xtype: 'component',
                styleHtmlContent: true,
                margin: '0 10 0 10',
                html: '<div style="text-align:center"><img src="resources/images/Main/logo@3x.png" width="30%" height="30%"></div><div style="text-align:center"></div>'
            },
            {
                xtype: 'fieldset',
                defaults: {
                    labelWidth: '40%'
                },
                margin: '0 20 0 20',
                items: [{
                    xtype: 'textfield',
                    name: 'Account', allowBlank: true,
                    style: 'height:40px;font-size: 1.4em;',
                    placeHolder: config.str.loginNameTip
                }, {
                    xtype: 'passwordfield',
                    name: 'Password', allowBlank: true,
                    style: 'height:40px;font-size: 1.4em;',
                    placeHolder: config.str.loginPwdTip
                }, {
                    xtype: 'hiddenfield',
                    name: 'IsRememberLogin',
                    value: true
                }, {
                    xtype: 'hiddenfield',
                    name: 'Marked',
                    value: 'phone'
                }]
            },

        {
            xtype: 'button',
            text: config.str.loginTitle,
            action: 'login',
            cls: 'loginButton',
            style: 'font-size:1.4em;font-weight:bold',
            height: '60px',
            margin: '30 20 0 20',
            handler: function (but) {
                this.up('userLogin').fireEvent('login', but);
            }
        }, {
            xtype: 'panel',
            //横向
            layout: 'hbox',
            defaults: {
                flex: 1
            },
            items: [
                //注册帐号
                {
                    html: '<span class="zc">' + config.str.Registered + '</span>',
                    style: {
                        'text-align': 'left',
                        'margin': '20px 10px 0 20px',
                        'color': ' #00BBFF',
                        'font-weight': 'bold',
                        'font-size': '0.8em'
                    },
                    listeners: [{
                        event: 'tap',
                        fn: function () {
                            util.redirectTo("QST.Main.Registered", "", { parentUrl: "QST.Main.Login", url: config.url + '/User/Add' });
                        },
                        element: 'innerElement',
                        delegate: 'span.zc'
                    }]
                },
                //忘记密码
                {
                    html: '<span class="zh">' + config.str.Retrieve + '</span>',
                    style: {
                        'text-align': 'right',
                        'margin': '20px 20px 0 10px',
                        'color': ' #00BBFF',
                        'font-weight': 'bold',
                        'font-size': '0.8em'
                    },
                    listeners: [{
                        event: 'tap',
                        fn: function () {
                            util.redirectTo("QST.Main.Retrieve", "", { parentUrl: "QST.Main.Login" });
                        },
                        element: 'innerElement',
                        delegate: 'span.zh'
                    }]
                }
            ]
        },
        {
            html: '<span class="zc">使用社交帐号登录</span>',
            style: {
                'text-align': 'center',
                'margin': '100px 10px 0 20px',
                'color': ' #888888',
                'font-weight': 'bold',
                'font-size': '0.9em'
            },
        },
        {
            xtype: 'panel',
            layout: 'hbox',
            defaults: { flex: 1 },
            items: [
                //微博
                {
                    html: '<div style="text-align:center"><img src="resources/images/Main/weibo@2x.png" width="50%" height="50%"></div><div style="text-align:center"></div>',
                    style: {
                        'text-align': 'left',
                        'margin': '40px 10px 0 50px',
                        'color': ' #666',
                        'font-size': '.8em'
                    },
                    listeners: [{
                        event: 'tap',
                        fn: function () {
                            util.redirectTo("QST.Main.Registered", "", { parentUrl: "QST.Main.Login", url: config.url + '/User/Add' });
                        },
                        element: 'innerElement',
                        delegate: 'span.zc'
                    }]
                },
                //微信
                {
                    html: '<div style="text-align:center"><img src="resources/images/Main/weixin@2x.png" width="50%" height="50%"></div><div style="text-align:center"></div>',
                    style: {
                        'text-align': 'center',
                        'margin': '40px 20px 0 10px',
                        'color': ' #666',
                        'font-size': '.8em'
                    },
                    listeners: [{
                        event: 'tap',
                        fn: function () {
                            util.redirectTo("QST.Main.Retrieve", "", { parentUrl: "QST.Main.Login" });
                        },
                        element: 'innerElement',
                        delegate: 'span.zh'
                    }]
                },
                //QQ
                {
                    html: '<div style="text-align:center"><img src="resources/images/Main/QQ@2x.png" width="50%" height="50%"></div><div style="text-align:center"></div>',
                    style: {
                        'text-align': 'right',
                        'margin': '40px 50px 0 10px',
                        'color': ' #666',
                        'font-size': '.8em'
                    },
                    listeners: [{
                        event: 'tap',
                        fn: function () {
                            util.redirectTo("QST.Main.Retrieve", "", { parentUrl: "QST.Main.Login" });
                        },
                        element: 'innerElement',
                        delegate: 'span.zh'
                    }]
                }
            ]
        }
        ],
        listeners: {
            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back", { fType: 'Login' });
            },
            //登录
            login: function (but) {
                var me = this;
                var account = me.down('textfield[name=Account]').getValue();
                var password = me.down('passwordfield[name=Password]').getValue();
                //是否输入帐号或密码
                if (account != "" && password != "") {
                    util.showMessage(config.str.LoginStatus, false);
                    me.submit({
                        url: config.url + '/User/LoginPhone',
                        method: 'POST',
                        success: function (action, response) {
                            //存储用户信息
                            util.storeSet("logininfor", Ext.JSON.encode(response.data));
                            config.LoginUser = response.data;
                            util.hideMessage();
                            //初始化用户数据
                            me.loadInit();
                            SHUtil.UpdateEquipment();
                            ////更新当前用户设备信息[10秒后执行 不占用菜单请求时间]
                            //setTimeout(SHUtil.UpdateEquipment, 10000);
                            //Ext.Msg.alert('提示', response.msg);
                        },
                        failure: function (action, response) {
                            util.hideMessage();
                            if (response.status == 500) {
                                Ext.Msg.alert('提示', config.string.LoginError);
                            }
                            else {
                                Ext.Msg.alert('提示', response.msg);
                            }
                        }
                    });
                } else {
                    Ext.Msg.alert('提示', "请输入帐号和密码");
                }
            }
        }
    },
    //初始化
    constructor: function (config) {
        var me = this;
        me.callParent(arguments);
        util.rightSwipe(me, "Back");
        ////加载头部菜单信息
        me.add(this.getHeader());

    },
    // 初始化系统信息
    loadInit: function () {
        var me = this;
        config.CurrentTime = 0;
        //加载用户信息
        Ext.Ajax.request({
            url: config.url + '/InitData/InitPhoneMyInfo',
            success: function (response) {
                Ext.apply(config.idata,
                    Ext.decode(response.responseText)
                );
                util.hideMessage();
                //切换到主界面
                me.switchingMain();
            }
        });
    },
    //切换到主界面
    switchingMain: function () {
        //util.redirectTo("QST.Main.Projects", "", { parentUrl: "QST.Main.Login" });
        var pInfo = Ext.decode(util.storeGet("projectInfo"));
        config.project = pInfo;
        var user = Ext.decode(util.storeGet("logininfor"));
        var LoginName = "";
        if (this.User) {
            LoginName = this.User.LoginName
        }
        if (LoginName == user.LoginName) {
            if (!pInfo) {
                util.redirectTo("QST.Main.Projects", "", { parentUrl: "QST.Main.Login", url: this.Url });
            } else {
                util.redirectTo("QST.Main.Layout", "", { projectName: pInfo.pName, projectId: pInfo.pId, url: this.Url });
                this.onSuccess();
            }
        } else {
            util.redirectTo("QST.Main.Projects", "", { parentUrl: "QST.Main.Login", url: this.Url });
        }
    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params)
            this.backUrl = params.parentUrl;
        if (params.url)
            this.Url = params.url

        if (params.onSuccess) {
            this.onSuccess = params.onSuccess;
        }
        this.User = Ext.decode(util.storeGet("logininfor"));
        //获得用户登陆信息
        var user = Ext.decode(util.storeGet("logininfor"));
        if (user) {
            this.setValues({ Account: user.LoginName, Password: '' });
        }
    },
    //头部菜单信息(private)
    getHeader: function () {
        var me = this;
        if (!this._homeHeaderBar) {
            this._homeHeaderBar = Ext.create("app.user.NavigationBar", {
                title: me._title,
                docked: 'top',
                items: [
                    {
                        iconCls: 'arrow_left',
                        action: 'Back',
                        cls: 'nbutton',
                        align: 'left',
                        handler: function (but) {
                            me.fireEvent('Back', but, me);
                        }
                    }
                ],
            });
        }
        return this._homeHeaderBar;
    },
    //子界面返回到此界面时加载
    overViewResult: function (params) {
        if (params) {

        }
    }
});