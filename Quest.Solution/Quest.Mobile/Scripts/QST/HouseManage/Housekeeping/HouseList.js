Ext.define('QST.HouseManage.Housekeeping.HouseList', {
    extend: 'Ext.Container',
    xtype: 'housekeeping_houselist',
    alternateClassName: 'personal',
    requires: ['QST.HouseManage.Housekeeping.HouseBottom', 'QST.HouseManage.Housekeeping.HouseMiddleImg'],
    config: {
        scrollable: {
            directionLock: true,
            //注意横向竖向模式的配置，不能配错  
            direction: 'vertical',
            //隐藏滚动条样式  
            indicators: false
        },
        items: [{
            xtype: 'house_middle_img'
        }, {
            xtype: 'housemanage_housekeeping_housebottom'
        }],
        listeners: {
            //返回前一界面
            Back: function (list) {
                util.redirectTo(this.backUrl, "back", {});
                this.reset();
            }
        }
    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params) {
            this.backUrl = params.parentUrl;
        }
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
        this.add(this.getHomeHeader());
    },
    //头部菜单信息(private)
    getHomeHeader: function () {
        var me = this;
        var arrBut = [];
        //返回按钮
        arrBut.push({
            iconCls: 'arrow_left',
            action: 'Back',
            cls: 'nbutton',
            align: 'left',
            handler: function (but) {
                me.fireEvent('Back', but, me);
            }
        });
        var nbcls = Ext.baseCSSPrefix + 'nbConten-bar navigationBar';
        if (me._navigationbarCls)
            nbcls = me._navigationbarCls;
        if (!this._homeHeaderBar) {
            this._homeHeaderBar = Ext.create("app.user.NavigationBar", {
                title: config.str.Housekeeping,
                items: arrBut,
                docked: 'top',
                config: {
                    baseCls: Ext.baseCSSPrefix + 'toolbar',
                    cls: nbcls
                }
            });
        }
        return this._homeHeaderBar;
    },
})