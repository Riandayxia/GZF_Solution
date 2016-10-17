/**-----------------------------------------------------------------
* @explanation:登陆
* @created：Raindya
* @create time：2015/11/24 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define("QST.Main.Retrieve", {
    alternateClassName: 'main_retrieve',
    extend: 'Ext.form.Panel',
    xtype: 'main_retrieve',
    requires: ['Ext.form.FieldSet', 'Ext.field.Password', 'Ext.field.Select'],
    config: {
        scrollable: null,
        title: config.str.Reset,
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
                       placeHolder: '注册时使用的手机号（必填）',
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
                                 style: 'font-size:0.8em;',
                                 height: '40px',
                                 width: '80px',
                                 handler: function (but) {
                                     //this.up('measuremanage_materialbilling_edit').fireEvent('setContract', but);
                                 }
                             }
                         ]
                     },
                     {
                         name: 'Password',
                         placeHolder: '新密码,6-12位的数字或字母（必填）',
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
                    {
                        name: 'Passwords',
                        xtype: 'passwordfield',
                        placeHolder: '再次输入密码（必填）',
                        allowBlank: true
                    },
               ]
           },
        {
            xtype: 'button',
            text: '确认',
            action: 'login',
            cls: 'loginButton',
            style: 'font-size:1.4em;',
            height: '60px',
            margin: '30 20 0 20',
            handler: function (but) {
                var me = this.up('main_retrieve');
                me.fireEvent('formSubmit', but, me);
            }
        }],
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
                var pwds = view.down('passwordfield[name=Passwords]').getValue();
                if (pwd != pwds) {
                    Ext.Msg.alert('提示', '两次输入的密码不一致');
                    return;
                }
                view.submit({
                    url: config.url + '/User/Reset',
                    method: 'POST',
                    //提交成功
                    success: function (action, response) {
                        ////存储用户信息
                        //util.storeSet("logininfor", Ext.JSON.encode(response.data));
                        //util.showMessage(response.msg, true);
                        ////初始化用户数据
                        //view.loadInit();
                        ////更新当前用户设备信息[5秒后执行 不占用菜单请求时间]
                        //setTimeout(SHUtil.UpdateEquipment, 5000);
                        util.redirectTo(this.backUrl, "back");
                        this.reset();
                        util.showMessage("重置成功，请登录！", true);
                    },
                    //提交失败
                    failure: function (action, response) {
                        util.hideMessage();
                        if (response.status == 500) {
                            Ext.Msg.alert('提示', '重置失败！');
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
        if (params)
            this.backUrl = params.parentUrl;
        if (params.url)
            this.Url = params.url
        //获得用户登陆信息
        var user = Ext.decode(util.storeGet("logininfor"));
        this.setValues({ Account: user.LoginName });
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