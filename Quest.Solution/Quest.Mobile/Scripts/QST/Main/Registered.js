/**-----------------------------------------------------------------
* @explanation:注册
* @created：Raindya
* @create time：2015/11/24 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define("QST.Main.Registered", {
    alternateClassName: 'main_registered',
    extend: 'Ext.form.Panel',
    xtype: 'main_registered',
    requires: ['Ext.form.FieldSet', 'Ext.field.Password', 'Ext.field.Select'],
    config: {
        scrollable: null,
        formSubmit: false,
        title: config.str.Register,
        redirect: null,
        items: [
            {
                xtype: 'fieldset',
                defaults: {
                    labelWidth: '40%'
                },
                margin: '30 10 0 10',
                items: [
                    {
                        name: 'Mobile',
                        placeHolder: '请输入手机号（必填）',
                        xtype: 'textfield', regex: /^1[3|4|5|8][0-9]{9}$/,
                        regexText: '手机号码格式错误！',
                        anchor: '90%',
                        allowBlank: true
                    },
                     {
                         label: '手机是否验证',
                         name: 'IsValidMobile',
                         xtype: 'hiddenfield'
                     },
                     {
                         xtype: 'panel',
                         layout: 'hbox',
                         cls: 'selectFile',
                         items: [
                             {
                                 xtype: 'textfield',
                                 name: 'VerificationCode',
                                 placeHolder: '验证码（必填）',
                                 allowBlank: true,
                                 flex: 1,
                                 labelWidth: '33%'
                             }, {
                                 xtype: 'button',
                                 text: '获取验证码',
                                 cls: 'loginButton',
                                 style: 'font-size:0.8em',
                                 height: '40px',
                                 width: '80px',
                                 handler: function (but) {
                                     //事件
                                 }
                             }
                         ]
                     },
                     {
                         name: 'Password',
                         placeHolder: '请输入密码,6-12位的数字或字母（必填）',
                         xtype: 'passwordfield',
                         regex: /^[\da-zA-z]{6,12}$/,
                         regexText: '密码格式错误！',
                         anchor: '90%',
                         allowBlank: true
                     },
                     {
                         label: '密码是否验证',
                         name: 'IsValidPassword',
                         xtype: 'hiddenfield'
                     },
                ]
            },
        {
            xtype: 'button',
            text: config.str.Register,
            action: 'login',
            cls: 'loginButton',
            style: 'font-size:1.4em;',
            height: '60px',
            margin: '30 20 0 20',
            handler: function (but) {
                var me = this.up('main_registered');
                me.fireEvent('formSubmit', but, me);
            }
        },
        {
            xtype: 'panel',
            layout: 'hbox',
            defaults: {
                flex: 1
            },
            items: [
                {
                    html: '已有帐号？<span class="dl", style="color: #00BBFF ">' + config.str.loginTitle + '</span>',
                    style: {
                        'font-size': '1.4em;',
                        'text-align': 'center',
                        'margin': '20px 10px 0 20px',
                        'color': ' #666',
                    },
                    listeners: [{
                        event: 'tap',
                        fn: function () {
                            util.redirectTo("QST.Main.Login");
                        },
                        element: 'innerElement',
                        delegate: 'span.dl'
                    }]
                }
            ]
        },
         {
             xtype: 'panel',
             layout: 'hbox',
             defaults: {
                 flex: 1,
             },
             items: [

                 {
                     xtype: 'checkboxfield',
                     name: 'Protocol',
                     margin: '250 0 0 0',
                     //html: '<img class="rad" src="resources/images/Users/Address/yigouxuan@2x.png" width="20%" height="20%"> ',
                     //style: {
                     //    'font-size': '1.4em;',
                     //    'text-align': 'center',
                     //    'margin': '20px 10px 0 20px',
                     //    'color': ' #666',
                     //},
                     //listeners: [{
                     //    event: 'tap',
                     //    fn: function () {
                     //        util.redirectTo("QST.Main.Login");
                     //    },
                     //    element: 'innerElement',
                     //    delegate: 'img.rad'
                     //}],
                     checked: true,
                 }, {
                     html: '<div style="width:300px;">已同意并阅读<span class="dl", style="color: #00BBFF ">《注册协议》</span></div>',
                     style: {
                         'font-size': '1.4em;',
                         'text-align': 'left',
                         'margin': '255px 150px 0 0',
                         'color': ' #666'
                     },
                     listeners: [{
                         event: 'tap',
                         fn: function () {
                             util.redirectTo("QST.Main.Login");
                         },
                         element: 'innerElement',
                         delegate: 'span.dl'
                     }]
                 }
             ]
         }, ],
        listeners: {
            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back", { fType: 'Login' });
            },
            //自定义提交
            formSubmit: function (but, view) {
                //验证手机
                var phone = view.down('textfield[name=Mobile]').getValue();
                if (phone == "" || phone == null) {
                    Ext.Msg.alert('提示', '电话号码不能为空！');
                    return;
                }
                //验证手机格式
                var phoneErrors = view.down('textfield[name=Mobile]').config
                if (!phoneErrors.regex.test(phone)) {
                    Ext.Msg.alert('提示', phoneErrors.regexText);
                    return;
                }
                //验证码
                var vcod = view.down('textfield[name=VerificationCode]').getValue();
                if (vcod == "" || vcod == null) {
                    Ext.Msg.alert('提示', '验证码不能为空！');
                    return;
                }
                //验证密码
                var pwd = view.down('passwordfield[name=Password]').getValue();
                if (pwd == "" || pwd == null) {
                    Ext.Msg.alert('提示', '密码不能为空！');
                    return;
                }
                //验证密码格式
                var passwordErrors = view.down('passwordfield[name=Password]').config
                if (!passwordErrors.regex.test(pwd)) {
                    Ext.Msg.alert('提示', passwordErrors.regexText);
                    return;
                }
                //是否同意协议
                var Protocol = view.down('checkboxfield[name=Protocol]').getChecked();
                if (Protocol == false) {
                    Ext.Msg.alert('提示', '未同意并阅读注册协议！');
                    return;
                }
                view.submit({
                    url: config.url + '/User/Registered',
                    method: 'POST',
                    //提交成功
                    success: function (action, response) {
                        //存储用户信息
                        //util.storeSet("logininfor", Ext.JSON.encode(response.data));
                        //util.showMessage(response.msg, true);
                        ////初始化用户数据
                        //view.loadInit();
                        ////更新当前用户设备信息[5秒后执行 不占用菜单请求时间]
                        //setTimeout(SHUtil.UpdateEquipment, 5000);
                        util.redirectTo(this.backUrl, "back");
                        this.reset();
                        util.showMessage("注册成功，请登录！", true);
                    },
                    //提交失败
                    failure: function (action, response) {
                        util.hideMessage();
                        if (response.status == 500) {
                            Ext.Msg.alert('提示', '注册失败！');
                        }
                        else {
                            util.showMessage(response.msg, true);
                        }
                    }
                });
            }
        }
    },
    //初始化
    constructor: function (config) {
        var me = this;
        me.callParent(arguments);
        ////加载头部菜单信息
        me.add(this.getHeader());
    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params) {
            this.backUrl = params.parentUrl;
        }
        if (params.url) {
            //this.setSubUrl(params.url);
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
    }
});