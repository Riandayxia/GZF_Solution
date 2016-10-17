/**-----------------------------------------------------------------
* @explanation:主界面信息栏
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Main.InfoTab', {
    extend: 'Ext.TabPanel',
    xtype: 'main_infotab',
    requires: [],
    config: {
        fullscreen: true,
        cls: 'navToolbarH',
        tabBar: {
            //高亮
            ui: 'light',
            layout: { //设置每个tab的位置为居中
                pack: 'center',
                align: 'center'
            },
            scrollable: { //设置可滚动的属性
                direction: 'horizontal',
                indicators: false
            }
        },
        listeners: {
            activeitemchange: function (tabBar, newTab, oldTab) {
                if (config.idata && config.idata.myInfo) {
                    tabBar.cIndex = tabBar.indexOf(newTab) - 1;
                    var view = newTab.getActiveItem();
                    var tab = newTab.tab;
                    switch (tab._title) {
                        case config.string.IApproval:
                            this.tabIndex = 0;
                            view.loadData();
                            break;
                        case config.string.ITask:
                            this.tabIndex = 1;
                            view.loadData();
                            break;
                        case config.string.IProcess:
                            this.tabIndex = 2;
                            view.loadData();
                            break;
                        case config.string.INotification:
                            this.tabIndex = 3;
                            view.loadData();
                            break;
                    };
                }
            }
        }
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
        var menus = me.LoadTopMenu();
        me.setItems(menus);
        me.SetRoll();
    },
    // 加载数据
    loadData: function () {
        var me = this;
        me.showNumber();
        if (!me.tabIndex) {
            me.tabIndex = 0;
        }
        var tab = this.getActiveItem(me.tabIndex);
        var view = tab.getActiveItem();
        view.loadData();
    },
    SetRoll: function () {
        var me = this;
        me.element.on({
            swipe: function (e, target, options, eOpts) {
                var index = me.cIndex;
                if (e.direction === 'right' && e.distance >= 80) {
                    index -= 1;
                    if (index <= 0) {
                        index = 0;
                    }
                }
                if (e.direction === 'left' && e.distance >= 80) {
                    index += 1;
                    if (index >= me.tLength) {
                        index = me.tLength - 1;
                    }
                }
                me.setActiveItem(index);
            }
        });
    },
    //显示信息数量
    showNumber: function () {
        var aTab = this.down('.tab[title="' + config.string.IApproval + '"]');
        var tTab = this.down('.tab[title="' + config.string.ITask + '"]');
        var pTab = this.down('.tab[title="' + config.string.IProcess + '"]');
        var nTab = this.down('.tab[title="' + config.string.INotification + '"]');
        var main_layout = this.up('main_layout');
        Ext.Ajax.request({
            url: config.url + '/WFTemple/GetWFInfo',
            params: { PId: config.project.pId },
            success: function (response) {
                var rdata = Ext.decode(response.responseText);
                if (rdata.success) {
                    aTab.setBadgeText(rdata.data.ACount);
                    tTab.setBadgeText(rdata.data.TCount);
                    pTab.setBadgeText(rdata.data.PCount);
                    nTab.setBadgeText(rdata.data.NCount);
                    var number = rdata.data.ACount + rdata.data.TCount + rdata.data.PCount + rdata.data.NCount;
                    main_layout.showNumber(number);
                }
            }
        });
    },
    // 加载头部菜单
    LoadTopMenu: function () {

        var menus = [{
                xtype: 'container',
                title: config.string.INotification,
                layout: 'card',
                items: [{
                    xtype: 'projectmanage_notification_list',
                }]
            },{
            xtype: 'container',
            title: config.string.IApproval,
            layout: 'card',
            items: [{
                xtype: 'wfapproval_list',
            }]
        }, {
            xtype: 'container',
            title: config.string.ITask,
            layout: 'card',
            items: [{
                xtype: 'wftask_list',
            }]
        }, {
            xtype: 'container',
            title: config.string.IProcess,
            layout: 'card',
            items: [{
                xtype: 'myprocess_list',
            }]
        }];
        this.tLength = menus.length;
        return menus;
    }
});
