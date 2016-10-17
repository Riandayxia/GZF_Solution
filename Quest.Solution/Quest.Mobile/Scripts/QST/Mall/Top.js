Ext.define('QST.Mall.Top', {
    extend: 'Ext.Container',
    xtype: 'mall_top',
    alternateClassName: 'mall_top',
    config: {
        docked: 'top',
        cls:'malltop',
        items: [
            {
                xtype: 'toolbar',
                docked: 'top',
                items: [
                    {
                        iconCls: 'list',
                    },
                    {
                        xtype: 'searchfield'
                    }
                ]
            }
        ]
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
    }
})