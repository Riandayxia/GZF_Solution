/**-----------------------------------------------------------------
* @explanation:主界面菜单
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Main.Home', {
    extend: 'Ext.Container',
    xtype: 'main_home',
    requires: ['QST.Main.HomeTopImg', 'QST.Main.HomeMenu', 'QST.Main.HomeMiddleImg'],
    fullscreen: true,
    config: {
        title: '城南花园',
        layout: 'vbox',
        scrollable: {
            directionLock: true,
            //注意横向竖向模式的配置，不能配错  
            direction: 'vertical',
            //隐藏滚动条样式  
            indicators: false
        },
        items: [
            {
                xtype: 'home_top_img'
            }, {
                xtype: 'home_menu',
            }, {
                xtype: 'main_middle_img'
            }, {
                xtype: 'panel',
                cls: 'home_msg',
                height: 60,
                html: '限时抢购推荐'
            }, {
                xtype: 'panel',
                cls: 'home_msg',
                height: 60,
                html: '限时抢购推荐'
            }
        ]
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
        //加载头部
        me.add(this.getHeaderBar());
    },
    //获得头部
    getHeaderBar: function () {
        var me = this;
        if (!this._headerBar) {
            this._headerBar = Ext.create("app.user.NavigationBar", {
                title: me._title,
                docked: 'top',
                items: [{
                    action: 'Back',
                    cls: 'nbutton',
                    align: 'left',
                    iconCls: 'maps',
                    text: '地区',
                    handler: function (but) {
                        me.fireEvent('Back', but, me);
                    }
                }]
            });
        }
        return this._headerBar;
    }
});
