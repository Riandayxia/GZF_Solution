/**-----------------------------------------------------------------
* @explanation:主界面菜单
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Main.HomeMenu', {
    extend: 'Ext.Container',
    xtype: 'home_menu',
    config: {
        layout: 'vbox',
        cls: 'home',
        style: 'margin: 5px 15px;',
        defaults: {
            layout: 'hbox',
            defaults: {
                flex: 1,
                xtype: 'button',
                iconAlign: 'top'
            }
        },
        items: [
            {
                items: [{
                    text: '在线缴费',
                    iconCls: 'wyf',
                    handler: function (but) {
                        util.redirectTo("QST.Property.PAccount.Layout", "", { parentUrl: "QST.Main.Layout" });
                    }
                }, {
                    text: '社区商城',
                    iconCls: 'mall',
                    handler: function (but) {
                        util.redirectTo("QST.Property.Complaints.Edit", "", { parentUrl: "QST.Main.Layout" });
                    }
                }, {
                    text: '家政服务',
                    iconCls: 'housekeeping',

                    handler: function (but) {
                        util.redirectTo("QST.HouseManage.Housekeeping.HouseList", "", { parentUrl: "QST.Main.Layout" });
                    }
                },{
                    text: '社区活动',
                    iconCls: 'information',
                    handler: function (but) {
                        util.redirectTo("QST.Property.Community.Layout", "", { parentUrl: "QST.Main.Layout" });
                    }
                }]
            },
            {
                items: [ {
                    text: '报事报修',
                    iconCls: 'service',
                    handler: function (but) {
                        util.redirectTo("QST.Property.NewsPaper.Edit", "", { parentUrl: "QST.Main.Layout" });
                    }
                }, {
                    text: '便民服务',
                    iconCls: 'convenience',
                    handler: function (but) {
                        util.redirectTo("QST.Property.Convenience.Layout", "", { parentUrl: "QST.Main.Layout" });
                    }
                }, {
                    text: '在线营业厅',
                    iconCls: 'business',
                }, {
                    text: '在线看直播',
                    iconCls: 'live',
                }]
            },
        ],
        listeners: {
        }
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
    }
});
