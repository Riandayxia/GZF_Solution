Ext.define('QST.Property.Convenience.Layout', {
    extend: 'Ext.Container',
    xtype: 'property_convenience_layout',
    alternateClassName: 'convenience',
    requires: ['QST.Property.Convenience.Top', 'QST.Property.Convenience.List','QST.Property.Convenience.Middle'],
    config: {
        layout: 'vbox',
        title: '便民服务',
        scrollable: {
            directionLock: true,
            //注意横向竖向模式的配置，不能配错  
            direction: 'vertical',
            //隐藏滚动条样式  
            indicators: false
        },
        items: [{
            xtype: 'property_convenience_top',
        }, {
            xtype: 'property_convenience_middle'
        } ,{
                xtype: 'panel',
                layout: 'card',
                flex: 3,
                items:[{
                    xtype: 'property_convenience_list'
                }]
        }] ,
        listeners: {
            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back");
            },
        }
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
        //加载头部
        me.add(this.getHeaderBar());
    },  //主界面到此界面时加载[List刷新时会默认加载此方法]
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
})