/**-----------------------------------------------------------------
* @explanation:热销
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Mall.Selling', {
    extend: 'Ext.Container',
    xtype: 'mall_selling',
    config: {
        layout: 'vbox',
        cls: 'home',
        style: 'margin: 5px 15px;',
        defaults: {
            layout: 'hbox',
            defaults: {
                flex: 1,
                xtype: 'button',
                iconAlign: 'bottom'
            }
        },
        items: [
            {
                items: [{
                    text: '电器节打折',
                    iconCls: 'htgl',
                    handler: function (but) {
                    }
                }, {
                    text: '红米国庆特价',
                    iconCls: 'htgl',
                    handler: function (but) {
                    }
                }, {
                    text: '新西兰苹果',
                    iconCls: 'htgl',
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
