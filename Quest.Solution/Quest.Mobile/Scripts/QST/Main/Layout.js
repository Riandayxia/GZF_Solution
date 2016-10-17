/**-----------------------------------------------------------------
* @explanation:主界面布局
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Main.Layout', {
    extend: 'Ext.TabPanel',
    xtype: 'main_layout',
    id: 'QST_Main_Layout',
    requires: ['QST.Util', 'QST.Main.Home', 'QST.Mall.Layout', 'QST.Personal.Layout', 'QST.Main.Login'],
    config: {
        fullscreen: true,
        cls: 'navToolbarHone',
        tabBar: {
            docked: 'bottom',
            //高亮
            ui: 'light',
            layout: { //设置每个tab的位置为居中
                align: 'stretch'
            },
            defaults: {
                flex: 1,
                layout: 'card'
            }
        },
        items: [{
            xtype: 'container',
            title: config.str.HomeArea,
            selected: true,
            iconCls: 'home',
            layout: 'card',
            id: 'c_main_home',
            items: [{
                xtype: 'main_home'
            }]
        }, {
            xtype: 'container',
            title: config.str.Wares,
            iconCls: 'star',
            layout: 'card',
            id: 'c_mall_layout',
            items: [{
                xtype: 'mall_layout',
            }]
        }, {
            xtype: 'container',
            title: config.str.Finance,
            iconCls: 'info',
            layout: 'card',
            id: 'c_main_infotab',
            items: [{
                xtype: 'panel',
                html:'接口對接中'
            }]
        }, {
            xtype: 'container',
            title: config.str.MiArea,
            iconCls: 'settings',
            layout: 'card',
            id: 'c_personal_layout',
            items: [{
                xtype: 'personal_layout',
            }]
        }],
        listeners: {
            Back: function () {
                //退出程序
                if (this.isExit)
                    navigator.app.exitApp();
                this.isExit = true;
                util.showMessage(config.str.PressExitApp, true);
            },
            // 菜单切换
            activeitemchange: function (tabBar, newTab, oldTab) {
                var tab = newTab.tab;
                newTab.badgeText = 18;
                var view = newTab.getActiveItem();
                this.tabIndex = 0;
                switch (tab._title) {
                    case config.str.WorkArea:
                        this.tabIndex = 1;
                        break;
                    case config.str.InfoAre:
                        this.tabIndex = 2;
                        break;
                    case config.str.MiArea:
                        this.tabIndex = 3;
                        break;
                    default:
                        this.tabIndex = 0;
                        break;
                }
                tabBar.setActiveItem(newTab);
            }
        }
    },
    //安卓 返回按钮
    onBackTap: function () {
        //得到当前active放回上一级
        var view = Ext.Viewport.getActiveItem();
        //util.redirectTo(view.backUrl, "back");
        view.fireEvent('Back', this, view);
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        // 初始化系统信息
        me.loadInit();
        me.callParent(arguments);
        //加载Cordova
        me.loadCordova();
    },
    // 初始化系统信息
    loadInit: function () {
    },
    //加载Cordova
    loadCordova: function () {
        var me = this;
        document.addEventListener("deviceready", onDeviceReady, false);
        function onDeviceReady() {
            //监听安卓 返回按钮
            document.addEventListener("backbutton", backTap, false);
            function backTap() {
                me.onBackTap();
            }
        }
    }
});
