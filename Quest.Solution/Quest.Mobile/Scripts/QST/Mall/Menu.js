/**-----------------------------------------------------------------
* @explanation:商城面菜单
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Mall.Menu', {
    extend: 'Ext.Container',
    xtype: 'mall_menu',
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
                    text: '数码电器',
                    iconCls: 'xiyiji',
                    handler: function (but) {
                    }
                }, {
                    text: '精品水果',
                    iconCls: 'shuiguo',
                    handler: function (but) {
                    }
                }, {
                    text: '食品生鲜',
                    iconCls: 'shengxian',
                    handler: function (but) {
                    }
                }, {
                    text: '日用百货',
                    iconCls: 'zhijin',
                    handler: function (but) {
                    }
                }]
            }
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
