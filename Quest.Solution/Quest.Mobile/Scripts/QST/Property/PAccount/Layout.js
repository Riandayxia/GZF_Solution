Ext.define('QST.Property.PAccount.Layout', {
    extend: 'Ext.Container',
    xtype: 'property_paccount_layout',
    requires: [ 'QST.Property.PAccount.List', 'QST.Property.PAccount.HList'],
    fullscreen: true,
    config: {
        title: '重庆市',
        layout: 'vbox',
        scrollable: {
            directionLock: true
        },
        items: [
            {
                xtype: 'panel',
                cls: 'home_msg',
                flex: 0.2,
                html: '缴费用户'
            }, {
                xtype: 'panel',
                layout: 'card',
                flex: 3,
                items:[{
                    xtype: 'property_paccount_list'
                }]
            }, {
                layout: 'hbox',
                flex: 1.3, cls: 'bottom',
                style: 'background:#fbfbfb',
                items: [
                    {
                        xtype: "button", text: '缴费记录', style: ' margin-left:30%;',
                        handler: function (but) {
                            util.redirectTo("QST.Property.PAccount.HList", "", { parentUrl: "QST.Property.PAccount.Layout" });
                        }
                    },
                    {
                        xtype: "button", text: '|', style: '',
                    },
                    {
                        xtype: "button", text: '帮助中心', style: '',
                        handler: function (but) {
                            util.redirectTo("", "", { parentUrl: "QST.Property.PAccount.Layout" });
                        }
                    }]
            }
        ],
        listeners: {
            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back");
            },
        }

    },
    //layout: 'vbox',
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
        //加载头部
        me.add(this.getHeaderBar());
    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params)
            this.backUrl = params.parentUrl;
    },
    //获得头部
    getHeaderBar: function () {
        var me = this;
        if (!this._headerBar) {
            this._headerBar = Ext.create("app.user.NavigationBar", {
                title: me._title,
                docked: 'top',
                items: [{
                    iconCls: 'arrow_left',
                    action: 'Back',
                    cls: 'nbutton',
                    align: 'left',
                    handler: function (but) {
                        me.fireEvent('Back', but, me);
                    }
                }]
            });
        }
        return this._headerBar;
    }
});